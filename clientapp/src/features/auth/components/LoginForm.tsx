import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { authService } from '../../../api/services/authService';
import { login } from '../authSlice';

const LoginForm: React.FC = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError(null);
        setLoading(true);

        try {
            const response = await authService.login({ email, password });
            localStorage.setItem('authToken', response.token);
            dispatch(login({
                token: response.token,
                user: response.user
            }));
            navigate('/tickets');
        } catch (err: any) {
            setError(err.response?.data?.message || 'Nieprawidłowy email lub hasło');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="flex min-h-screen items-center justify-center bg-dark-bg p-4">
            <div className="w-full max-w-md rounded-lg bg-dark-secondary p-6 shadow-md">
                <h2 className="mb-6 text-center text-2xl font-bold text-white">Logowanie</h2>

                {error && (
                    <div className="mb-4 rounded-md bg-red-500/20 p-3 text-red-300">
                        {error}
                    </div>
                )}

                <form onSubmit={handleSubmit} className="space-y-4">
                    <div>
                        <label htmlFor="email" className="mb-1 block text-sm font-medium text-gray-300">
                            Email
                        </label>
                        <input
                            id="email"
                            type="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            className="w-full rounded-md border border-gray-700 bg-dark-bg p-2 text-white"
                            required
                        />
                    </div>

                    <div>
                        <label htmlFor="password" className="mb-1 block text-sm font-medium text-gray-300">
                            Hasło
                        </label>
                        <input
                            id="password"
                            type="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            className="w-full rounded-md border border-gray-700 bg-dark-bg p-2 text-white"
                            required
                        />
                    </div>

                    <button
                        type="submit"
                        disabled={loading}
                        className="w-full rounded-md bg-accent-gold p-2 font-medium text-black transition hover:bg-amber-400 disabled:opacity-50"
                    >
                        {loading ? 'Logowanie...' : 'Zaloguj się'}
                    </button>
                </form>

                <p className="mt-4 text-center text-sm text-gray-400">
                    Nie masz jeszcze konta?{' '}
                    <a href="/register" className="text-accent-gold hover:underline">
                        Zarejestruj się
                    </a>
                </p>
            </div>
        </div>
    );
};

export default LoginForm;