
import { useState } from 'react';
import type { Doctor } from '../types/doctor';

interface BookAppointmentProps {
    doctor: Doctor;
    onBack: () => void;
}

function getTodayLocal(): string {
    const now = new Date();
    const year = now.getFullYear();
    const month = String(now.getMonth() + 1).padStart(2, '0');
    const day = String(now.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`;
}

export default function BookAppointment({
    doctor,
    onBack,
}: BookAppointmentProps) {
    const [selectedDate, setSelectedDate] = useState('');
    const [selectedTime, setSelectedTime] = useState<string | null>(null);
    const [reason, setReason] = useState('');
    const [showSummary, setShowSummary] = useState(false);

    const today = getTodayLocal();

    return (
        <main className="wrap" style={{ padding: '40px 20px' }}>
            <button
                className="btn o sm"
                type="button"
                onClick={onBack}
                style={{ marginBottom: 24 }}
            >
                ← Doktor Profiline Dön
            </button>

            <h1>Randevu Oluştur</h1>
            <p className="mut">
                Randevu almak istediğiniz tarihi seçin.
            </p>

            <div className="grid g2" style={{ marginTop: 28 }}>
                <section className="card">
                    <h2>Seçilen Doktor</h2>

                    <h3 style={{ marginTop: 20 }}>{doctor.name}</h3>
                    <p className="mut">{doctor.title}</p>
                    <p className="mut">📍 {doctor.hospital}</p>

                    <span className="bdg s-ok">
                        {doctor.specialty}
                    </span>
                </section>

                <section className="card">
                    <h2>Randevu Tarihi</h2>

                    <label
                        htmlFor="appointment-date"
                        style={{
                            display: 'block',
                            marginTop: 20,
                            marginBottom: 8,
                        }}
                    >
                        Tarih seçiniz
                    </label>

                    <input
                        id="appointment-date"
                        className="in"
                        type="date"
                        min={today}
                        value={selectedDate}
                        onChange={(event) => {
                            setSelectedDate(event.target.value);
                            setSelectedTime(null);
                            setShowSummary(false);
                        }}
                        style={{ width: '100%' }}
                    />


                    {selectedDate && (
                        <div style={{ marginTop: 24 }}>
                            <p>
                                <strong>Seçilen tarih:</strong>{' '}
                                {new Date(
                                    `${selectedDate}T12:00:00`
                                ).toLocaleDateString('tr-TR', {
                                    day: 'numeric',
                                    month: 'long',
                                    year: 'numeric',
                                    weekday: 'long',
                                })}
                            </p>

                            <h3 style={{ marginTop: 24, marginBottom: 8 }}>
                                Saat Seçimi
                            </h3>

                            <p className="mut" style={{ fontSize: 13 }}>
                                Aşağıdaki saatler yalnızca arayüz testi için
                                gösterilmektedir. Gerçek müsaitlik bilgisi değildir.
                            </p>

                            <div
                                style={{
                                    display: 'grid',
                                    gridTemplateColumns:
                                        'repeat(auto-fit, minmax(85px, 1fr))',
                                    gap: 10,
                                    marginTop: 16,
                                }}
                            >
                                {DEMO_TIME_SLOTS.map((time) => (
                                    <button
                                        key={time}
                                        type="button"
                                        className={
                                            selectedTime === time ? 'btn sm' : 'btn o sm'
                                        }
                                        aria-pressed={selectedTime === time}
                                        onClick={() => {
                                            setSelectedTime(time);
                                            setShowSummary(false);
                                        }}
                                    >
                                        {time}
                                    </button>
                                ))}
                            </div>

                            {selectedTime && (
                                <div style={{ marginTop: 24 }}>
                                    <p>
                                        <strong>Seçilen saat:</strong> {selectedTime}
                                    </p>

                                    <button
                                        className="btn"
                                        type="button"
                                        onClick={() => setShowSummary(true)}
                                    >
                                        Devam Et
                                    </button>
                                </div>
                            )}
                        </div>
                    )}
                </section>
            </div>


            {showSummary && selectedDate && selectedTime && (
                <section
                    className="card"
                    style={{ marginTop: 28 }}
                >
                    <h2>Randevu Özeti</h2>

                    <p className="mut">
                        Bilgilerinizi kontrol ederek randevu talebinizi
                        hazırlayabilirsiniz.
                    </p>

                    <div style={{ marginTop: 24, lineHeight: 2 }}>
                        <p>
                            <strong>Doktor:</strong> {doctor.name}
                        </p>

                        <p>
                            <strong>Uzmanlık:</strong> {doctor.specialty}
                        </p>

                        <p>
                            <strong>Hastane:</strong> {doctor.hospital}
                        </p>

                        <p>
                            <strong>Tarih:</strong>{' '}
                            {new Date(
                                `${selectedDate}T12:00:00`
                            ).toLocaleDateString('tr-TR')}
                        </p>

                        <p>
                            <strong>Saat:</strong> {selectedTime}
                        </p>
                    </div>

                    <div style={{ marginTop: 24 }}>
                        <label
                            htmlFor="appointment-reason"
                            style={{
                                display: 'block',
                                marginBottom: 8,
                                fontWeight: 600,
                            }}
                        >
                            Randevu Nedeni (İsteğe bağlı)
                        </label>

                        <textarea
                            id="appointment-reason"
                            className="in"
                            rows={4}
                            maxLength={500}
                            placeholder="Randevu nedeninizi kısaca açıklayabilirsiniz..."
                            value={reason}
                            onChange={(event) =>
                                setReason(event.target.value)
                            }
                            style={{
                                width: '100%',
                                resize: 'vertical',
                            }}
                        />
                    </div>

                    <div
                        style={{
                            display: 'flex',
                            gap: 12,
                            marginTop: 24,
                            flexWrap: 'wrap',
                        }}
                    >
                        <button
                            className="btn o"
                            type="button"
                            onClick={() => setShowSummary(false)}
                        >
                            Seçimi Düzenle
                        </button>

                        <button
                            className="btn"
                            type="button"
                            onClick={() => {
                                console.log('Randevu taslağı:', {
                                    doctorId: doctor.id,
                                    date: selectedDate,
                                    time: selectedTime,
                                    reason,
                                });

                                window.alert(
                                    'Randevu bilgileri hazır. API bağlantısını sonraki adımda yapacağız.'
                                );
                            }}
                        >
                            Randevu Talebini Hazırla
                        </button>
                    </div>
                </section>
            )}

        </main>
    );
}

const DEMO_TIME_SLOTS = [
    '09:00',
    '09:30',
    '10:00',
    '10:30',
    '11:00',
    '11:30',
    '13:00',
    '13:30',
    '14:00',
    '14:30',
    '15:00',
    '15:30',
    '16:00',
    '16:30',
];
