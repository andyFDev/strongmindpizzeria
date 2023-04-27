import { Button, Dialog, DialogActions, DialogContent, DialogTitle, TextField } from '@mui/material';
import { useState } from "react";
import { apiClient } from '../utilities/apiClient';

interface LoginModalProps {
    openModal: boolean;
    toggleModal: () => void;
    onLogin: (userName: string, roles: string[]) => void;
}

interface User {
    id?: string;
    name?: string;
    roles?: string[];
}

export function LoginModal(props: LoginModalProps) {
    const { openModal, toggleModal, onLogin } = props;

    const [name, setName] = useState('');

    const close = () => {
        setName('');
        toggleModal();
    };

    const onClickSubmitLogin = () => {
        apiClient.get<User>('users', { params: { name: name } })
            .then(resp => {
                if (resp.status === 200) {
                    localStorage.setItem('userId', resp.data?.id as string);
                    localStorage.setItem('userName', resp.data?.name as string);
                    localStorage.setItem("userRoles", JSON.stringify(resp.data?.roles as string[]));
                    onLogin(resp.data?.name as string, resp.data?.roles as string[])
                }
                else {
                    localStorage.removeItem('userId');
                    localStorage.removeItem('userName');
                    localStorage.removeItem('userRoles');
                    onLogin('', [] as string[])
                }
            })
            .catch(err => {
                localStorage.removeItem('userId');
                localStorage.removeItem('userName');
                localStorage.removeItem('userRoles');
                onLogin('', [] as string[])
            });

        close();
    }

    return (
        <Dialog open={openModal} onClose={close}>
            <DialogTitle>Login</DialogTitle>
            <DialogContent>
                <TextField
                    required
                    label="Username"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    variant="standard"
                />
            </DialogContent>
            <DialogActions>
                <Button
                    variant="contained"
                    onClick={onClickSubmitLogin}
                    disabled={name === ""}
                >
                    LOGIN
                </Button>
            </DialogActions>
        </Dialog>
    );
}