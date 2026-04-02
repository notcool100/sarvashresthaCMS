import { redirect, type Handle } from '@sveltejs/kit';
import { UserRole } from '$lib/types/api';

export const handle: Handle = async ({ event, resolve }) => {
	const authData = event.cookies.get('auth_data');

	if (authData) {
		try {
			event.locals.user = JSON.parse(authData);
		} catch (e) {
			event.locals.user = null;
			event.cookies.delete('auth_data', { path: '/' });
		}
	} else {
		event.locals.user = null;
	}

	const isProtectedRoute = event.url.pathname.startsWith('/admin') || 
							 event.url.pathname.startsWith('/staff') ||
							 event.url.pathname.startsWith('/user');

	if (isProtectedRoute && !event.locals.user) {
		throw redirect(303, '/login');
	}

	// Admin route protection
	if (event.url.pathname.startsWith('/admin') && event.locals.user?.role !== UserRole.Admin) {
		throw redirect(303, '/');
	}

	return resolve(event);
};
