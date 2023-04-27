import { useMutation, useQueryClient } from "@tanstack/react-query";
import { apiClient } from "../../utilities/apiClient";

interface ToUpdatePizza {
    id?: string;
    name?: string;
    toppings?: string[];
}

export const useUpdatePizza = () => {
    const queryClient = useQueryClient();
    const { isLoading, mutate } = useMutation({
        mutationFn: (toUpdatePizza: ToUpdatePizza) => apiClient.put(`pizzas/${toUpdatePizza.id}`, toUpdatePizza),
        onSettled: () => {
            queryClient.invalidateQueries({ queryKey: ['pizzas'] })
        }
    });

    return {
        updatingPizza: isLoading,
        updatePizza: mutate
    }
};