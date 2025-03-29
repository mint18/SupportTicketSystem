import { Provider } from 'react-redux';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { store } from './app/store';
import './App.css';
import MainLayout from './features/layout/MainLayout';
import ProtectedRoute from './features/common/ProtectedRoute';
import LoginForm from './features/auth/components/LoginForm';
import RegisterForm from './features/auth/components/RegisterForm';

// Tymczasowe komponenty zastępcze - zaimplementujemy je później
const TicketsList = () => <div>Lista zgłoszeń</div>;
const TicketDetails = () => <div>Szczegóły zgłoszenia</div>;
const CreateTicket = () => <div>Utwórz zgłoszenie</div>;
const UserAccount = () => <div>Twoje konto</div>;

function App() {
  return (
    <Provider store={store}>
      <BrowserRouter>
        <Routes>
          <Route path="/login" element={<LoginForm />} />
          <Route path="/register" element={<RegisterForm />} />
          
          <Route element={<ProtectedRoute />}>
            <Route element={<MainLayout />}>
              <Route path="/tickets" element={<TicketsList />} />
              <Route path="/tickets/:id" element={<TicketDetails />} />
              <Route path="/tickets/create" element={<CreateTicket />} />
              <Route path="/account" element={<UserAccount />} />
              <Route path="/" element={<Navigate to="/tickets" replace />} />
            </Route>
          </Route>
        </Routes>
      </BrowserRouter>
    </Provider>
  );
}

export default App;