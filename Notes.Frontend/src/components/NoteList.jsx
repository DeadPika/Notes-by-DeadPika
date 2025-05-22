import React from 'react';
import { Link } from 'react-router-dom';

const NoteList = ({ notes, onDelete }) => {
  return (
    <div className="mb-4">
      <h2 className="text-xl font-bold mb-2">Заметки</h2>
      {notes.length === 0 ? (
        <p>Заметок нет.</p>
      ) : (
        <ul className="space-y-2">
          {notes.map((note) => (
            <li key={note.id} className="flex justify-between items-center p-2 bg-white rounded shadow">
              <Link to={`/notes/${note.id}`} className="text-blue-500 hover:underline">
                {note.title}
              </Link>
              <button
                onClick={() => onDelete(note.id)}
                className="text-red-500 hover:text-red-700"
              >
                Удалить
              </button>
            </li>
          ))}
        </ul>
      )}
      <Link to="/notes/create" className="mt-4 inline-block p-2 bg-green-500 text-white rounded hover:bg-green-600">
        Создать заметку
      </Link>
    </div>
  );
};

export default NoteList;