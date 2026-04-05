import { PUBLIC_API_BASE_URL } from '$env/static/public';
import type { booking, BookingCreateRequest, ServiceResponse } from '$lib/types/api';

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

export const bookingService = {
  async getAll(): Promise<ServiceResponse<booking[]>> {
    const response = await fetch(`${API_BASE_URL}/booking`);
    return await response.json();
  },

  async getById(id: number): Promise<ServiceResponse<booking>> {
    const response = await fetch(`${API_BASE_URL}/booking/${id}`);
    return await response.json();
  },

  async create(payload: BookingCreateRequest): Promise<ServiceResponse<number>> {
    const response = await fetch(`${API_BASE_URL}/booking`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        ...authHeaders()
      },
      body: JSON.stringify({
        roomId: payload.room_id,
        offerId: (payload as { offer_id?: number | null }).offer_id ?? null,
        guestName: payload.guest_name,
        guestEmail: payload.email,
        checkIn: payload.checkin,
        checkOut: payload.checkout,
        totalPrice: payload.price,
        discountAmount: payload.discountamount,
        finalPrice: payload.finalprice
      })
    });
    return await response.json();
  },

  async update(payload: { id: number; status: string }): Promise<ServiceResponse<boolean>> {
    const response = await fetch(`${API_BASE_URL}/booking/${payload.id}/status`, {
      method: 'PATCH',
      headers: {
        'Content-Type': 'application/json',
        ...authHeaders()
      },
      body: JSON.stringify({ status: payload.status })
    });
    return await response.json();
  },

  async remove(id: number): Promise<ServiceResponse<boolean>> {
    const response = await fetch(`${API_BASE_URL}/booking/${id}`, {
      method: 'DELETE',
      headers: {
        ...authHeaders()
      }
    });
    return await response.json();
  }
};
