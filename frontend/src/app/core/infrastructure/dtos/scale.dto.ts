export interface CreateScaleDto {
  name: string;
  ipAddress: string;
  port: number;
  //isActive: boolean;
}

export interface UpdateScaleDto extends CreateScaleDto {}