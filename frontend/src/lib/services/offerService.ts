import { PUBLIC_API_BASE_URL } from '$env/static/public';
import type { Offer, OfferRequest, ServiceResponse } from '$lib/types/api';

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

export const offerService = {
  async getAll(): Promise<ServiceResponse<Offer[]>> {
    const response = await fetch(`${API_BASE_URL}/offer`, { headers: { ...authHeaders() } });
    return await response.json();
  },

  async getById(id: number): Promise<ServiceResponse<Offer>> {
    const response = await fetch(`${API_BASE_URL}/offer/${id}`, { headers: { ...authHeaders() } });
    return await response.json();
  },

  async create(payload: OfferRequest): Promise<ServiceResponse<number>> {
    const response = await fetch(`${API_BASE_URL}/offer`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        ...authHeaders()
      },
      body: JSON.stringify(payload)
    });
    return await response.json();
  },

  async update(id: number, payload: OfferRequest): Promise<ServiceResponse<boolean>> {
    const response = await fetch(`${API_BASE_URL}/offer/${id}`, {
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
    const response = await fetch(`${API_BASE_URL}/offer/${id}`, {
      method: 'DELETE',
      headers: { ...authHeaders() }
    });
    return await response.json();
  }
};
