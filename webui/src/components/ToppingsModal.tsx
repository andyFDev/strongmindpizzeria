import DeleteIcon from '@mui/icons-material/Delete';
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, IconButton, List, ListItem, ListItemButton, ListItemText, Stack, TextField } from '@mui/material';
import { useState } from "react";
import { useAddTopping } from '../hooks/Toppings/useAddTopping';
import { useDeleteTopping } from '../hooks/Toppings/useDeleteTopping';
import { useGetToppings } from '../hooks/Toppings/useGetToppings';
import { UpdateToppingModal } from './UpdateToppingModal';

interface ToppingsModalProps {
    openModal: boolean;
    toggleModal: () => void;
    userName: string;
}

export function ToppingsModal(props: ToppingsModalProps) {
    const { openModal, toggleModal } = props;

    const { addTopping } = useAddTopping();
    const { deleteTopping } = useDeleteTopping();
    const { gettingToppings, gotToppings, toppings } = useGetToppings(props.userName);

    const [openUpdateToppingsModal, setUpdateOpenToppingsModal] = useState(false);

    const [toAddToppingName, setToAddToppingName] = useState('');

    const [toUpdateToppingId, setToUpdateToppingId] = useState('');
    const [toUpdateToppingName, setToUpdateToppingName] = useState('');

    const tooManyToppings = () => toppings && toppings.length >= 20;

    const onSubmitAddTopping = () => {
        addTopping({ name: toAddToppingName });
        setToAddToppingName('');
    }

    return (
        <Dialog open={openModal} onClose={toggleModal}>
            <DialogTitle>Manage Toppings</DialogTitle>
            <DialogContent sx={{ pb: 0, overflowY: "revert" }}>
                <Stack p={0.75} spacing={1}>
                    <TextField
                        error={tooManyToppings()}
                        value={toAddToppingName}
                        label="New Topping"
                        onChange={(e) => setToAddToppingName(e.target.value)}
                        helperText="Max 20 entries"
                    />
                    <Button
                        variant="contained"
                        disabled={toAddToppingName === "" || (tooManyToppings())}
                        onClick={onSubmitAddTopping}
                    >
                        ADD TOPPING
                    </Button>
                </Stack>
            </DialogContent>
            <DialogContent sx={{ pt: 0, pb: 0 }}>
                <List dense>
                    {gettingToppings && "Loading..."}
                    {gotToppings && toppings!.map((data) => {
                        return (
                            <ListItem
                                secondaryAction={
                                    <IconButton
                                        onClick={() => deleteTopping({ id: data.id })}
                                        edge="end"
                                        aria-label="delete"
                                    >
                                        <DeleteIcon />
                                    </IconButton>
                                }
                            >
                                <ListItemButton onClick={() => {
                                    setToUpdateToppingId(data.id ?? "0");
                                    setToUpdateToppingName(data.name ?? "0");
                                    setUpdateOpenToppingsModal(true);
                                }}>
                                    <ListItemText id={data.id} primary={data.name} />
                                </ListItemButton>
                            </ListItem>
                        );
                    })}
                </List>
            </DialogContent>
            <DialogActions>
                <Button onClick={toggleModal}>CLOSE</Button>
            </DialogActions>
            <UpdateToppingModal
                openModal={openUpdateToppingsModal}
                toggleModal={() => setUpdateOpenToppingsModal(false)}
                toUpdateToppingId={toUpdateToppingId}
                setToUpdateToppingId={(toppingId) => setToUpdateToppingId(toppingId)}
                toUpdateToppingName={toUpdateToppingName}
                setToUpdateToppingName={(toppingName) => setToUpdateToppingName(toppingName)}
            />
        </Dialog>
    );
}