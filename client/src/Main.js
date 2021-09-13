import React, { useContext } from 'react';
import { Redirect, Route, Switch } from 'react-router-dom';
import Dashboard from './Components/Dashboard';
import Login from './Components/Access/Login/Login';
import { LosTodosContext } from './App';
import AuthRoute from './AuthRoute';

const Main = () => {
  const { user } = useContext(LosTodosContext);

  return (
    <Switch>
      <Route exact path='/login'>
        {user !== null ? <Redirect to={{ pathname: '/' }} /> : <Login />}
      </Route>
      <AuthRoute path='/' component={Dashboard} />
    </Switch>
  );
};

export default Main;
