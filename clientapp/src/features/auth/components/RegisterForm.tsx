import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import { authService } from '../../../api/services/authService';
import { login } from '../authSlice';

const RegisterForm: React.FC = () => {
   const [email, setEmail] = useState('');
   const [password, setPassword] = useState('');
   const [username, setUsername] = useState('');
   const dispatch = useDispatch();

   const handleSubmit = async (e: React.FormEvent) => {
       e.preventDefault();
       try {
           const response = await authService.register({ 
               email, 
               password, 
               username 
           });
           dispatch(login({ 
               token: response.token, 
               user: response.user 
           }));
       } catch (error) {
           console.error('Registration failed', error);
       }
   };

   return (
       <form onSubmit={handleSubmit}>
           <input 
               type="text"
               value={username}
               onChange={(e) => setUsername(e.target.value)}
               placeholder="Username"
               required 
           />
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
           <button type="submit">Register</button>
       </form>
   );
};

export default RegisterForm;