import React, { useContext, useState } from 'react';
import {
  makeStyles,
  Grid,
  TextField,
  Typography,
  Divider,
  Button,
} from '@material-ui/core';
import HighlightOffIcon from '@material-ui/icons/HighlightOff';
import TodoTagFilter from './TodoTagFilter';
import httpClient from '../../API/httpClient';
import AddTodoResult from './AddTodoResult';
import { LosTodosContext } from '../../App';

const useStyles = makeStyles(() => ({
  root: {
    flexDirection: 'column',
    padding: '120px 40px 20px 40px',
  },
  addTodoForm: {
    marginTop: 40,
    '& .MuiOutlinedInput-input': {
      padding: '10px 14px',
    },
    '& label': {
      transform: 'translate(14px, 12px) scale(1)',
    },
  },
  addTodoContainer: {
    margin: '10px auto',
  },
  addTodoBtn: {
    flexBasis: '40%',
    backgroundColor: '#ffc400',
    color: 'white',
    '&:hover': {
      backgroundColor: '#CC9C00',
      color: 'white',
    },
  },
  todoListItem: {
    marginTop: 10,
    marginBottom: 10,
  },
  selectTags: {
    width: '100%',
  },
  chip: {
    marginRight: 5,
  },
}));

const AddTodo = () => {
  const styles = useStyles();

  const [todoListName, setTodoListName] = useState('');
  const [selectedTags, setSelectedTags] = useState([]);
  const [todos, setTodos] = useState(['']);
  const [result, setResult] = useState(null);

  const { myTags, fetchMyData } = useContext(LosTodosContext);

  const addAnotherTodo = () => {
    setTodos([...todos, '']);
  };

  const removeTodo = (todo) => {
    const newTodos = todos.filter((p) => p !== todo);
    setTodos(newTodos);
  };

  const modifyTodo = (todo, updatedValue) => {
    const index = todos.findIndex((p) => p === todo);
    if (index !== -1) {
      let newTodos = [...todos];
      newTodos[index] = updatedValue;
      setTodos(newTodos);
    }
  };

  const submitTodoList = async (e) => {
    e.preventDefault();

    try {
      const created = await httpClient.TodoSets.create({
        name: todoListName,
      });
      console.log('CREATED: ', created);
      const myTodoSets = await httpClient.TodoSets.myTodoSets();
      console.log('TODO SETS LOADED: ', myTodoSets);
      const tagsForTodoResource = {
        todoSetId: myTodoSets[0].id,
        tags: selectedTags,
      };
      console.log('TAGSforTODOresource', tagsForTodoResource);
      await httpClient.Tags.addToTodoSet(tagsForTodoResource);

      const todosResource = [];
      todos.forEach((todo) => {
        todosResource.push({
          name: todo,
          checked: false,
          todoSetId: myTodoSets[0].id,
        });
      });

      await httpClient.Todos.addMultiple(todosResource);
      setResult({
        status: 'ok',
        info: myTodoSets[0].id,
      });
      fetchMyData();
    } catch (error) {
      if (error.response) {
        setResult({
          status: 'error',
          info: `Błąd: ${error.response.status}. ${error.response.data}`,
        });
      } else if (error.request) {
        setResult({
          status: 'error',
          info: 'Błąd: Nie udało się wysłać żądania. Spróbuj ponownie',
        });
      } else {
        setResult({
          status: 'error',
          info: 'Wystąpił nieoczekiwany błąd. Spróbuj ponownie',
        });
      }
    }
  };

  const refreshForm = () => {
    setTodoListName('');
    setSelectedTags([]);
    setTodos(['']);
    setResult(null);
  };

  return (
    <>
      {result !== null ? (
        <AddTodoResult result={result} refreshForm={refreshForm} />
      ) : (
        <Grid container item className={styles.root}>
          <Typography variant='h4'>Dodaj listę zadań</Typography>
          <Divider />
          <form
            className={styles.addTodoForm}
            onSubmit={submitTodoList}
            onKeyDown={(e) => {
              e.key === 'Enter' && e.preventDefault();
            }}
          >
            <Grid
              container
              alignItems='center'
              className={styles.addTodoContainer}
            >
              <Grid item xs={3} style={{ fontSize: '1.2rem' }}>
                Nazwa listy zadań:
              </Grid>
              <Grid item xs={9}>
                <TextField
                  type='text'
                  value={todoListName}
                  onChange={(e) => {
                    setTodoListName(e.target.value);
                  }}
                  autoComplete={false}
                  fullWidth={true}
                  label='Nazwa listy'
                  placeholder='Wprowadź nazwę listy zadań'
                  required
                  variant='outlined'
                  autoFocus
                />
              </Grid>
            </Grid>
            <Grid
              container
              alignItems='center'
              style={{ marginTop: 10, marginBottom: 10 }}
            >
              <Grid item xs={3} style={{ fontSize: '1.2rem' }}>
                Tagi listy:
              </Grid>
              <Grid item xs={9}>
                <TodoTagFilter
                  selectedOptions={selectedTags}
                  setSelectedOptions={setSelectedTags}
                  options={myTags}
                />
              </Grid>
            </Grid>
            <Grid
              container
              alignItems='center'
              style={{ marginTop: 10, marginBottom: 10 }}
            >
              {todos.map((todo, index) => (
                <Grid container item className={styles.todoListItem}>
                  <Grid item xs={3} style={{ fontSize: '1.2rem' }}>
                    {index === 0 ? 'Zadania: ' : ''}
                  </Grid>
                  <Grid item xs={9} container justify='space-between'>
                    <Button
                      onClick={() => removeTodo(todo)}
                      disabled={todos.length === 1}
                      variant='contained'
                      color='primary'
                    >
                      <HighlightOffIcon />
                    </Button>
                    <TextField
                      type='text'
                      value={todo}
                      onChange={(e) => {
                        modifyTodo(todo, e.target.value);
                      }}
                      onKeyDown={(e) => {
                        if (e.key === 'Enter') {
                          addAnotherTodo();
                        }
                      }}
                      style={{ flexBasis: '70%' }}
                      autoComplete={false}
                      label='Zadanie'
                      placeholder='Wprowadź opis zadania'
                      required
                      variant='outlined'
                      autoFocus={index !== 0}
                    />
                  </Grid>
                </Grid>
              ))}
            </Grid>
            <Grid style={{ marginTop: 15 }} container justify='center'>
              <Button
                onClick={() => {
                  addAnotherTodo();
                }}
                className={styles.addTodoBtn}
                variant='contained'
              >
                Dodaj kolejne
              </Button>
            </Grid>
            <Grid style={{ marginTop: 30 }} container justify='flex-end'>
              <Button
                style={{ padding: '10px 30px' }}
                type='submit'
                variant='contained'
                color='primary'
              >
                Zapisz listę zadań
              </Button>
            </Grid>
          </form>
        </Grid>
      )}
    </>
  );
};

export default AddTodo;
