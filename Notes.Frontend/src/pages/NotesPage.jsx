import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { getNotes } from '../api/api';

const NotesPage = () => {
  const navigate = useNavigate();

  useEffect(() => {
    const fetchNotes = async () => {
      try {
        const notes = await getNotes(); // Без проверки авторизации
        console.log(notes); // Для отладки
      } catch (error) {
        console.log('Error fetching notes:', error); // Логи для диагностики
        // Уберите перенаправление или сделайте его опциональным
      }
    };
    fetchNotes();
  }, []);

  return (
    <div>
      <h1>Notes Page</h1>
      {/* Здесь рендеринг заметок */}
    </div>
  );
};

export default NotesPage;