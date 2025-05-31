import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

const Notes = () => {
  const [notes, setNotes] = useState([]);
  const { token } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchNotes = async () => {
      try {
        const response = await fetch('http://localhost:5269/api/v1/note', {
          headers: { Authorization: `Bearer ${token}` },
          credentials: 'include'
        });

        // Проверяем статус ответа вручную
        if (!response.ok) {
          if (response.status === 401) {
            navigate('/login');
            return;
          }
          throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        setNotes(data);
      } catch (error) {
        console.error('Error fetching notes:', error);
        // Перенаправление на /login только при явных ошибках аутентификации
        // Если ошибка сети, можно показать сообщение об ошибке
        navigate('/login'); // Можно заменить на отображение ошибки
      }
    };
    fetchNotes();
  }, [token, navigate]);

  return (
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4">Заметки</h1>
      <ul className="list-disc pl-5">
        {notes.map((note) => (
          <li key={note.id} className="mb-2">
            {note.title || 'Без названия'}
          </li>
        ))}
      </ul>
      {notes.length > 0 && <span className="text-red-500">Удалить</span>}
      <button
        onClick={() => navigate('/create-note')}
        className="bg-green-500 text-white p-2 rounded mt-4"
      >
        Создать заметку
      </button>
    </div>
  );
};

export default Notes;