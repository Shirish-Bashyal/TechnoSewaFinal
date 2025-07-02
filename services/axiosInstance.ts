import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';

const baseURL = "https://22da-2400-1a00-bb20-1efe-b169-373b-37dc-2181.ngrok-free.app";
// const baseURL = "https://localhost:44335";
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



// export async function fetchWithAuth(
//   endpoint: string,
//   options: RequestInit = {}
// ) {
//   try {
//     const token = await AsyncStorage.getItem('token');

//     // Get existing headers or create empty object
//     const existingHeaders = options.headers || {};

//     // Compose headers, merge existing and Authorization header if token present
//     const headers = {
//       'content-type': 'application/json',
//       ...existingHeaders,
//       ...(token ? { Authorization: `Bearer ${token}` } : {}),
//     };

//     const url = baseURL + endpoint;

//     const response = await fetch(url, {
//       ...options,
//       headers,
//     });

//     if (!response.ok) {
//       const errorBody = await response.json().catch(() => ({}));
//       const message = errorBody.message || 'Request failed';
//       throw new Error(message);
//     }

//     return await response.json();
//   } catch (error) {
//     throw error;
//   }
// }