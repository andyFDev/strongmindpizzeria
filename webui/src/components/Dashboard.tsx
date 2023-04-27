import DeleteIcon from '@mui/icons-material/Delete';
import LocalPizzaIcon from '@mui/icons-material/LocalPizza';
import { Autocomplete, Box, Button, CircularProgress, Container, IconButton, Stack, TextField } from '@mui/material';
import { DataGrid, GridCellParams, GridColDef } from '@mui/x-data-grid';
import { useState } from 'react';
import { useAddPizza } from '../hooks/Pizzas/useAddPizza';
import { useDeletePizza } from '../hooks/Pizzas/useDeletePizza';
import { useGetPizzas } from '../hooks/Pizzas/useGetPizzas';
import { useGetToppings } from '../hooks/Toppings/useGetToppings';
import { ToppingsModal } from './ToppingsModal';
import { UpdatePizzaModal } from './UpdatePizzaModal';

interface DashboardProps {
    userName: string;
    roles: string[];
}

export function Dashboard(props: DashboardProps) {
    const [tablePagination, setTablePagination] = useState({ pageSize: 10, page: 0 });

    const { deletePizza } = useDeletePizza();
    const { addPizza } = useAddPizza();
    const { gettingPizzas, errorGettingPizzas, gotPizzas, pizzas } = useGetPizzas(props.userName);
    const { toppings } = useGetToppings(props.userName);

    const [openPizzaModal, setOpenPizzaModal] = useState(false);
    const [openToppingsModal, setopenToppingsModal] = useState(false);

    const [toAddPizzaName, setToAddPizzaName] = useState('');
    const [toAddPizzaToppings, setToAddPizzaToppings] = useState([] as string[]);

    const [toUpdatePizzaId, setToUpdatePizzaId] = useState('');
    const [toUpdatePizzaName, setToUpdatePizzaName] = useState('');
    const [toUpdatePizzaToppings, setToUpdatePizzaToppings] = useState([] as string[]);

    const columns: GridColDef[] = [
        { field: 'name', headerName: 'Name', maxWidth: 200, flex: 1, },
        { field: 'toppings', headerName: 'Toppings', flex: 1, },
        {
            field: 'delete', headerName: '', headerAlign: 'center', maxWidth: 100, align: 'center', flex: 1, renderCell: (params) => {
                return (
                    <IconButton onClick={(e) => deletePizza({ id: params.id as string })}>
                        <DeleteIcon />
                    </IconButton>
                );
            }
        }
    ];

    const tooManyPizzas = () => pizzas && pizzas.length >= 20;

    const onClickTableRow = (params: GridCellParams) => {
        if (params.colDef.field === "delete") {
            return;
        }

        setToUpdatePizzaId(params.id as string);
        setToUpdatePizzaName(params.row.name);
        setToUpdatePizzaToppings(params.row.toppings);
        setOpenPizzaModal(true);
    }

    return (
        <Box component="main">
            {props.userName &&
                <Container sx={{ mt: 4 }}>
                    <Stack direction={{ xs: 'column', md: 'row' }} p={1} spacing={1}>
                        <TextField
                            value={toAddPizzaName}
                            required
                            label="Pizza Name"
                            variant="outlined"
                            onChange={(e) => setToAddPizzaName(e.target.value)}
                        />
                        <Autocomplete
                            sx={{ minWidth: 150, flexGrow: 1 }}
                            multiple
                            id="tags-standard"
                            options={toppings?.map(x => x.name as string) ?? [] as string[]}
                            value={toAddPizzaToppings}
                            onChange={(e, value) => setToAddPizzaToppings(value)}
                            renderInput={(params) => (
                                <TextField
                                    {...params}
                                    required
                                    label="Toppings"
                                />
                            )}
                        />
                        <Button
                            sx={{ minWidth: 100 }}
                            onClick={() => {
                                addPizza({ name: toAddPizzaName, toppings: toAddPizzaToppings });
                                setToAddPizzaName('');
                                setToAddPizzaToppings([] as string[]);
                            }}
                            variant="contained"
                            disabled={toAddPizzaName === "" || toAddPizzaToppings === undefined || toAddPizzaToppings.length === 0 || tooManyPizzas()}
                        >
                            {tooManyPizzas() ? '20 Max' : 'Add'}
                        </Button>
                    </Stack>
                    {props.userName && gettingPizzas &&
                        <Stack sx={{ mt: 4 }} display="flex" justifyContent="center" flexDirection="row">
                            <CircularProgress />
                        </Stack>
                    }
                    {errorGettingPizzas && 'An error has occurred'}
                    {props.roles.some(x => x === 'ToppingUser') && gotPizzas &&
                        <DataGrid
                            sx={{ "&.MuiDataGrid-root .MuiDataGrid-cell:focus-within": { outline: "none !important" } }}
                            className="MuiDataGrid-root"
                            autoHeight
                            loading={gettingPizzas}
                            getRowId={(row) => row.id}
                            rows={pizzas!}
                            columns={columns}
                            paginationModel={tablePagination}
                            onPaginationModelChange={setTablePagination}
                            pageSizeOptions={[5]}
                            onCellClick={onClickTableRow}
                            disableRowSelectionOnClick
                        />
                    }
                    <Stack sx={{ mt: 4 }} display="flex" justifyContent="center" flexDirection="row">
                        <Button
                            onClick={() => setopenToppingsModal(true)}
                            variant="outlined"
                            startIcon={<LocalPizzaIcon />}
                            disabled={!props.roles.some(x => x === 'ToppingManager')}
                        >
                            Toppings
                        </Button>
                    </Stack>
                </Container>
            }
            <UpdatePizzaModal
                openModal={openPizzaModal}
                toggleModal={() => setOpenPizzaModal(false)}
                toUpdatePizzaId={toUpdatePizzaId}
                setToUpdatePizzaId={(pizzaId) => setToUpdatePizzaId(pizzaId)}
                toUpdatePizzaName={toUpdatePizzaName}
                setToUpdatePizzaName={(pizzaName) => setToUpdatePizzaName(pizzaName)}
                toUpdatePizzaToppings={toUpdatePizzaToppings}
                setToUpdatePizzaToppings={(pizzaToppings) => setToUpdatePizzaToppings(pizzaToppings)}
                allPizzaToppings={toppings?.map(x => x.name as string) ?? [] as string[]}
            />
            <ToppingsModal
                userName={props.userName}
                openModal={openToppingsModal}
                toggleModal={() => setopenToppingsModal(false)}
            />
        </Box>
    );
}