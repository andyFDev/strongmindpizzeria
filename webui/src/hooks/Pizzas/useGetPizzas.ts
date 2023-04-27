import { useQuery } from "@tanstack/react-query";
import { apiClient } from "../../utilities/apiClient";

interface Pizzas {
    id?: string;
    name?: string;
    toppings?: string[];
    createdDate?: Date;
    createdBy?: string;
}

export const useGetPizzas = (userName: string) => {
    const { isLoading, isError, isSuccess, data } = useQuery({
        queryKey: ['pizzas'],
        queryFn: async () => {
            const { data } = await apiClient.get<Pizzas[]>('pizzas');
            return data;
        },
        enabled: !!userName,
    });

    return {
        gettingPizzas: isLoading,
        errorGettingPizzas: isError,
        gotPizzas: isSuccess,
        pizzas: data
    }
};