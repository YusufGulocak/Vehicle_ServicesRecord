import axios from 'axios';

// Axios instance: tüm istekler bu base URL üzerinden gider
const api = axios.create({
  baseURL: 'https://localhost:7027/api',
});

// Her istekten önce çalışır — localStorage'dan token alıp header'a ekler
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default api;
