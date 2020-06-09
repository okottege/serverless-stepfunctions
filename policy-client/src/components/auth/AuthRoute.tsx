import React from 'react';
import {RouteComponentProps, Route, Redirect} from 'react-router-dom';

interface AuthRouteProps {
  component: React.FC<RouteComponentProps>
  path: string,
  exact?: boolean
}

const AuthRoute = ({component, path, exact = false}: AuthRouteProps): JSX.Element => {
  const isAuthenticated = !!localStorage.getItem("AUTH_TOKEN");
  const message = 'Please login to view this page';

  return (
    <Route
      exact={exact}
      path={path}
      render={(props: RouteComponentProps) =>
        isAuthenticated
          ? (component)
          : (
            <Redirect to={{pathname: '/', state: {message, requestedPath: path}}} />
          )
      }
    />
  );
};

export default AuthRoute;
