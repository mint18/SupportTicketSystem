import client from '../client';
import { LoginCredentials, RegisterCredentials, AuthResponse } from '../types';

export const authService = {
    login: async (credentials: LoginCredentials) => {
        const response = await client.post('/identity/login', credentials);
        const token = response.data.accessToken;
        localStorage.setItem('authToken', token);
        return {
            token: token,
            user: {
                id: 'user-id',
                email: credentials.email,
                roles: []
            }
        };
    },

    register: (credentials: RegisterCredentials) => 
        client.post<AuthResponse>('/identity/register', credentials)
            .then(r => r.data)
            .catch(err => {
                console.error('Register error:', err.response?.data);
                throw err;
            }),

    logout: () => {
        localStorage.removeItem('authToken');
    },

    getToken: () => localStorage.getItem('authToken')
};