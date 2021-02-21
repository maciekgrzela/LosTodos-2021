import React, { useContext } from 'react';
import { Redirect, Route, Switch } from 'react-router-dom';
import Dashboard from './Components/Dashboard';
import Login from './Components/Access/Login/Login';
import Register from './Components/Access/Register/Register';
import ForgetPassword from './Components/Access/ForgetPassword/ForgetPassword';
import { LosTodosContext } from './App';

const Main = () => {
  const { user } = useContext(LosTodosContext);

  return (
    <Switch>
      <Route exact path='/'>
        {user !== null ? (
          <Dashboard />
        ) : (
          <Redirect to={{ pathname: '/login' }} />
        )}
      </Route>
      <Route exact path='/login'>
        {user !== null ? <Redirect to={{ pathname: '/' }} /> : <Login />}
      </Route>
      <Route exact path='/register'>
        {user !== null ? <Redirect to={{ pathname: '/' }} /> : <Register />}
      </Route>
      <Route exact path='/forget/password'>
        {user !== null ? (
          <Redirect to={{ pathname: '/' }} />
        ) : (
          <ForgetPassword />
        )}
      </Route>
      <Route path='/'>
        {user !== null ? (
          <Dashboard />
        ) : (
          <Redirect to={{ pathname: '/login' }} />
        )}
      </Route>
    </Switch>
  );
};

export default Main;
