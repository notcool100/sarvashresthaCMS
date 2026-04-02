import type { LoginRequest, RegisterRequest, AuthResponse, ServiceResponse } from '../types/api';

const API_BASE_URL = 'https://localhost:7136/api'; // Update with your actual backend URL

export const authService = {
  async login(request: LoginRequest): Promise<ServiceResponse<AuthResponse>> {
    const response = await fetch(`${API_BASE_URL}/auth/login`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(request)
    });
    return await response.json();
  },

  async register(request: RegisterRequest): Promise<ServiceResponse<AuthResponse>> {
    const response = await fetch(`${API_BASE_URL}/auth/register`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(request)
    });
    return await response.json();
  },

  async refreshToken(accessToken: string, refreshToken: string): Promise<ServiceResponse<AuthResponse>> {
    const response = await fetch(`${API_BASE_URL}/auth/refresh-token`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ accessToken, refreshToken })
    });
    return await response.json();
  },

  logout() {
    localStorage.removeItem('auth_data');
  }
};
