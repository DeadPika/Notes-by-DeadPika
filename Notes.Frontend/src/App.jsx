import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { AuthProvider } from './contexts/AuthContext';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import NotesPage from './pages/NotesPage';
import NotePage from './pages/NotePage';

function App() {
  return (
    <AuthProvider>
      <Router>
        <Routes>
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route path="/notes" element={<NotesPage />} />
          <Route path="/" element={<LoginPage />} /> {/* Перенаправление по умолчанию, если нужно */}
          <Route path="/notes/:id" element={<NotePage />} />
          <Route path="/notes/create" element={<NotePage />} />
          <Route path="/notes/edit/:id" element={<NotePage />} />
        </Routes>
      </Router>
    </AuthProvider>
  );
}

export default App;