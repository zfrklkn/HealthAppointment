import { useEffect, useState } from 'react';
import { doctorService } from '../services/doctorService';
import { mapDoctorResponseToDoctor } from '../mappers/doctorMapper';
import type { Doctor } from '../types/doctor';

export function useDoctors() {
    const [doctors, setDoctors] = useState<Doctor[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const controller = new AbortController();

        const fetchDoctors = async () => {
            try {
                setIsLoading(true);
                setError(null);

                const response = await doctorService.getAll(
                    controller.signal
                );

                if (controller.signal.aborted) return;

                setDoctors(response.map(mapDoctorResponseToDoctor));
            } catch {
                if (controller.signal.aborted) return;

                setError('Doktorlar yüklenirken bir hata oluştu.');
            } finally {
                if (!controller.signal.aborted) {
                    setIsLoading(false);
                }
            }
        };

        void fetchDoctors();

        return () => controller.abort();
    }, []);

    return { doctors, isLoading, error };
}