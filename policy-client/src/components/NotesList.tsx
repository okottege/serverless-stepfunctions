import React, {useState} from 'react';

type Note = {
  id: string,
  title: string,
  detail: string
}

function renderNote(note: Note): JSX.Element {
  return (
    <React.Fragment>
      <h3>{note.title}</h3>
      <p>{note.detail}</p>
    </React.Fragment>
  )
}

function renderNoteList(noteList: Note[]) {
  return noteList?.length === 0
    ? <p>There are no notes to display.</p>
    : noteList.map(renderNote);
}

export default function NotesList() {
  const [notes] = useState<Note[]>([]);
  return(
    <React.Fragment>
      <h1>The Notes List</h1>
      {renderNoteList(notes)}
    </React.Fragment>
  )
}
