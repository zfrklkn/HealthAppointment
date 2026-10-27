import type { Doctor } from '../types/doctor';
import type { DoctorResponseDto } from '../services/doctorService';

export function mapDoctorResponseToDoctor(
    response: DoctorResponseDto
): Doctor {
    return {
        id: response.id,
        name: `${response.firstName} ${response.lastName}`,
        title: response.title,
        specialty: 'Branş bilgisi yüklenmedi',
        hospital: null,
        rating: null,
        biography: response.biography ?? '',
        isActive: response.isActive,
    };
}