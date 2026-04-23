import api from './api';

export const getRecordsByVehicle = (vehicleId) => api.get(`/servicerecord/vehicle/${vehicleId}`);
export const createRecord = (data) => api.post('/servicerecord', data);
export const updateRecord = (id, data) => api.put(`/servicerecord/${id}`, data);
export const deleteRecord = (id) => api.delete(`/servicerecord/${id}`);
