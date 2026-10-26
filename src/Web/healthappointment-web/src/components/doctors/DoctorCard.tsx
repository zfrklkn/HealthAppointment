
import type { Doctor } from '../../types/doctor';

interface DoctorCardProps {
    doctor: Doctor;
    onViewProfile: (doctor: Doctor) => void;
    onBookAppointment: (doctor: Doctor) => void;
}

export default function DoctorCard({
    doctor,
    onViewProfile,
    onBookAppointment,
}: DoctorCardProps) {
    const initials = doctor.name
        .replace('Dr. ', '')
        .split(' ')
        .map((part) => part.charAt(0))
        .join('');

    return (
        <div className="card h">
            <div
                style={{
                    display: 'flex',
                    gap: 14,
                    alignItems: 'flex-start',
                }}
            >
                <div className="av">{initials}</div>

                <div style={{ flex: 1, minWidth: 0 }}>
                    <b>{doctor.name}</b>

                    <div className="mut" style={{ fontSize: 13 }}>
                        {doctor.title}
                    </div>

                    <div className="mut" style={{ fontSize: 12 }}>
                        📍 {doctor.hospital} · ★ {doctor.rating}
                    </div>
                </div>

                <span
                    className={`bdg ${doctor.isAvailable ? 's-ok' : 's-gr'
                        }`}
                >
                    {doctor.isAvailable ? 'Uygun' : 'Dolu'}
                </span>
            </div>

            <p
                className="mut"
                style={{ fontSize: 13, margin: '16px 0' }}
            >
                {doctor.biography}
            </p>

            <div
                style={{
                    display: 'flex',
                    gap: 8,
                    flexWrap: 'wrap',
                }}
            >
                <button
                    className="btn o sm"
                    onClick={() => onViewProfile(doctor)}
                >
                    Profili Gör
                </button>

                <button
                    className="btn sm"
                    disabled={!doctor.isAvailable}
                    onClick={() => onBookAppointment(doctor)}
                >
                    Randevu Al
                </button>
            </div>
        </div>
    );
}
