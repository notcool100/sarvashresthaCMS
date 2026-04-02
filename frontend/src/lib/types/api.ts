export interface ServiceResponse<T> {
  data: T | null;
  message: string;
  success: boolean;
  errors: string[] | null;
}

export interface AuthResponse {
  accessToken: string;
  refreshToken: string;
  username: string;
  email: string;
  role: UserRole;
}

export enum UserRole {
  Admin = 1,
  Staff = 2,
  Customer = 3
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  username: string;
  email: string;
  password: string;
}
