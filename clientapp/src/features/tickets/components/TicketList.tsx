import { useEffect, useState } from 'react';
import { ticketService } from '../../../api/services/ticketService';
import { TicketResponseDto } from '../../../api/types';

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

    if (loading) return <div>Loading...</div>;
    if (error) return <div>{error}</div>;

    return (
        <div>
            <h1>Tickets</h1>
            {tickets.length === 0 ? (
                <p>No tickets found</p>
            ) : (
                <ul>
                    {tickets.map((ticket) => (
                        <li key={ticket.id}>
                            <h3>{ticket.title}</h3>
                            <p>{ticket.description}</p>
                            <p>Status: {ticket.statusName || 'No status'}</p>
                            <p>Created: {new Date(ticket.createdDate).toLocaleDateString()}</p>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
};

export default TicketList;