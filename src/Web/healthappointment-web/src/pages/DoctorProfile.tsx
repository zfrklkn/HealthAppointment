
import type { Doctor } from '../types/doctor';

interface DoctorProfileProps {
    doctor: Doctor;
    onBack: () => void;
    onBookAppointment: (doctor: Doctor) => void;
}

export default function DoctorProfile({
    doctor,
    onBack,
    onBookAppointment,
}: DoctorProfileProps) {
    const initials = doctor.name
        .replace('Dr. ', '')
        .split(' ')
        .map((part) => part.charAt(0))
        .join('');

    return (
        <main className="wrap" style={{ padding: '40px 20px' }}>
            <button
                className="btn o sm"
                type="button"
                onClick={onBack}
                style={{ marginBottom: 24 }}
            >
                ← Doktorlara Dön
            </button>

            <div className="card">
                <div
                    style={{
                        display: 'flex',
                        alignItems: 'center',
                        gap: 20,
                        flexWrap: 'wrap',
                    }}
                >
                    <div
                        className="av"
                        style={{
                            width: 88,
                            height: 88,
                            fontSize: 28,
                        }}
                    >
                        {initials}
                    </div>

                    <div style={{ flex: 1 }}>
                        <h1 style={{ marginBottom: 8 }}>{doctor.name}</h1>
                        <p className="mut">{doctor.title}</p>
                        <p className="mut">📍 {doctor.hospital}</p>
                        <p>⭐ {doctor.rating} / 5</p>
                    </div>

                    <span
                        className={`bdg ${doctor.isAvailable ? 's-ok' : 's-gr'
                            }`}
                    >
                        {doctor.isAvailable ? 'Uygun' : 'Dolu'}
                    </span>
                </div>

                <hr style={{ margin: '28px 0', opacity: 0.15 }} />

                <h2>Doktor Hakkında</h2>
                <p className="mut" style={{ lineHeight: 1.8 }}>
                    {doctor.biography}
                </p>

                <h2 style={{ marginTop: 28 }}>Uzmanlık Alanı</h2>
                <p className="mut">{doctor.specialty}</p>

                <button
                    className="btn"
                    type="button"
                    disabled={!doctor.isAvailable}
                    style={{ marginTop: 28 }}
                    onClick={() => onBookAppointment(doctor)}
                >
                    Randevu Al
                </button>
            </div>
        </main>
    );
}
