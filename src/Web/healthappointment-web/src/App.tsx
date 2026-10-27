
import { useEffect, useState } from 'react';
import Header from './components/layout/Header';
import Home from './pages/Home';
import Doctors from './pages/Doctors';
import type { Doctor } from './types/doctor';
import DoctorProfile from './pages/DoctorProfile';
import BookAppointment from './pages/BookAppointment';
import { doctorService } from './services/doctorService';

function App() {
  const [page, setPage] = useState('home');
  const [searchQuery, setSearchQuery] = useState('');
  const [selectedSpecialty, setSelectedSpecialty] = useState('Tümü');
  const [selectedDoctor, setSelectedDoctor] = useState<Doctor | null>(null);

  useEffect(() => {
    const testDoctorApi = async () => {
      try {
        const doctors = await doctorService.getAll();

        console.log('Doctor API - Gerçek Veriler:', doctors);
      } catch (error) {
        console.error('Doctor API bağlantı hatası:', error);
      }
    };

    testDoctorApi();
  }, []);

  const handleSearch = (query: string, specialty: string) => {
    setSearchQuery(query);
    setSelectedSpecialty(specialty);
    setPage('doctors');
  };

  const handleViewDoctor = (doctor: Doctor) => {
    setSelectedDoctor(doctor);
    setPage('doctor-profile');
  };

  const handleBookAppointment = (doctor: Doctor) => {
    setSelectedDoctor(doctor);
    setPage('book-appointment');
  };

  return (
    <>
      <Header page={page} onNavigate={setPage} />

      {page === 'home' && (
        <Home
          onSearch={handleSearch}
          onViewDoctor={handleViewDoctor}
          onBookAppointment={handleBookAppointment}
        />
      )}

      {page === 'doctors' && (
        <Doctors
          key={`${searchQuery}-${selectedSpecialty}`}
          initialQuery={searchQuery}
          initialSpecialty={selectedSpecialty}
          onViewDoctor={handleViewDoctor}
          onBookAppointment={handleBookAppointment}
        />
      )}

      {page === 'doctor-profile' && selectedDoctor && (
        <DoctorProfile
          doctor={selectedDoctor}
          onBack={() => setPage('doctors')}
          onBookAppointment={handleBookAppointment}
        />
      )}

      {page === 'book-appointment' && selectedDoctor && (
        <BookAppointment
          doctor={selectedDoctor}
          onBack={() => setPage('doctor-profile')}
        />
      )}

      {page !== 'home' && page !== 'doctors' && page !== 'doctor-profile' && page !== 'book-appointment' && (
        <main className="wrap" style={{ paddingTop: 40 }}>
          <h1>
            {page === 'appointments'
              ? 'Randevularım'
              : 'Yönetim Paneli'}
          </h1>
          <p className="mut">
            Bu sayfayı sonraki adımlarda hazırlayacağız.
          </p>
        </main>
      )}
    </>
  );
}

export default App;
