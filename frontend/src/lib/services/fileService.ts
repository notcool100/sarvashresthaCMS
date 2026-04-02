import { PUBLIC_API_BASE_URL } from '$env/static/public';
import type { ServiceResponse } from '$lib/types/api';

const API_BASE_URL = PUBLIC_API_BASE_URL;

function getAccessToken(): string | null {
  if (typeof localStorage === 'undefined') return null;
  const raw = localStorage.getItem('auth_data');
  if (!raw) return null;
  try {
    const parsed = JSON.parse(raw);
    return parsed?.accessToken ?? null;
  } catch {
    return null;
  }
}

function authHeaders() {
  const token = getAccessToken();
  return token ? { Authorization: `Bearer ${token}` } : {};
}

export const fileService = {
  async uploadRoomImage(file: File): Promise<ServiceResponse<string>> {
    const formData = new FormData();
    formData.append('file', file);

    const response = await fetch(`${API_BASE_URL}/upload/room`, {
      method: 'POST',
      headers: { ...authHeaders() },
      body: formData
    });
    return await response.json();
  },

  async uploadRoomImages(files: FileList | File[]): Promise<ServiceResponse<string[]>> {
    const formData = new FormData();
    Array.from(files).forEach((file) => formData.append('files', file));

    const response = await fetch(`${API_BASE_URL}/upload/room/multiple`, {
      method: 'POST',
      headers: { ...authHeaders() },
      body: formData
    });
    return await response.json();
  },

  async uploadOfferImage(file: File): Promise<ServiceResponse<string>> {
    const formData = new FormData();
    formData.append('file', file);

    const response = await fetch(`${API_BASE_URL}/upload/offer`, {
      method: 'POST',
      headers: { ...authHeaders() },
      body: formData
    });
    return await response.json();
  }
};
