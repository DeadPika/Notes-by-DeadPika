import React, { useState } from 'react';
import { useAuth } from '../contexts/AuthContext';
import { useNavigate } from 'react-router-dom';

const AuthForm = ({ type = 'login' }) => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [username, setUsername] = useState('');
  const { signIn, signUp } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (type === 'login') {
        await signIn(email, password);
        navigate('/notes');
      } else {
        await signUp(username, password, email);
        navigate('/login');
      }
    } catch (error) {
      alert(error.response?.data?.message || error.message || 'Ошибка авторизации');
    }
  };

  return (
    <div className="max-w-md mx-auto p-4 bg-white rounded shadow mt-10">
      <h2 className="text-2xl font-bold mb-4">{type === 'login' ? 'Вход' : 'Регистрация'}</h2>
      <form onSubmit={handleSubmit}>
        {type === 'register' && (
          <div className="mb-4">
            <label className="block text-gray-700">Имя пользователя</label>
            <input
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              className="w-full p-2 border rounded"
              required
            />
          </div>
        )}
        <div className="mb-4">
          <label className="block text-gray-700">Email</label>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            className="w-full p-2 border rounded"
            required
          />
        </div>
        <div className="mb-4">
          <label className="block text-gray-700">Пароль</label>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            className="w-full p-2 border rounded"
            required
          />
        </div>
        <button type="submit" className="w-full p-2 bg-blue-500 text-white rounded hover:bg-blue-600">
          {type === 'login' ? 'Войти' : 'Зарегистрироваться'}
        </button>
      </form>
    </div>
  );
};

export default AuthForm;