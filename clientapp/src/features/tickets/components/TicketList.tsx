import { useEffect, useState } from 'react';
import { ticketService } from '../../../api/services/ticketService';
import { TicketResponseDto } from '../../../api/types';
import { Link } from 'react-router-dom';

const TicketList = () => {
    const [tickets, setTickets] = useState<TicketResponseDto[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchTickets = async () => {
            try {
                const data = await ticketService.getAll();
                setTickets(data);
                setLoading(false);
            } catch (err) {
                console.error(err);
                setError('Failed to fetch tickets');
                setLoading(false);
            }
        };

        fetchTickets();
    }, []);

    if (loading) return <div>Ładowanie...</div>;
    if (error) return <div className="text-red-500">{error}</div>;

    return (
        <div>
            <div className="flex justify-between items-center mb-6">
                <h1 className="text-2xl font-bold">Zgłoszenia</h1>
                <Link 
                    to="/tickets/create" 
                    className="bg-accent-gold text-black px-4 py-2 rounded"
                >
                    Nowe zgłoszenie
                </Link>
            </div>
            
            {tickets.length === 0 ? (
                <p>Brak zgłoszeń</p>
            ) : (
                <div className="grid gap-4">
                    {tickets.map((ticket) => (
                        <div 
                            key={ticket.id} 
                            className="bg-dark-secondary p-4 rounded border border-gray-700"
                        >
                            <Link to={`/tickets/${ticket.id}`} className="block">
                                <h3 className="text-xl font-semibold">{ticket.title}</h3>
                                <div className="flex justify-between mt-2">
                                    <span className="text-gray-400">
                                        Status: {ticket.statusName || 'Brak'}
                                    </span>
                                    <span className="text-gray-400">
                                        {new Date(ticket.createdDate).toLocaleDateString()}
                                    </span>
                                </div>
                                {ticket.assignedToName && (
                                    <div className="mt-2 text-green-400">
                                        Przypisane do: {ticket.assignedToName}
                                    </div>
                                )}
                            </Link>
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
};

export default TicketList;