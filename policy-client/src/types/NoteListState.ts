interface NoteListInit {
  status: 'init'
}

interface NoteListLoading {
  status: 'loading'
}

interface NoteListLoaded<T> {
  status: 'loaded',
  notes: T[]
}

interface NoteListError {
  status: 'error',
  error: Error
}

export type NoteListState<T> =
  | NoteListInit
  | NoteListLoading
  | NoteListLoaded<T>
  | NoteListError;
