import axios from 'axios';

const api = axios.create({
  baseURL: 'https://notes-4g7h.onrender.com',
  withCredentials: true,
});

export const register = async (username, password, email) => {
  const response = await api.post('/api/v1/User/register', { UserName: username, Password: password, Email: email });
  return response.data;
};

export const login = async (email, password) => {
  const response = await api.post('/api/v1/User/login', { Email: email, Password: password });
  if (response.status === 200) {
    return response;
  }
  throw new Error('Login failed with status: ' + response.status);
};

export const getNotes = async (version = 'v1') => {
  const response = await api.get(`/api/${version}/note/GetAll`);
  return response.data;
};

export const getNoteDetails = async (id, version = 'v1') => {
  const response = await api.get(`/api/${version}/note/Get/${id}`);
  return response.data;
};

export const createNote = async (note, version = 'v1') => {
  const response = await api.post(`/api/${version}/note/Create`, note);
  return response.data;
};

export const updateNote = async (id, note, version = 'v1') => {
  const response = await api.put(`/api/${version}/note/Update`, { ...note, Id: id });
  return response.data;
};

export const deleteNote = async (id, version = 'v1') => {
  await api.delete(`/api/${version}/note/Delete/${id}`);
};