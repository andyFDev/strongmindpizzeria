import { useMutation, useQueryClient } from "@tanstack/react-query";
import { apiClient } from "../../utilities/apiClient";

interface ToUpdateTopping {
    id?: string;
    name?: string;
}

export const useUpdateTopping = () => {
    const queryClient = useQueryClient();
    const { isLoading, mutate } = useMutation({
        mutationFn: (toUpdateTopping: ToUpdateTopping) => apiClient.put(`toppings/${toUpdateTopping.id}`, toUpdateTopping),
        onSettled: () => {
            queryClient.invalidateQueries({ queryKey: ['toppings'] })
            queryClient.invalidateQueries({ queryKey: ['pizzas'] })
        }
    });

    return {
        updatingTopping: isLoading,
        updateTopping: mutate
    }
};