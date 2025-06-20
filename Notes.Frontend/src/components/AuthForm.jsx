import { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

const AuthForm = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [username, setUsername] = useState('');
  const [error, setError] = useState('');
  const { signIn, signUp } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();
  const isLogin = location.pathname === '/login';

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    console.log('Submitting:', { email, password, username, isLogin });
    try {
      let result;
      if (isLogin) {
        result = await signIn(email, password);
        if (result) {
          navigate('/notes');
        }
      } else {
        result = await signUp(username, password, email);
        if (result && result.success) {
          navigate('/login');
        }
      }
    } catch (error) {
      console.error('Auth error:', error.response?.data || error.message);
      setError(error.response?.data?.message || error.message || 'Произошла ошибка');
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
      {error && <p className="text-red-500 mb-4">{error}</p>}
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