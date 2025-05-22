import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5269/api', // Укажи URL твоего API
});

export const register = async (username, password, email) => {
  const response = await api.post('/User/register', { UserName: username, Password: password, Email: email });
  return response.data;
};

export const login = async (email, password) => {
  const response = await api.post('/User/login', { Email: email, Password: password });
  return response.data;
};

export const getNotes = async (version = 'v1') => {
  const response = await api.get(`/${version}/note/GetAll`);
  return response.data;
};

export const getNoteDetails = async (id, version = 'v1') => {
  const response = await api.get(`/${version}/note/Get/${id}`);
  return response.data;
};

export const createNote = async (note, version = 'v1') => {
  const response = await api.post(`/${version}/note/Create`, note);
  return response.data;
};

export const updateNote = async (id, note, version = 'v1') => {
  const response = await api.put(`/${version}/note/Update`, { ...note, Id: id });
  return response.data;
};

export const deleteNote = async (id, version = 'v1') => {
  await api.delete(`/${version}/note/Delete/${id}`);
};