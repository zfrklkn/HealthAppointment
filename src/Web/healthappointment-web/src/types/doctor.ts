
export interface Doctor {
    id: string;
    name: string;
    title: string;
    specialty: string;
    hospital: string | null;
    rating: number | null;
    biography: string;
    isActive: boolean;
}
