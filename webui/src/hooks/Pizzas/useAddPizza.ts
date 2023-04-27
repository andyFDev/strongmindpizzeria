import { useMutation, useQueryClient } from "@tanstack/react-query";
import { apiClient } from "../../utilities/apiClient";

interface NewPizza {
    name?: string;
    toppings?: string[];
}

export const useAddPizza = () => {
    const queryClient = useQueryClient();
    const { isLoading, mutate } = useMutation({
        mutationFn: (newPizza: NewPizza) => apiClient.post('pizzas', newPizza),
        onSettled: () => {
            queryClient.invalidateQueries({ queryKey: ['pizzas'] })
        }
    });

    return {
        addingPizza: isLoading,
        addPizza: mutate
    }
};