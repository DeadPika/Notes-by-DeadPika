import { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import { login } from '../api/api'; // Импортируем для проверки

const AuthForm = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [username, setUsername] = useState('');
  const { signIn } = useAuth(); // Используем signIn из AuthContext
  const navigate = useNavigate();
  const location = useLocation();
  const isLogin = location.pathname === '/login';

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      let result;
      if (isLogin) {
        result = await signIn(email, password); // Вызываем signIn из AuthContext
      } else {
        result = await login(email, password); // Для регистрации можно использовать напрямую
        // Или адаптировать под register из AuthContext, если нужно
      }
      console.log('Auth response:', result);
      if (result && result.token) {
        console.log('Token received:', result.token);
        navigate('/notes'); // Перенаправление после успеха
      } else {
        throw new Error('Токен не получен');
      }
    } catch (error) {
      console.error('Auth error:', error.message);
      // Отображение ошибки пользователю (например, alert или состояние)
    }
  };

  return (
    <form onSubmit={handleSubmit} className="max-w-md mx-auto mt-8 p-4 bg-white shadow-md rounded">
      {!isLogin && (
        <div className="mb-4">
          <label className="block text-gray-700">Имя пользователя</label>
          <input
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            className="w-full p-2 border rounded"
            placeholder="Введите имя пользователя"
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
          placeholder="Введите email"
        />
      </div>
      <div className="mb-4">
        <label className="block text-gray-700">Пароль</label>
        <input
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          className="w-full p-2 border rounded"
          placeholder="Введите пароль"
        />
      </div>
      <button
        type="submit"
        className="w-full bg-blue-500 text-white p-2 rounded hover:bg-blue-600"
      >
        {isLogin ? 'Войти' : 'Зарегистрироваться'}
      </button>
    </form>
  );
};

export default AuthForm;