import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate, useLocation } from 'react-router-dom';
import { AuthProvider, useAuth } from './contexts/AuthContext';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import NotesPage from './pages/NotesPage';
import NotePage from './pages/NotePage';
import Navbar from './components/Navbar';

// Компонент PrivateRoute
const PrivateRoute = ({ children }) => {
  const { token } = useAuth();
  const [isLoading, setIsLoading] = useState(true);
  const location = useLocation();

  useEffect(() => {
    // Проверка токена
    setIsLoading(false);
  }, [token]);

  if (isLoading) {
    return null; // Можно заменить на спиннер
  }

  const isAuthPage = ['/login', '/register'].includes(location.pathname);
  return token || isAuthPage ? children : <Navigate to="/login" state={{ from: location }} />;
};

function App() {
  return (
    <AuthProvider>
      <Router>
        <Navbar /> {/* Добавляем навигацию */}
        <div style={{ padding: '2rem' }}>
          <Routes>
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route
              path="/*"
              element={
                <PrivateRoute>
                  <Routes>
                    <Route path="/" element={<Navigate to="/notes" />} />
                    <Route path="/notes" element={<NotesPage />} />
                    <Route path="/notes/:id" element={<NotePage />} />
                    <Route path="/notes/create" element={<NotePage />} />
                    <Route path="/notes/edit/:id" element={<NotePage />} />
                  </Routes>
                </PrivateRoute>
              }
            />
          </Routes>
        </div>
      </Router>
    </AuthProvider>
  );
}

export default App;