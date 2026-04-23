import api from './api';

export const login = async (userName, passwordHash) => {
  const response = await api.post('/auth/login', { userName, passwordHash });
  return response.data; // { token: "..." }
};

export const register = async (userName, passwordHash, email) => {
  const response = await api.post('/auth/register', { userName, passwordHash, email });
  return response.data;
};
