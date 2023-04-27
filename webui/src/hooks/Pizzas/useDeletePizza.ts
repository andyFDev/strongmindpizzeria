import { useMutation, useQueryClient } from "@tanstack/react-query";
import { apiClient } from "../../utilities/apiClient";

interface ToDeletePizza {
    id?: string;
}

export const useDeletePizza = () => {
    const queryClient = useQueryClient();
    const { isLoading, mutate } = useMutation({
        mutationFn: (toDeletePizza: ToDeletePizza) => apiClient.delete(`pizzas/${toDeletePizza.id}`),
        onSettled: () => {
            queryClient.invalidateQueries({ queryKey: ['pizzas'] })
        }
    });

    return {
        deletingPizza: isLoading,
        deletePizza: mutate
    }
};