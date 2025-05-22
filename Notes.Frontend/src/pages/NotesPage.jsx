import React, { useState, useEffect } from 'react';
import { useAuth } from '../contexts/AuthContext';
import { useNavigate } from 'react-router-dom';
import NoteList from '../components/NoteList';
import { getNotes, deleteNote } from '../api/api';

const NotesPage = () => {
  const [notes, setNotes] = useState([]);
  const { token } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (!token) {
      navigate('/login');
      return;
    }
    fetchNotes();
  }, [token, navigate]);

  const fetchNotes = async () => {
    const data = await getNotes('v1');
    setNotes(data.notes || []);
  };

  const handleDelete = async (id) => {
    await deleteNote(id, 'v1');
    fetchNotes();
  };

  return (
    <div className="container mx-auto p-4">
      <NoteList notes={notes} onDelete={handleDelete} />
    </div>
  );
};

export default NotesPage;