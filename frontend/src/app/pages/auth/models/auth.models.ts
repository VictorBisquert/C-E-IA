export interface LoginRequest {
  email: string;
  password: string;
}

export interface AuthResponse {
  token: string;
  succes: boolean;
  message: string;
  expiration: string;
}

export interface UserInfo {
  email: string;
  token: string;
  expiration: Date;
}