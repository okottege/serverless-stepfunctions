import { useEffect, useState } from 'react';
import {Note} from "../types/Note";
import {NoteListState} from "../types/NoteListState";
import axios from 'axios';

const useNoteListService = (getUrl: string) => {
  const [notes, setNotes] = useState<NoteListState<Note>>({
    status: 'loading'
  });

  useEffect(() => {
    const fetchNotes = async () => {
      try {
        const response = await axios.get<Note[]>(getUrl);
        const notes = response.data;
        setNotes({status: 'loaded', notes});
      } catch (error) {
        setNotes({status: 'error', error});
      }
    };
    // noinspection JSIgnoredPromiseFromCall
    fetchNotes();
  }, [getUrl]);

  return notes;
};

export default useNoteListService;
