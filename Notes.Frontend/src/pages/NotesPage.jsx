import React, { useEffect, useState } from 'react';
import { useAuth } from '../contexts/AuthContext';
import { getNotes, createNote } from '../api/api';

const NotesPage = () => {
  const { token } = useAuth();
  const [notes, setNotes] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchNotes = async () => {
      if (token) {
        try {
          const data = await getNotes();
          setNotes(data);
        } catch (error) {
          console.error('Ошибка загрузки заметок:', error);
        } finally {
          setLoading(false);
        }
      }
    };
    fetchNotes();
  }, [token]);

  const handleCreateNote = async () => {
    try {
      const newNote = { title: 'Новая заметка', content: '' }; // Пример данных
      const response = await createNote(newNote);
      setNotes([...notes, response.data]); // Обновляем список заметок
    } catch (error) {
      console.error('Ошибка создания заметки:', error);
    }
  };

  if (loading) return <div>Загрузка...</div>;
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