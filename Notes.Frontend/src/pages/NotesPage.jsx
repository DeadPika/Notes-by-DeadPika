import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getNotes } from '../api/api';

const NotesPage = () => {
  const [notes, setNotes] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchNotes = async () => {
      try {
        const data = await getNotes();
        setNotes(data);
      } catch (error) {
        console.error('Error fetching notes:', error);
      }
    };
    fetchNotes();
  }, []);

  return (
    <div>
      <h1>Страница заметок.</h1>
      {notes.length > 0 ? (
        <ul>
          {notes.map(note => (
            <li key={note.id}>{note.title || 'Без заголовка'}</li>
          ))}
        </ul>
      ) : (
        <p>Заметки не найдены или ещё загружаются...</p>
      )}
    </div>
  );
};

export default NotesPage;