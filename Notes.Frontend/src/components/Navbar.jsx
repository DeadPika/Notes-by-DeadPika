import { Link } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

const Navbar = () => {
  const { token, logout } = useAuth();

  return (
    <nav style={{ backgroundColor: '#333', padding: '1rem' }}>
      <ul style={{ listStyle: 'none', display: 'flex', gap: '1rem', margin: 0, padding: 0 }}>
        {!token && (
          <>
            <li>
              <Link to="/register" style={{ color: 'white', textDecoration: 'none' }}>Регистрация</Link>
            </li>
            <li>
              <Link to="/login" style={{ color: 'white', textDecoration: 'none' }}>Логин</Link>
            </li>
          </>
        )}
        {token && (
          <li>
            <button
              onClick={logout}
              style={{ color: 'white', background: 'none', border: 'none', cursor: 'pointer' }}
            >
              Выйти
            </button>
          </li>
        )}
      </ul>
    </nav>
  );
};

export default Navbar;