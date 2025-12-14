export interface Scale {
  id: string;
  name: string;
  ipAddress: string;
  port: number;
  isActive: boolean;
  createdAt: string;
  lastConnectionAt?: string;
}