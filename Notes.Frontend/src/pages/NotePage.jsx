import React, { useState, useEffect } from 'react';
import { useParams, useLocation } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import { useNavigate } from 'react-router-dom';
import NoteDetails from '../components/NoteDetails';
import NoteForm from '../components/NoteForm';
import { getNoteDetails, createNote, updateNote } from '../api/api';

const NotePage = () => {
  const [note, setNote] = useState(null);
  const { token } = useAuth();
  const { id } = useParams();
  const location = useLocation();
  const navigate = useNavigate();
  const isEdit = location.pathname.includes('edit');
  const isCreate = location.pathname.includes('create');

  useEffect(() => {
    if (!token) {
      navigate('/login');
      return;
    }
    if (id && !isCreate) {
      fetchNoteDetails();
    }
  }, [id, token, navigate, isCreate]);

  const fetchNoteDetails = async () => {
    const data = await getNoteDetails(id, 'v1');
    setNote(data);
  };

  const handleSubmit = async (noteData) => {
    if (isCreate) {
      await createNote(noteData, 'v1');
    } else {
      await updateNote(id, noteData, 'v1');
    }
  };

  return (
    <div className="container mx-auto p-4">
      {isEdit || isCreate ? (
        <NoteForm onSubmit={handleSubmit} initialNote={note || {}} />
      ) : (
        <NoteDetails note={note} />
      )}
    </div>
  );
};

export default NotePage;