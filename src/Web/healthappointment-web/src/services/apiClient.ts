import axios from 'axios';

console.log(
    'API Base URL:',
    import.meta.env.VITE_API_BASE_URL
);

export const apiClient = axios.create({
    baseURL: import.meta.env.VITE_API_BASE_URL,
    timeout: 15000,
    headers: {
        'Content-Type': 'application/json',
    },
});