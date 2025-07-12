import * as React from "react";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

import axiosInstance from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";

const { VerifyTechnician } = API_ENDPOINTS;

export interface viewTechniciantByIdResponse {
	success: boolean;
	message: string;
}

export const verifyTechnician = async (TechnicianId: number): Promise<viewTechniciantByIdResponse> => {
	try {
		const response = await axiosInstance.get(`${VerifyTechnician}?TechnicianId=${TechnicianId}`);
		return response.data;
	} catch (error: any) {
		console.error("Error response from server:", error.response);
		throw new Error("Failed view Employee");
	}
};

export const useVerifyTechnicianMutation = () => {
	const queryClient = useQueryClient();
	const [verifyingId, setVerifyingId] = React.useState<number | null>(null);

	return useMutation({
		mutationFn: (technicianId: number) => verifyTechnician(technicianId),
		onSuccess: () => {
			queryClient.invalidateQueries({ queryKey: ["getAllTechnicians"] }); // adjust to match your fetch key
			setVerifyingId(null);
		},
		onError: () => {
			setVerifyingId(null);
		},
	});
};
