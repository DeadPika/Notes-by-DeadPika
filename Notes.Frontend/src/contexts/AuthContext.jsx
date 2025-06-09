import { createContext, useContext, useState, useEffect } from 'react';
import { login, register } from '../api/api';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [token, setToken] = useState('');

  useEffect(() => {
    const rawCookies = document.cookie;
    console.log('Raw cookies:', rawCookies);
    const savedToken = rawCookies
      .split('; ')
      .find(row => row.startsWith('note-cookies='))
      ?.split('=')[1] || '';
    console.log('Extracted token from cookies:', savedToken);
    if (savedToken) {
      setToken(savedToken);
    }
  }, []);

  const signIn = async (email, password) => {
    try {
      const data = await login(email, password); // Получаем данные ответа
      console.log('Login response data:', data); // Лог для отладки
      let extractedToken = data.token || data.access_token; // Адаптируй под структуру ответа
      if (!extractedToken) {
        // Проверяем куки, если токен не в ответе
        const rawCookies = document.cookie;
        extractedToken = rawCookies
          .split('; ')
          .find(row => row.startsWith('note-cookies='))
          ?.split('=')[1] || '';
      }
      if (!extractedToken) throw new Error('Токен не получен');
      setToken(extractedToken);
      return extractedToken;
    } catch (error) {
      console.error('SignIn error:', error);
      throw error;
    }
  };

  const signUp = async (username, password, email) => {
    try {
      const data = await register(username, password, email);
      console.log('Register response:', data);
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