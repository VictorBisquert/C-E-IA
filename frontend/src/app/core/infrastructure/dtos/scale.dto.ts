export interface ScaleDto {
  id?: string;
  name: string;
  ipAddress: string;
  port: number;
  isActive: boolean;
}



export interface UpdateScaleDto extends ScaleDto {}