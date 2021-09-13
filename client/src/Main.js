import React, { useContext } from 'react';
import { Redirect, Route, Switch } from 'react-router-dom';
import Dashboard from './Components/Dashboard';
import Login from './Components/Access/Login/Login';
import { LosTodosContext } from './App';
import AuthRoute from './AuthRoute';
import Register from './Components/Access/Register/Register';

const Main = () => {
  const { user } = useContext(LosTodosContext);

  return (
    <Switch>
      <Route exact path='/login'>
        {user !== null ? <Redirect to={{ pathname: '/' }} /> : <Login />}
      </Route>
      <Route exact path='/register'>
        {user !== null ? <Redirect to={{ pathname: '/' }} /> : <Register />}
      </Route>
      <AuthRoute path='/' component={Dashboard} />
    </Switch>
  );
};

export default Main;
