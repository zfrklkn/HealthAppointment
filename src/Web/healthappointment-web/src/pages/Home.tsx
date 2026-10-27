
import { useState } from 'react';
import DoctorCard from '../components/doctors/DoctorCard';
import { useDoctors } from '../hooks/useDoctors';
import type { Doctor } from '../types/doctor';

interface HomeProps {
    onSearch: (query: string, specialty: string) => void;
    onViewDoctor: (doctor: Doctor) => void;
    onBookAppointment: (doctor: Doctor) => void;
}

const specialties = [
    'Tümü',
    'Dahiliye',
    'Kardiyoloji',
    'Dermatoloji',
    'Ortopedi',
    'Çocuk Sağlığı',
    'Nöroloji',
];

export default function Home({ onSearch, onViewDoctor, onBookAppointment }: HomeProps) {
    const [query, setQuery] = useState('');
    const [specialty, setSpecialty] = useState('Tümü');
    const { doctors, isLoading, error } = useDoctors();

    return (
        <>
            <section className="hero">
                <div className="wrap">
                    <h1>
                        Sağlığınız
                        <br />
                        <span>Bizim Önceliğimiz</span>
                    </h1>

                    <p
                        className="mut"
                        style={{ maxWidth: 480, fontSize: 17 }}
                    >
                        Alanında uzman doktorlarla kolayca randevu alın,
                        sağlığınıza bir adım daha yaklaşın.
                    </p>

                    <form
                        className="sb"
                        onSubmit={(event) => {
                            event.preventDefault();
                            onSearch(query, specialty);
                        }}
                    >
                        <input
                            className="in"
                            type="search"
                            aria-label="Doktor ara"
                            placeholder="Doktor adı ara"
                            value={query}
                            onChange={(event) => setQuery(event.target.value)}
                        />

                        <select
                            className="in"
                            aria-label="Branş"
                            value={specialty}
                            onChange={(event) =>
                                setSpecialty(event.target.value)
                            }
                        >
                            {specialties.map((item) => (
                                <option key={item} value={item}>
                                    {item}
                                </option>
                            ))}
                        </select>

                        <button className="btn" type="submit">
                            Doktor Ara
                        </button>
                    </form>
                </div>
            </section>

            <div
                className="wrap"
                style={{ padding: '28px 20px' }}
            >
                <div className="grid g3">
                    {[
                        {
                            icon: '🩺',
                            title: 'Uzman Doktorlar',
                            description: 'Alanında deneyimli hekimler',
                        },
                        {
                            icon: '⏱',
                            title: 'Hızlı Randevu',
                            description: 'Dakikalar içinde randevu alın',
                        },
                        {
                            icon: '🛡',
                            title: 'Güvenli ve Kolay',
                            description: 'Kişisel verileriniz korunur',
                        },
                    ].map((feature) => (
                        <div
                            key={feature.title}
                            style={{
                                display: 'flex',
                                gap: 12,
                                alignItems: 'center',
                            }}
                        >
                            <div
                                className="av"
                                style={{
                                    background: 'var(--sky)',
                                    color: 'var(--pri)',
                                    width: 48,
                                    height: 48,
                                }}
                            >
                                {feature.icon}
                            </div>

                            <div>
                                <b>{feature.title}</b>
                                <div
                                    className="mut"
                                    style={{ fontSize: 13 }}
                                >
                                    {feature.description}
                                </div>
                            </div>
                        </div>
                    ))}
                </div>

                <section style={{ marginTop: 40 }}>
                    <h2 style={{ marginBottom: 16 }}>
                        Öne Çıkan Doktorlar
                    </h2>

                    {isLoading ? (
                        <p className="mut">Doktorlar yükleniyor...</p>
                    ) : error ? (
                        <p className="mut" role="alert">
                            {error}
                        </p>
                    ) : doctors.length === 0 ? (
                        <p className="mut">
                            Henüz listelenecek doktor bulunmuyor.
                        </p>
                    ) : (
                        <div className="grid g2">
                            {doctors
                                .filter((doctor) => doctor.isActive)
                                .slice(0, 3)
                                .map((doctor) => (
                                    <DoctorCard
                                        key={doctor.id}
                                        doctor={doctor}
                                        onViewProfile={onViewDoctor}
                                        onBookAppointment={onBookAppointment}
                                    />
                                ))}
                        </div>
                    )}
                </section>
            </div>
        </>
    );
}
