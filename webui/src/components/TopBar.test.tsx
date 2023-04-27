import { QueryClient } from '@tanstack/query-core';
import { QueryClientProvider } from '@tanstack/react-query';
import { render, screen } from '@testing-library/react';
import { TopBar } from './TopBar';

test('should render the avatar button', () => {
    const queryClient = new QueryClient();
    render(
        <QueryClientProvider client={queryClient}>
            <TopBar
                userName={"UserTest"}
                onLoginClick={() => { }}
                onLogout={() => { }}
            />
        </QueryClientProvider>
    );

    const searchCustomerField = screen.getByTestId('avatarButton');
    expect(searchCustomerField).toBeInTheDocument();
});