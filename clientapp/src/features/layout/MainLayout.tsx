import { NavLink, Outlet } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import { logout, selectUser } from '../auth/authSlice';

const MainLayout = () => {
 const user = useSelector(selectUser);
 const dispatch = useDispatch();

 const handleLogout = () => {
   dispatch(logout());
 };

 return (
  <div className="flex h-screen bg-dark-bg text-dark-text">
  <div className="w-64 h-screen bg-dark-secondary p-4 border-r border-gray-700">
       <nav className="space-y-2">
         <NavLink 
           to="/account" 
           className={({ isActive }) => `block p-2 rounded ${isActive ? 'bg-accent-gold text-black' : 'hover:bg-gray-700'}`}
         >
           Twoje konto
         </NavLink>
         <NavLink 
           to="/tickets" 
           className={({ isActive }) => `block p-2 rounded ${isActive ? 'bg-accent-gold text-black' : 'hover:bg-gray-700'}`}
         >
           Support
         </NavLink>
         <button 
           onClick={handleLogout} 
           className="w-full text-left p-2 rounded hover:bg-gray-700"
         >
           Wyloguj
         </button>
       </nav>
     </div>
     <main className="flex-1 p-6 bg-dark-bg overflow-auto">
       <Outlet />
     </main>
   </div>
 );
};

export default MainLayout;