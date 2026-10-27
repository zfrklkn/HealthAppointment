
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

                    {doctor.hospital && (
                        <p className="mut">📍 {doctor.hospital}</p>
                    )}

                    {doctor.rating !== null && (
                        <p>⭐ {doctor.rating} / 5</p>
                    )}

                    <span
                        className={`bdg ${doctor.isActive ? 's-ok' : 's-gr'
                            }`}
                    >
                        {doctor.isActive ? 'Aktif' : 'Pasif'}
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
                    disabled={!doctor.isActive}
                    style={{ marginTop: 28 }}
                    onClick={() => onBookAppointment(doctor)}
                >
                    Randevu Al
                </button>
            </div>
        </main>
    );
}
