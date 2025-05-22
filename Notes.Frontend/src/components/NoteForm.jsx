import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const NoteForm = ({ onSubmit, initialNote = { title: '', details: '' } }) => {
  const [title, setTitle] = useState(initialNote.title);
  const [details, setDetails] = useState(initialNote.details);
  const navigate = useNavigate();

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit({ title, details });
    navigate('/notes');
  };

  return (
    <div className="max-w-md mx-auto p-4 bg-white rounded shadow mt-10">
      <h2 className="text-2xl font-bold mb-4">{initialNote.id ? 'Редактировать заметку' : 'Создать заметку'}</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-4">
          <label className="block text-gray-700">Заголовок</label>
          <input
            type="text"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            className="w-full p-2 border rounded"
            required
          />
        </div>
        <div className="mb-4">
          <label className="block text-gray-700">Детали</label>
          <textarea
            value={details}
            onChange={(e) => setDetails(e.target.value)}
            className="w-full p-2 border rounded"
            rows="4"
            required
          />
        </div>
        <button type="submit" className="w-full p-2 bg-blue-500 text-white rounded hover:bg-blue-600">
          Сохранить
        </button>
      </form>
    </div>
  );
};

export default NoteForm;