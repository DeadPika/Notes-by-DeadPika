import React from 'react';
import { Link } from 'react-router-dom';

const NoteDetails = ({ note }) => {
  if (!note) return <p>Выберите заметку для просмотра.</p>;

  return (
    <div className="p-4 bg-white rounded shadow">
      <h2 className="text-xl font-bold mb-2">{note.title}</h2>
      <p className="text-gray-700 mb-2">{note.details}</p>
      <p className="text-gray-500">Создано: {new Date(note.creationDate).toLocaleDateString()}</p>
      <Link to={`/notes/edit/${note.id}`} className="mt-2 inline-block text-blue-500 hover:underline">
        Редактировать
      </Link>
    </div>
  );
};

export default NoteDetails;