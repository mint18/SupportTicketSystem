// src/features/auth/authSlice.ts
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { AuthState, AuthResponse } from '../../api/types';

const initialState: AuthState = {
    user: null,
    token: localStorage.getItem('authToken'),
    isAuthenticated: !!localStorage.getItem('authToken')
  };

const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        login: (state, action: PayloadAction<AuthResponse>) => {
            state.token = action.payload.token;
            state.user = action.payload.user;
            state.isAuthenticated = true;
            try {
                localStorage.setItem('authToken', action.payload.token);
            } catch {}
        },
        logout: (state) => {
            state.token = null;
            state.user = null;
            state.isAuthenticated = false;
            try {
                localStorage.removeItem('authToken');
            } catch {}
        }
    }
});

export const { login, logout } = authSlice.actions;
export const selectUser = (state: { auth: AuthState }) => state.auth.user;
export const selectIsAuthenticated = (state: { auth: AuthState }) => state.auth.isAuthenticated;
export const selectToken = (state: { auth: AuthState }) => state.auth.token;
export default authSlice.reducer;