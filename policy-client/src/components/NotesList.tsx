import React from 'react';
import {Card, Alert} from 'react-bootstrap';
import useNoteListService from "../services/NoteListService";
import {NoteListState} from "../types/NoteListState";
import {Note} from "../types/Note";

const noteListUrl = 'https://api.noteapp.info/v1/notes';

function renderNote(note: Note): JSX.Element {
  return (
    <React.Fragment>
      <h3>{note.title}</h3>
      <p>{note.content}</p>
    </React.Fragment>
  )
}

function renderNoteList(noteListState: NoteListState<Note>) {
  switch (noteListState.status) {
    case "loading": return <p>Loading...</p>
    case "loaded":
      return noteListState?.notes?.length === 0
        ? <p>There are no notes to display.</p>
        : noteListState.notes.map(renderNote);
    case "error": return <Alert variant="danger">There was an error loading the notes.</Alert>
  }
}

export default function NotesList() {
  const notes = useNoteListService(noteListUrl);

  return(
    <Card>
      <Card.Body>
        <h1>The Notes List</h1>
        {renderNoteList(notes)}
      </Card.Body>
    </Card>
  )
}
