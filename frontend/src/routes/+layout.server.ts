import type { AuthResponse } from '$lib/types/api';
import type { RequestEvent } from '@sveltejs/kit';

export const load = async ({ locals }: RequestEvent) => {
	return {
		user: locals.user as AuthResponse | null
	};
};
