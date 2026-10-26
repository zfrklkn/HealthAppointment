
import { useMemo, useState } from 'react';
import DoctorCard from '../components/doctors/DoctorCard';
import { mockDoctors } from '../data/mockDoctors';
import type { Doctor } from '../types/doctor';

interface DoctorsProps {
    initialQuery?: string;
    initialSpecialty?: string;
    onViewDoctor: (doctor: Doctor) => void;
    onBookAppointment: (doctor: Doctor) => void;
}

export default function Doctors({
    initialQuery = '',
    initialSpecialty = 'Tümü',
    onViewDoctor,
    onBookAppointment,
}: DoctorsProps) {
    const [query, setQuery] = useState(initialQuery);
    const [specialty, setSpecialty] = useState(initialSpecialty);

    const specialties = [
        'Tümü',
        ...new Set(mockDoctors.map((doctor) => doctor.specialty)),
    ];

    const filteredDoctors = useMemo(() => {
        return mockDoctors.filter((doctor) => {
            const matchesQuery = doctor.name
                .toLocaleLowerCase('tr-TR')
                .includes(query.trim().toLocaleLowerCase('tr-TR'));

            const matchesSpecialty =
                specialty === 'Tümü' ||
                doctor.specialty === specialty;

            return matchesQuery && matchesSpecialty;
        });
    }, [query, specialty]);

    return (
        <main className="wrap" style={{ padding: '40px 20px' }}>
            <h1>Doktorlarımız</h1>
            <p className="mut">
                Uzman doktorlarımız arasından size uygun olanı seçin.
            </p>

            <div className="sb" style={{ marginTop: 24 }}>
                <input
                    className="in"
                    type="search"
                    placeholder="Doktor adı ara..."
                    aria-label="Doktor adı ara"
                    value={query}
                    onChange={(event) => setQuery(event.target.value)}
                />

                <select
                    className="in"
                    aria-label="Branş seç"
                    value={specialty}
                    onChange={(event) => setSpecialty(event.target.value)}
                >
                    {specialties.map((item) => (
                        <option key={item} value={item}>
                            {item}
                        </option>
                    ))}
                </select>
            </div>

            <p className="mut" style={{ margin: '24px 0 16px' }}>
                {filteredDoctors.length} doktor bulundu.
            </p>

            {filteredDoctors.length > 0 ? (
                <div className="grid g2">
                    {filteredDoctors.map((doctor) => (
                        <DoctorCard
                            key={doctor.id}
                            doctor={doctor}
                            onViewProfile={onViewDoctor}
                            onBookAppointment={onBookAppointment}
                        />
                    ))}
                </div>
            ) : (
                <div className="card" style={{ textAlign: 'center' }}>
                    <h3>Doktor bulunamadı</h3>
                    <p className="mut">
                        Farklı bir doktor adı veya branş deneyebilirsiniz.
                    </p>
                </div>
            )}
        </main>
    );
}
