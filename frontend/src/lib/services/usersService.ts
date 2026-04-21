import { PUBLIC_API_BASE_URL } from '$env/static/public';
import type { users, ServiceResponse } from '$lib/types/api';

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

export const usersService = {
  async getAll(): Promise<ServiceResponse<users[]>> {
    const response = await fetch(`${API_BASE_URL}/users`, {
      headers: {
        ...authHeaders()
      }
    });
    return await response.json();
  },

//   async getById(id: number): Promise<ServiceResponse<users>> {
//     const response = await fetch(`${API_BASE_URL}/users/${id}`, {
//       headers: {
//         ...authHeaders()
//       }
//     });
//     return await response.json();
//   },
//   async getbyuserID(userId:number): Promise<ServiceResponse<users>> {
//     const response = await fetch(`${API_BASE_URL}/booking/${userId}`, {
//       headers: {
//         ...authHeaders()
//       }
//     });
//     return await response.json();
//   },

//   async create(payload: BookingCreateRequest): Promise<ServiceResponse<number>> {
//     const response = await fetch(`${API_BASE_URL}/booking`, {
//       method: 'POST',
//       headers: {
//         'Content-Type': 'application/json',
//         ...authHeaders()
//       },
//       body: JSON.stringify({
//         roomId: payload.roomId,
//         offerId: (payload as { offer_id?: number | null }).offer_id ?? null,
//         guestName: payload.guestName,
//         guestEmail: payload.guestEmail,
//         checkIn: payload.checkIn,
//         checkOut: payload.checkOut,
//         totalPrice: payload.totalPrice,
//         discountAmount: payload.discountAmount,
//         finalPrice: payload.finalprice,
//         numberOfPeople: payload.numberOfPeople
//       })
//     });
//     return await response.json();
//   },

//   async update(payload: { id: number; status: string }): Promise<ServiceResponse<boolean>> {
//     const response = await fetch(`${API_BASE_URL}/booking/${payload.id}/status`, {
//       method: 'PATCH',
//       headers: {
//         'Content-Type': 'application/json',
//         ...authHeaders()
//       },
//       body: JSON.stringify({ status: payload.status })
//     });
//     return await response.json();
//   },

//   async remove(id: number): Promise<ServiceResponse<boolean>> {
//     const response = await fetch(`${API_BASE_URL}/booking/${id}`, {
//       method: 'DELETE',
//       headers: {
//         ...authHeaders()
//       }
//     });
//     return await response.json();
//   }
};
