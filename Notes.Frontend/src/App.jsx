import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { AuthProvider } from './contexts/AuthContext';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import NotesPage from './pages/NotesPage';
import NotePage from './pages/NotePage';

const App = () => {
  return (
    <AuthProvider>
      <div className="min-h-screen bg-gray-100">
        <Routes>
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route path="/notes" element={<NotesPage />} />
          <Route path="/notes/:id" element={<NotePage />} />
          <Route path="/notes/create" element={<NotePage />} />
          <Route path="/notes/edit/:id" element={<NotePage />} />
          <Route path="/" element={<LoginPage />} />
        </Routes>
      </div>
    </AuthProvider>
  );
};

export default App;