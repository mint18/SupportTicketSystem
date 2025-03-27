// src/features/auth/LoginForm.tsx
import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import { authService } from '../../../api/services/authService';
import { login } from '../authSlice';

const LoginForm: React.FC = () => {
   const [email, setEmail] = useState('');
   const [password, setPassword] = useState('');
   const dispatch = useDispatch();

   const handleSubmit = async (e: React.FormEvent) => {
       e.preventDefault();
       try {
           const response = await authService.login({ email, password });
           dispatch(login({ 
               token: response.token, 
               user: response.user 
           }));
       } catch (error) {
           console.error('Login failed', error);
       }
   };

   return (
       <form onSubmit={handleSubmit}>
           <input 
               type="email" 
               value={email}
               onChange={(e) => setEmail(e.target.value)}
               placeholder="Email"
               required 
           />
           <input 
               type="password"
               value={password}
               onChange={(e) => setPassword(e.target.value)}
               placeholder="Password"
               required 
           />
           <button type="submit">Login</button>
       </form>
   );
};

export default LoginForm;