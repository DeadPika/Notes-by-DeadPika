import React from 'react';
import { useAuth } from '../contexts/AuthContext';
import { useNavigate } from 'react-router-dom';

const Navbar = () => {
  const { signOut, token } = useAuth();
  const navigate = useNavigate();

  const handleSignOut = () => {
    signOut();
    navigate('/login');
  };

  return (
    <nav className="bg-gray-800 p-4 text-white flex justify-between items-center">
      <div className="text-xl font-bold">Заметки от DeadPika.</div>
      <div>
        <button
          onClick={() => navigate('/notes')}
          className="mr-4 bg-blue-500 hover:bg-blue-600 text-white font-bold py-2 px-4 rounded"
        >
          Заметки
        </button>
        {token ? (
          <button
            onClick={handleSignOut}
            className="bg-red-500 hover:bg-red-600 text-white font-bold py-2 px-4 rounded"
          >
            Выход
          </button>
        ) : (
          <>
            <button
              onClick={() => navigate('/login')}
              className="mr-4 bg-green-500 hover:bg-green-600 text-white font-bold py-2 px-4 rounded"
            >
              Вход
            </button>
            <button
              onClick={() => navigate('/register')}
              className="bg-green-500 hover:bg-green-600 text-white font-bold py-2 px-4 rounded"
            >
              Регистрация
            </button>
          </>
        )}
      </div>
    </nav>
  );
};

export default Navbar;