import React, { useContext } from 'react';
import { Redirect, Route } from 'react-router';
import { LosTodosContext } from './App';

const AuthRoute = ({ component: Component, ...rest }) => {
  const { user } = useContext(LosTodosContext);

  return (
    <Route
      {...rest}
      render={(props) =>
        user !== null ? (
          <Component {...props} />
        ) : (
          <Redirect to={{ pathname: '/login' }} />
        )
      }
    />
  );
};

export default AuthRoute;
