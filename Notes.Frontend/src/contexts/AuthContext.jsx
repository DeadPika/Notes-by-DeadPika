import { createContext, useContext, useState, useEffect } from 'react';
import { login, register } from '../api/api';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [token, setToken] = useState('');

  useEffect(() => {
    const rawCookies = document.cookie;
    const savedToken = rawCookies
      .split('; ')
      .find(row => row.startsWith('note-cookies='))
      ?.split('=')[1] || '';
    if (savedToken) {
      setToken(savedToken);
    }
  }, []);

  const signIn = async (email, password) => {
    const result = await login(email, password);
    await new Promise(resolve => setTimeout(resolve, 2000)); // Задержка 2 секунды
    if (result.status === 200) {
      const rawCookies = document.cookie;
      const extractedToken = rawCookies
        .split('; ')
        .find(row => row.startsWith('note-cookies='))
        ?.split('=')[1] || '';
      if (extractedToken) {
        setToken(extractedToken);
      }
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