export interface TicketResponseDto {
    id: number;
    title: string;
    description: string;
    createdDate: string;
    statusName?: string;
}

export interface User {
    id: string;
    email: string;
    roles: string[];
 }
 
 export interface AuthState {
    user: User | null;
    token: string | null;
    isAuthenticated: boolean;
 }
 
 export interface LoginCredentials {
    email: string;
    password: string;
 }
 
 export interface RegisterCredentials extends LoginCredentials {
    username?: string;
 }
 
 export interface AuthResponse {
    token: string;
    user: User;
 }