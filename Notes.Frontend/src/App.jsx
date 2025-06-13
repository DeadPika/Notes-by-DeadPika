import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider } from './contexts/AuthContext';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import NotesPage from './pages/NotesPage';
import NotePage from './pages/NotePage';
import Navbar from './components/Navbar';
import AuthForm from './components/AuthForm';

function App() {
  return (
    <AuthProvider>
      <Router>
        <Navbar />
        <div style={{ padding: '2rem' }}>
          <Routes>
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route path="/*" element={
              <Routes>
                <Route path="/" element={<Navigate to="/notes" />} />
                <Route path="/notes" element={<NotesPage />} />
                <Route path="/notes/:id" element={<NotePage />} />
                <Route path="/notes/create" element={<NotePage />} />
                <Route path="/notes/edit/:id" element={<NotePage />} />
              </Routes>
            } />
          </Routes>
        </div>
      </Router>
    </AuthProvider>
  );
}

export default App;