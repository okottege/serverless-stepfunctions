import React from 'react';
import {
  BrowserRouter as Router,
  Route,
  Switch
} from 'react-router-dom';
import './App.css';
import Login from './components/auth/Login';
import NotesList from './components/NotesList';
import NotFound from './components/NotFound';

function App() {
  return (
    <Router>
      <Switch>
        <Route exact path="/" component={Login} />
        <Route exact path="/notes" component={NotesList} />
        <Route component={NotFound} />
      </Switch>
    </Router>
  );
}

export default App;
