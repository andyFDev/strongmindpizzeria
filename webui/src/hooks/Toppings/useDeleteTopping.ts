import { useMutation, useQueryClient } from "@tanstack/react-query";
import { apiClient } from "../../utilities/apiClient";

interface ToDeleteTopping {
    id?: string;
}

export const useDeleteTopping = () => {
    const queryClient = useQueryClient();
    const { isLoading, mutate } = useMutation({
        mutationFn: (toDeleteTopping: ToDeleteTopping) => apiClient.delete(`toppings/${toDeleteTopping.id}`),
        onSettled: () => {
            queryClient.invalidateQueries({ queryKey: ['toppings'] })
            queryClient.invalidateQueries({ queryKey: ['pizzas'] })
        }
    });

    return {
        deletingTopping: isLoading,
        deleteTopping: mutate
    }
};