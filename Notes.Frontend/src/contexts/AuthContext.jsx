import React, { createContext, useState, useContext } from 'react';
import { login, register } from '../api/api';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [token, setToken] = useState(document.cookie.split('; ').find(row => row.startsWith('note-cookies='))?.split('=')[1] || '');

  const signIn = async (email, password) => {
    const response = await login(email, password);
    setToken(response.token || '');
    document.cookie = `note-cookies=${response.token}; path=/`;
  };

  const signUp = async (username, password, email) => {
    await register(username, password, email);
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

export const useAuth = () => useContext(AuthContext);