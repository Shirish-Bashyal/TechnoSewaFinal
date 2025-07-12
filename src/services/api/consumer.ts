import AsyncStorage from "@react-native-async-storage/async-storage";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

import axiosInstance from "../axiosInstance";
import { API_ENDPOINTS } from "../endPoints";

const { GetAllConsumer } = API_ENDPOINTS;

export interface viewConsumerData {
	id: string;
	phone: string;
	name: string;
	address: string;
}

export interface viewAllConsumerResponse {
	success: boolean;
	message: string;
	data?: viewConsumerData[];
}

export const showAllConsumerData = async (): Promise<viewAllConsumerResponse> => {
	try {
		const response = await axiosInstance.get(GetAllConsumer);
		const Content = await response.data;
		return Content;
	} catch (error) {
		throw new Error("Failed to fetch  data");
	}
};

export const useShowAllConsumerData = () => {
	return useQuery<viewAllConsumerResponse, Error>({
		queryKey: ["ViewDataAllConsumer"],
		queryFn: showAllConsumerData,
	});
};
