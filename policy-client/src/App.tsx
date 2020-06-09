import React from 'react';
import {
  BrowserRouter as Router,
  Route,
  Switch
} from 'react-router-dom';
import {Jumbotron} from 'react-bootstrap';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Login from './components/auth/Login';
import NotesList from './components/NotesList';
import NotFound from './components/NotFound';
import AuthRoute from './components/auth/AuthRoute';

function App() {
  return (
    <Jumbotron>
      <Router>
        <Switch>
          <Route exact path="/" component={Login} />
          <Route exact path="/login" component={Login} />
          <AuthRoute exact path="/notes" component={NotesList} />
          <Route component={NotFound} />
        </Switch>
      </Router>
    </Jumbotron>
  );
}

export default App;
