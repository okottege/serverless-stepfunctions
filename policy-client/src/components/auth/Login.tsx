import React from 'react'
import {Card, Button} from 'react-bootstrap'

const Login = () : JSX.Element => (
  <Card>
    <Card.Body>
      <h1>Login to Notes app</h1>
      <p>Please enter the username and password</p>
      <Button>Login</Button>
    </Card.Body>
  </Card>
);

export default Login;

