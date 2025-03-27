import client from '../client';
import { TicketResponseDto } from '../types';

export const ticketService = {
    getAll: async () => {
        const response = await client.get<TicketResponseDto[]>('/tickets');
        return response.data;
    },

    getById: async (id: number) => {
        const response = await client.get<TicketResponseDto>(`/tickets/${id}`);
        return response.data;
    },

    create: async (data: { title: string; description: string }) => {
        const response = await client.post<number>('/tickets', data);
        return response.data;
    }
};