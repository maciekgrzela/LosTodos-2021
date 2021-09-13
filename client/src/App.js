import React, { createContext, useEffect, useState } from 'react';
import httpClient from './API/httpClient';
import Main from './Main';
import { sortByDate } from './Helpers/Helpers';

const LosTodosContext = createContext();

const App = () => {
  const [user, setUser] = useState(null);
  const [myTodoLists, setMyTodoLists] = useState([]);
  const [myTags, setMyTags] = useState([]);

  const login = async (data) => {
    setUser(data);
    if (data === null) {
      localStorage.removeItem('jwt');
    } else {
      localStorage.setItem('jwt', data.token);
      try {
        await fetchMyData();
      } catch (error) {
        setMyTags([]);
        setMyTodoLists([]);
      }
    }
  };

  const fetchMyData = async () => {
    await fetchMyTodos();
    await fetchMyTags();
  };

  const fetchMyTodos = async () => {
    try {
      const myTodosFetched = await httpClient.TodoSets.myTodoSets();
      setMyTodoLists(sortByDate(myTodosFetched));
    } catch (e) {
      console.log(e);
    }
  };

  const fetchMyTags = async () => {
    try {
      const myTagsFetched = await httpClient.Tags.myTags();
      setMyTags(sortByDate(myTagsFetched));
    } catch (e) {
      console.log(e);
    }
  };

  const updateMyTodo = (updated, todoSetId) => {
    const updatedTodoList = myTodoLists.filter((p) => p.id === todoSetId);
    updatedTodoList[0].todos = updatedTodoList[0].todos.filter(
      (p) => p.id !== updated.id
    );
    updatedTodoList[0].todos.push(updated);
    updatedTodoList[0].todos = sortByDate(updatedTodoList[0].todos);
    const remainTodos = myTodoLists.filter((p) => p.id !== todoSetId);
    const newUpdated = sortByDate([...remainTodos, updatedTodoList[0]]);
    setMyTodoLists(newUpdated);
  };

  const removeTodo = (id, todoSetId) => {
    const updatedTodoList = myTodoLists.filter((p) => p.id === todoSetId);
    updatedTodoList[0].todos = updatedTodoList[0].todos.filter(
      (p) => p.id !== id
    );
    updatedTodoList[0].todos = sortByDate(updatedTodoList[0].todos);
    const remainTodos = myTodoLists.filter((p) => p.id !== todoSetId);
    const newUpdated = sortByDate([...remainTodos, updatedTodoList[0]]);
    setMyTodoLists(newUpdated);
  };

  const addTodo = (todo, todoSetId) => {
    const updatedTodoList = myTodoLists.filter((p) => p.id === todoSetId);
    updatedTodoList[0].todos.push(todo);
    updatedTodoList[0].todos = sortByDate(updatedTodoList[0].todos);
    const remainTodos = myTodoLists.filter((p) => p.id !== todoSetId);
    const newUpdated = sortByDate([...remainTodos, updatedTodoList[0]]);
    setMyTodoLists(newUpdated);
  };

  const removeTodoList = (id) => {
    const newTodoLists = myTodoLists.filter((p) => p.id !== id);
    setMyTodoLists(sortByDate(newTodoLists));
  };

  useEffect(() => {
    const autoLogin = async () => {
      try {
        let loggedUser = await httpClient.User.currentUser();
        login(loggedUser);
      } catch (error) {
        setUser(null);
        localStorage.removeItem('jwt');
      }
    };

    if (localStorage.getItem('jwt') !== null) {
      autoLogin();
    }
  }, []);

  return (
    <LosTodosContext.Provider
      value={{
        user,
        login,
        myTags,
        myTodoLists,
        fetchMyData,
        updateMyTodo,
        removeTodo,
        addTodo,
        removeTodoList,
      }}
    >
      <Main />
    </LosTodosContext.Provider>
  );
};

export { App, LosTodosContext };
