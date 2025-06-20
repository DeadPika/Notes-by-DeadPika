import axios from 'axios';

const api = axios.create({
  baseURL: 'https://notes-4g7h.onrender.com/api',
  withCredentials: true, // Для поддержки кросс-доменных куки (хотя теперь не нужно для токена)
});

export const register = async (username, password, email) => {
  const response = await api.post('/v1/User/register', { UserName: username, Password: password, Email: email });
  return response.data;
};

export const login = async (email, password) => {
  const response = await api.post('/v1/User/login', { Email: email, Password: password });
  if (response.status === 200) {
    return response;
  }
  throw new Error('Login failed with status: ' + response.status);
};

export const getNotes = async (version = 'v1') => {
  try {
    const response = await api.get(`/${version}/note/GetAll`, {
      headers: { Authorization: `Bearer ${localStorage.getItem('token')}` }
    });
    return response.data; // Возвращает { notes: [...] }
  } catch (error) {
    console.error('API Error:', error.response?.data || error.message);
    throw error;
  }
};

// Остальные методы (getNoteDetails, createNote и т.д.) обновите аналогично
export const getNoteDetails = async (id, version = 'v1') => {
  const response = await api.get(`/${version}/note/Get/${id}`);
  return response.data;
};

export const createNote = async (noteData) => {
  const response = await api.post('/v1/note/Create', noteData, {
    headers: { Authorization: `Bearer ${localStorage.getItem('token')}` }
  });
  return response;
};

export const updateNote = async (id, note, version = 'v1') => {
  const response = await api.put(`/${version}/note/Update`, { ...note, Id: id });
  return response.data;
};

export const deleteNote = async (id, version = 'v1') => {
  await api.delete(`/${version}/note/Delete/${id}`);
};