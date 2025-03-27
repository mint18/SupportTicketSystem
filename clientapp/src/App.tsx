import { Provider } from 'react-redux';
import { store } from './app/store';
import './App.css';
import LoginForm from './features/auth/components/LoginForm';
import RegisterForm from './features/auth/components/RegisterForm';
import TicketList from './features/tickets/components/TicketList';

function App() {
    return (
        <Provider store={store}>
            <div className="App">
                <TicketList />
                <RegisterForm />  
                <LoginForm />
            </div>
        </Provider>
    );
}

export default App;