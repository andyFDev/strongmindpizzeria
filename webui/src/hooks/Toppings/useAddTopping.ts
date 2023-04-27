import { useMutation, useQueryClient } from "@tanstack/react-query";
import { apiClient } from "../../utilities/apiClient";

interface NewTopping {
    name?: string;
}

export const useAddTopping = () => {
    const queryClient = useQueryClient();
    const { isLoading, mutate } = useMutation({
        mutationFn: (newTopping: NewTopping) => apiClient.post('toppings', newTopping),
        onSettled: () => {
            queryClient.invalidateQueries({ queryKey: ['toppings'] })
            queryClient.invalidateQueries({ queryKey: ['pizzas'] })
        }
    });

    return {
        addingTopping: isLoading,
        addTopping: mutate
    }
};