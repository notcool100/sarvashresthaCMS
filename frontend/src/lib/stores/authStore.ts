import { writable } from 'svelte/store';
import { browser } from '$app/environment';
import type { AuthResponse } from '../types/api';

const initialAuth = browser ? JSON.parse(localStorage.getItem('auth_data') || 'null') : null;

export const authStore = writable<AuthResponse | null>(initialAuth);

if (browser) {
  authStore.subscribe((value) => {
    if (value) {
      localStorage.setItem('auth_data', JSON.stringify(value));
    } else {
      localStorage.removeItem('auth_data');
    }
  });
}
