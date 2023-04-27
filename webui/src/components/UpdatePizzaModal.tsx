import { Autocomplete, Button, Dialog, DialogContent, Stack, TextField } from '@mui/material';
import { useUpdatePizza } from '../hooks/Pizzas/useUpdatePizza';

interface UpdatePizzaModalProps {
    openModal: boolean;
    toggleModal: () => void;
    toUpdatePizzaId: string;
    setToUpdatePizzaId: (pizzaId: string) => void;
    toUpdatePizzaName: string;
    setToUpdatePizzaName: (pizzaName: string) => void;
    toUpdatePizzaToppings: string[];
    setToUpdatePizzaToppings: (pizzaToppings: string[]) => void;
    allPizzaToppings?: string[];
}

export function UpdatePizzaModal(props: UpdatePizzaModalProps) {

    const { updatePizza } = useUpdatePizza();

    const onClose = () => {
        props.setToUpdatePizzaId('');
        props.setToUpdatePizzaName('');
        props.setToUpdatePizzaToppings([] as string[]);
        props.toggleModal();
    }

    const onSubmitUpdate = () => {
        updatePizza({ id: props.toUpdatePizzaId, name: props.toUpdatePizzaName, toppings: props.toUpdatePizzaToppings });
        onClose();
    }

    return (
        <Dialog open={props.openModal} onClose={onClose}>
            <DialogContent>
                <Stack p={0.75} spacing={1}>
                    <TextField
                        required
                        label="Pizza Name"
                        variant="outlined"
                        defaultValue={props.toUpdatePizzaName}
                        onChange={(e) => props.setToUpdatePizzaName(e.target.value)}
                    />
                    <Autocomplete
                        multiple
                        id="tags-standard"
                        options={props.allPizzaToppings ?? [] as string[]}
                        defaultValue={props.toUpdatePizzaToppings}
                        onChange={(e, value) => props.setToUpdatePizzaToppings(value)}
                        renderInput={(params) => (
                            <TextField
                                {...params}
                                required
                                label="Toppings"
                            />
                        )}
                    />
                    <Button
                        onClick={onSubmitUpdate}
                        variant="contained"
                        disabled={
                            props.toUpdatePizzaName === ""
                            || props.toUpdatePizzaToppings === undefined
                            || props.toUpdatePizzaToppings.length == 0}
                    >
                        UPDATE
                    </Button>
                </Stack>
            </DialogContent>
        </Dialog>
    );
}