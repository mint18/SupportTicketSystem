import client from '../client';
import { LoginCredentials, RegisterCredentials, AuthResponse } from '../types';

export const authService = {
    login: (credentials: LoginCredentials) => 
        client.post<AuthResponse>('/identity/login', credentials)
            .then(r => r.data)
            .catch(err => {
                console.error('Login error:', err.response?.data);
                throw err;
            }),

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