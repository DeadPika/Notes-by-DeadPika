import { createContext, useContext, useState, useEffect } from 'react';
import { login, register } from '../api/api';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [token, setToken] = useState('');

  // Загружаем токен из localStorage при монтировании
  useEffect(() => {
    const savedToken = localStorage.getItem('token');
    if (savedToken) {
      setToken(savedToken);
    }
  }, []);

  const signIn = async (email, password) => {
    try {
      const response = await login(email, password);
      if (response.status === 200) {
        const { token } = response.data;
        localStorage.setItem('token', token); // Сохраняем в localStorage
        setToken(token); // Обновляем состояние
        return true;
      }
      throw new Error('Логин не выполнен');
    } catch (error) {
      throw new Error('Ошибка входа: ' + (error.message || 'Неизвестная ошибка'));
    }
  };

  const signUp = async (username, password, email) => {
    const result = await register(username, password, email);
    await new Promise(resolve => setTimeout(resolve, 2000)); // Задержка 2 секунды
    return { success: true };
  };

  const signOut = () => {
    setToken('');
    localStorage.removeItem('token');
  };

  return (
    <AuthContext.Provider value={{ token, signIn, signUp, signOut }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};