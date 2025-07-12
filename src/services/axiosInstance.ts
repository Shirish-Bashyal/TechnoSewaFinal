import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';

// export const baseURL = "https://62cf5cc96ebc.ngrok-free.app";
// const baseURL = "https://localhost:44335";
export const baseURL = "https://localhost:7206";
// const baseURL = "https://192.168.1.71:44335"
// const baseURL = "https://10.0.2.2:44335"
 


const axiosInstance = axios.create({
  baseURL: baseURL,
  // headers: { "Content-Type": "application/json"},
});

axiosInstance.interceptors.request.use(async (config) => {
  const token = await AsyncStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default axiosInstance