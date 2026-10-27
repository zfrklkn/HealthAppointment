import { apiClient } from './apiClient';

export interface DoctorResponseDto {
    id: string;
    firstName: string;
    lastName: string;
    title: string;
    specialtyId: string;
    biography: string | null;
    profileImageUrl: string | null;
    isActive: boolean;
}

export const doctorService = {
    async getAll(signal?: AbortSignal): Promise<DoctorResponseDto[]> {
        const response = await apiClient.get<DoctorResponseDto[]>(
            '/api/doctors',
            { signal }
        );

        return response.data;
    },
};