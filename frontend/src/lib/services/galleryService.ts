import { PUBLIC_API_BASE_URL } from '$env/static/public';
import type { GalleryItem, GalleryItemRequest, ServiceResponse } from '$lib/types/api';

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

function authHeaders(): HeadersInit {
  const token = getAccessToken();
  return token ? { Authorization: `Bearer ${token}` } : {};
}

export const galleryService = {
  async getAll(): Promise<ServiceResponse<GalleryItem[]>> {
    const response = await fetch(`${API_BASE_URL}/gallery`);
    return await response.json();
  },

  async create(payload: GalleryItemRequest): Promise<ServiceResponse<number>> {
    const response = await fetch(`${API_BASE_URL}/gallery`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        ...authHeaders()
      },
      body: JSON.stringify(payload)
    });
    return await response.json();
  },

  async remove(id: number): Promise<ServiceResponse<boolean>> {
    const response = await fetch(`${API_BASE_URL}/gallery/${id}`, {
      method: 'DELETE',
      headers: {
        ...authHeaders()
      }
    });
    return await response.json();
  }
};
