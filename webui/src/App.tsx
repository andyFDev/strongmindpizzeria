import { createTheme, ThemeProvider } from '@mui/material';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { useState } from 'react';
import { Dashboard } from './components/Dashboard';
import { LoginModal } from './components/LoginModal';
import { TopBar } from './components/TopBar';

const queryClient = new QueryClient();
const theme = createTheme();

function App() {
    const [openLogin, setOpenLogin] = useState(false);
    const [userName, setUserName] = useState(localStorage.getItem("userName") ?? '');
    const [roles, setRoles] = useState<string[]>(JSON.parse(localStorage.getItem("userRoles") ?? '[]'));

    return (
        <QueryClientProvider client={queryClient}>
            <ThemeProvider theme={theme}>
                <TopBar
                    userName={userName}
                    onLoginClick={() => setOpenLogin(true)}
                    onLogout={(userName, roles) => {
                        setUserName(userName);
                        setRoles(roles);
                    }}
                />
                <Dashboard
                    userName={userName}
                    roles={roles}
                />
                <LoginModal
                    openModal={openLogin}
                    toggleModal={() => setOpenLogin(false)}
                    onLogin={(userName, roles) => {
                        setUserName(userName);
                        setRoles(roles);
                    }}
                />
            </ThemeProvider >
        </QueryClientProvider>
    );
}

export default App;
