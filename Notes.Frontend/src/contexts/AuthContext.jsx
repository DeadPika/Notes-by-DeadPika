import { createContext, useContext, useState, useEffect } from 'react';
import { login, register } from '../api/api';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [token, setToken] = useState('');

  // Функция для обновления токена из куки
  const updateTokenFromCookie = () => {
    const rawCookies = document.cookie;
    const savedToken = rawCookies
      .split('; ')
      .find(row => row.startsWith('note-cookies='))
      ?.split('=')[1] || '';
    if (savedToken && savedToken !== token) {
      setToken(savedToken);
    }
  };

  useEffect(() => {
    updateTokenFromCookie(); // Инициализация при монтировании
    const interval = setInterval(updateTokenFromCookie, 1000); // Периодическая проверка каждую секунду
    return () => clearInterval(interval); // Очистка интервала при размонтировании
  }, [token]);

  const signIn = async (email, password) => {
    const result = await login(email, password);
    await new Promise(resolve => setTimeout(resolve, 2000)); // Задержка 2 секунды
    if (result.status === 200) {
      updateTokenFromCookie(); // Обновляем токен после логина
      return true;
    }
    throw new Error('Логин не выполнен');
  };

  const signUp = async (username, password, email) => {
    const result = await register(username, password, email);
    await new Promise(resolve => setTimeout(resolve, 2000)); // Задержка 2 секунды
    return { success: true };
  };

  const signOut = () => {
    setToken('');
    document.cookie = 'note-cookies=; Max-Age=0; path=/';
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