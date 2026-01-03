export interface InstallationDto {
    id?: string;
    name: string;
    address: string;
    location: string;
    city: string;
    active: boolean;
}

export interface UpdateInstallationDto extends InstallationDto {}