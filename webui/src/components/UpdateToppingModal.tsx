import { Button, Dialog, DialogContent, Stack, TextField } from '@mui/material';
import { useUpdateTopping } from '../hooks/Toppings/useUpdateTopping';

interface UpdateToppingModalProps {
    openModal: boolean;
    toggleModal: () => void;
    toUpdateToppingId: string;
    setToUpdateToppingId: (toppingId: string) => void;
    toUpdateToppingName: string;
    setToUpdateToppingName: (toppingName: string) => void;
}

export function UpdateToppingModal(props: UpdateToppingModalProps) {

    const { updateTopping } = useUpdateTopping();

    const onClose = () => {
        props.setToUpdateToppingId('');
        props.setToUpdateToppingName('');
        props.toggleModal();
    }

    const onSubmitUpdate = () => {
        updateTopping({ id: props.toUpdateToppingId, name: props.toUpdateToppingName });
        onClose();
    }

    return (
        <Dialog open={props.openModal} onClose={onClose}>
            <DialogContent>
                <Stack direction="row" p={0.75} spacing={1}>
                    <TextField
                        required
                        label="Topping"
                        variant="outlined"
                        defaultValue={props.toUpdateToppingName}
                        onChange={(e) => props.setToUpdateToppingName(e.target.value)}
                    />
                    <Button
                        onClick={onSubmitUpdate}
                        variant="contained"
                        disabled={props.toUpdateToppingName === ""}
                    >
                        UPDATE
                    </Button>
                </Stack>
            </DialogContent>
        </Dialog>
    );
}