import { createContext, useContext, useState } from 'react';
import { login, register } from '../api/api';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [token, setToken] = useState('');

  const signIn = async (email, password) => {
    try {
      await login(email, password);
      await new Promise(resolve => setTimeout(resolve, 200)); // Задержка для синхронизации
      const rawCookies = document.cookie;
      console.log('Raw document.cookie:', rawCookies); // Лог для отладки
      const extractedToken = rawCookies // Переименовали, чтобы избежать конфликта
        .split('; ')
        .find(row => row.startsWith('note-cookies='))
        ?.split('=')[1] || '';
      console.log('Extracted token:', extractedToken); // Лог для отладки
      setToken(extractedToken);
      return extractedToken; // Убедимся, что возвращаем токен
    } catch (error) {
      console.error('SignIn error:', error);
      throw error;
    }
  };

  const signUp = async (username, password, email) => {
    try {
      await register(username, password, email);
    } catch (error) {
      console.error('SignUp error:', error);
      throw error;
    }
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