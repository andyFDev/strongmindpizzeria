import axios from "axios";

export const apiClient = axios.create({
    baseURL: process.env.REACT_APP_BASEAPIURL,
    headers: { user: localStorage.getItem("userId") },
    timeout: 1000
});

apiClient.interceptors.request.use((config) => {
    const userId = localStorage.getItem('userId');
    if (userId) {
        config.headers['User'] = userId;
    }
    return config;
},
    (error) => Promise.reject(error)
)