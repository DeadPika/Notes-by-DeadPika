import React, { useEffect, useState } from 'react';
import { useAuth } from '../contexts/AuthContext';
import { getNotes } from '../api/api';

const NotesPage = () => {
  const { token } = useAuth();
  const [notes, setNotes] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchNotes = async () => {
      if (!token) {
        setError('Нет токена авторизации');
        setLoading(false);
        return;
      }
      try {
        const data = await getNotes();
        console.log('Fetched notes:', data); // Для отладки
        if (Array.isArray(data)) {
          setNotes(data);
        } else {
          setNotes([]); // Если данные не массив, устанавливаем пустой массив
          setError('Данные не являются списком заметок');
        }
      } catch (error) {
        console.error('Ошибка загрузки заметок:', error);
        setError('Не удалось загрузить заметки');
      } finally {
        setLoading(false);
      }
    };
    fetchNotes();
  }, [token]);

  if (loading) return <div>Загрузка...</div>;
  if (error) return <div>Ошибка: {error}</div>;

  return (
    <div>
      <h1>Ваши заметки</h1>
      {notes.length === 0 ? (
        <p>У вас пока нет заметок.</p>
      ) : (
        <ul>
          {notes.map((note) => (
            <li key={note.id}>{note.title || note.id}</li>
          ))}
        </ul>
      )}
      <button onClick={handleCreateNote}>Создать заметку</button>
    </div>
  );
};

export default NotesPage;