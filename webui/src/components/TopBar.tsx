import { AppBar, Avatar, Box, Button, Container, IconButton, Menu, MenuItem, Toolbar, Tooltip, Typography } from "@mui/material";
import { useState } from 'react';

interface TopBarProps {
    userName: string;
    onLoginClick: () => void;
    onLogout: (userName: string, roles: string[]) => void;
}

export function TopBar(props: TopBarProps) {
    const { userName, onLoginClick, onLogout } = props;

    const [anchorElUser, setAnchorElUser] = useState<null | HTMLElement>(null);

    const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElUser(event.currentTarget);
    };

    const handleCloseUserMenu = () => {
        setAnchorElUser(null);
    };

    const logout = () => {
        localStorage.removeItem('userId');
        localStorage.removeItem('userName');
        localStorage.removeItem('userRoles');
        onLogout('', [] as string[]);
        handleCloseUserMenu();
    }

    return (
        <AppBar position="static">
            <Container maxWidth="xl">
                <Toolbar disableGutters>
                    <Typography
                        variant="h6"
                        sx={{ mr: 2, display: { xs: 'none', md: 'flex' } }}
                    >
                        StrongMindPizzeria
                    </Typography>
                    <Box sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' } }} />
                    <Typography
                        variant="h5"
                        sx={{ mr: 2, display: { xs: 'flex', md: 'none' }, flexGrow: 1 }}
                    >
                        StrongMindPizzeria
                    </Typography>
                    <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }} />
                    <Box sx={{ flexGrow: 0 }}>
                        {userName ? (
                            <Tooltip title="Open settings">
                                <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
                                    <Avatar>{`${userName.split(' ')[0][0]}`}</Avatar>
                                </IconButton>
                            </Tooltip>
                        ) : (
                            <Button color="inherit" onClick={() => onLoginClick()}>Login</Button>
                        )}
                        <Menu
                            sx={{ mt: '45px' }}
                            id="menu-appbar"
                            anchorEl={anchorElUser}
                            anchorOrigin={{
                                vertical: 'top',
                                horizontal: 'right',
                            }}
                            keepMounted
                            transformOrigin={{
                                vertical: 'top',
                                horizontal: 'right',
                            }}
                            open={Boolean(anchorElUser)}
                            onClose={handleCloseUserMenu}
                        >
                            <MenuItem key="Logout" onClick={logout}>
                                <Typography textAlign="center">Logout</Typography>
                            </MenuItem>
                        </Menu>
                    </Box>
                </Toolbar>
            </Container>
        </AppBar>
    );
}