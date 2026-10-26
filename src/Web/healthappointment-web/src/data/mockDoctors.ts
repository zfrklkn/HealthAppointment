
import type { Doctor } from '../types/doctor';

export const mockDoctors: Doctor[] = [
    {
        id: 'd1',
        name: 'Dr. Ayşe Demir',
        title: 'Dahiliye Uzmanı',
        specialty: 'Dahiliye',
        hospital: 'Özel Yaşam Hastanesi',
        rating: 4.9,
        biography:
            '10+ yıl deneyim. Kronik hastalıkların takibi ve genel sağlık kontrolleri.',
        isAvailable: true,
    },
    {
        id: 'd2',
        name: 'Dr. Mehmet Yılmaz',
        title: 'Kardiyoloji Uzmanı',
        specialty: 'Kardiyoloji',
        hospital: 'Medical Park Hastanesi',
        rating: 4.8,
        biography:
            'Kalp ritim bozuklukları ve koruyucu kardiyoloji üzerine çalışmaktadır.',
        isAvailable: true,
    },
    {
        id: 'd3',
        name: 'Dr. Elif Kaya',
        title: 'Dermatoloji Uzmanı',
        specialty: 'Dermatoloji',
        hospital: 'Liv Hospital',
        rating: 4.7,
        biography:
            'Akne, egzama ve estetik dermatoloji alanlarında hizmet verir.',
        isAvailable: true,
    },
];
