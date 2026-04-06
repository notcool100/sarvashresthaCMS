import { PUBLIC_API_BASE_URL } from '$env/static/public';
import type { Room, RoomCreateRequest, RoomUpdateRequest, ServiceResponse } from '$lib/types/api';

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

function authHeaders():HeadersInit {
  const token = getAccessToken();
  return token ? { Authorization: `Bearer ${token}` } : {};
}

export const roomService = {
  async getAll(): Promise<ServiceResponse<Room[]>> {
    const response = await fetch(`${API_BASE_URL}/room`);
    return await response.json();
  },

  async getById(id: number): Promise<ServiceResponse<Room>> {
    const response = await fetch(`${API_BASE_URL}/room/${id}`);
    return await response.json();
  },

  async create(payload: RoomCreateRequest): Promise<ServiceResponse<number>> {
    const response = await fetch(`${API_BASE_URL}/room`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        ...authHeaders()
      },
      body: JSON.stringify(payload)
    });
    return await response.json();
  },

  async update(payload: RoomUpdateRequest): Promise<ServiceResponse<boolean>> {
    const response = await fetch(`${API_BASE_URL}/room/${payload.id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        ...authHeaders()
      },
      body: JSON.stringify(payload)
    });
    return await response.json();
  },

  async remove(id: number): Promise<ServiceResponse<boolean>> {
    const response = await fetch(`${API_BASE_URL}/room/${id}`, {
      method: 'DELETE',
      headers: {
        ...authHeaders()
      }
    });
    return await response.json();
  }
};

