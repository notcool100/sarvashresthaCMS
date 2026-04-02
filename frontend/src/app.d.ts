import type { AuthResponse } from '$lib/types/api';

declare global {
	namespace App {
		// interface Error {}
		interface Locals {
			user: AuthResponse | null;
		}
		// interface PageData {}
		// interface PageState {}
		// interface Platform {}
	}
}

export {};
