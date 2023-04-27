import { useQuery } from "@tanstack/react-query";
import { apiClient } from "../../utilities/apiClient";

interface Topping {
    id?: string;
    name?: string;
    createdDate?: Date;
    createdBy?: string;
}

export const useGetToppings = (userName: string) => {
    const { isLoading, isError, isSuccess, data } = useQuery({
        queryKey: ['toppings'],
        queryFn: async () => {
            const { data } = await apiClient.get<Topping[]>('toppings');
            return data;
        },
        enabled: !!userName,
    });

    return {
        gettingToppings: isLoading,
        errorGettingToppings: isError,
        gotToppings: isSuccess,
        toppings: data
    }
};