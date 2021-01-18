import React from 'react';
import { Redirect, Route, Switch } from 'react-router-dom';
import TodoDashboard from './Components/TodoDashboard';
import Login from './Components/Access/Login/Login';
import Register from './Components/Access/Register/Register';
import ForgetPassword from './Components/Access/ForgetPassword/ForgetPassword';

const Main = ({ logged, setLogged }) => {
  return (
    <Switch>
      <Route exact path='/'>
        {logged ? (
          <TodoDashboard setLogged={setLogged} />
        ) : (
          <Redirect to={{ pathname: '/login' }} />
        )}
      </Route>
      <Route path='/login'>
        {logged ? (
          <Redirect to={{ pathname: '/' }} />
        ) : (
          <Login setLogged={setLogged} />
        )}
      </Route>
      <Route path='/register'>
        {logged ? <Redirect to={{ pathname: '/' }} /> : <Register />}
      </Route>
      <Route path='/forget/password'>
        {logged ? <Redirect to={{ pathname: '/' }} /> : <ForgetPassword />}
      </Route>
    </Switch>
  );
};

export default Main;
