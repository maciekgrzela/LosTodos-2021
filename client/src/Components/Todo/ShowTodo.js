import React, { useContext, useEffect, useRef, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import {
  Grid,
  Typography,
  Chip,
  Avatar,
  makeStyles,
  Button,
  TextField,
  CircularProgress,
} from '@material-ui/core';
import { LosTodosContext } from '../../App';
import HighlightOffIcon from '@material-ui/icons/HighlightOff';
import CheckCircleOutlineIcon from '@material-ui/icons/CheckCircleOutline';
import httpClient from '../../API/httpClient';

const useStyles = makeStyles(() => ({
  root: {
    display: 'flex',
    flexDirection: 'row',
    alignItems: 'flex-end',
    backgroundColor: 'rgb(245,245,245)',
    paddingTop: 120,
  },
  editing: {
    backgroundColor: '#CC9C00',
    color: '#fff',
    padding: '10px 20px',
    borderRadius: 30,
  },
  notepad: {
    display: 'flex',
    flexWrap: 'nowrap',
    justifyContent: 'center',
    height: '95%',
    backgroundColor: 'rgb(245,245,245)',
    flexBasis: '100%',
  },
  paper: {
    position: 'relative',
    height: '95%',
    width: '90%',
    background:
      'url("https://www.toptal.com/designers/subtlepatterns/patterns/linedpaper.png")',
    border: '2px dashed #BABABB',
    boxShadow: '5px 5px 0px 0px rgb(207,207,208)',
    zIndex: 20,
  },
  header: {
    position: 'absolute',
    top: '-26px',
    width: '100%',
    height: 70,
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    zIndex: '100',
    '& .MuiCircularProgress-colorPrimary': {
      transform: 'rotate(73deg) !important',
      zIndex: '1',
      position: 'relative',
      '&::before': {
        content: '""',
        position: 'absolute',
        width: 30,
        height: 30,
        backgroundColor: 'rgb(245,245,245)',
        display: 'block',
        borderRadius: '50%',
        top: 39,
        right: '-9px',
        zIndex: '-1',
        boxShadow: '-1px -2px 10px 0px rgba(0,0,0,0.35)',
      },
    },
  },
  heading: {
    display: 'flex',
    padding: '0 20px 0 20px',
    marginTop: 100,
  },
  successBtn: {
    backgroundColor: 'rgb(40,167,69)',
    color: '#fff',

    '&:hover': {
      background: 'rgba(40,167,69, 0.9)',
      color: '#fff',
    },
  },
  todoField: {
    '& .MuiOutlinedInput-input': {
      padding: '10px 14px',
    },
  },
  successInput: {
    '& .MuiInputBase-fullWidth': {
      backgroundColor: 'rgba(40,167,69, 0.2)',
    },
  },
}));

const ShowTodo = () => {
  let { id } = useParams();

  const { myTodoLists, updateMyTodo, removeTodo, addTodo } =
    useContext(LosTodosContext);
  const [todoList, setTodoList] = useState(null);
  const [addedTodo, setAddedTodo] = useState('');

  const modifiedTodo = useRef(null);

  const styles = useStyles();

  useEffect(() => {
    const index = myTodoLists.findIndex((p) => p.id === id);
    if (index !== -1) {
      setTodoList(myTodoLists[index]);
    } else {
      console.log('Zgłoś błąd');
    }
    setAddedTodo('');
  }, [myTodoLists, id]);

  const setTodoChecked = async (todo) => {
    try {
      const resource = {
        name: todo.name,
        checked: !todo.checked,
        todoSetId: id,
      };
      await httpClient.Todos.update(resource, todo.id);
      const updatedTodo = await httpClient.Todos.get(todo.id);
      updateMyTodo(updatedTodo, id);
    } catch (e) {
      console.log(e);
    }
  };

  const setTodoRemoved = async (todo) => {
    try {
      await httpClient.Todos.remove(todo.id);
      removeTodo(todo.id, id);
    } catch (e) {
      console.log(e);
    }
  };

  const addNewTodo = async () => {
    try {
      const resource = {
        name: addedTodo,
        checked: false,
        todoSetId: id,
      };
      const newTodoId = await httpClient.Todos.add(resource);
      const newTodo = await httpClient.Todos.get(newTodoId);
      addTodo(newTodo, id);
    } catch (e) {
      console.log(e);
    }
  };

  return (
    <Grid container item className={styles.root}>
      <div className={styles.notepad}>
        <div className={styles.paper}>
          <div className={styles.header}>
            <CircularProgress
              style={{ width: 80, height: 80, marginRight: '-30px' }}
              variant='determinate'
              thickness={4.5}
              value={75}
            />
            <CircularProgress
              style={{ width: 80, height: 80, marginRight: '-30px' }}
              variant='determinate'
              thickness={4.5}
              value={75}
            />
            <CircularProgress
              style={{ width: 80, height: 80, marginRight: '-30px' }}
              variant='determinate'
              thickness={4.5}
              value={75}
            />
            <CircularProgress
              style={{ width: 80, height: 80, marginRight: '-30px' }}
              variant='determinate'
              thickness={4.5}
              value={75}
            />
            <CircularProgress
              style={{ width: 80, height: 80, marginRight: '-30px' }}
              variant='determinate'
              thickness={4.5}
              value={75}
            />
            <CircularProgress
              style={{ width: 80, height: 80, marginRight: '-30px' }}
              variant='determinate'
              thickness={4.5}
              value={75}
            />
            <CircularProgress
              style={{ width: 80, height: 80, marginRight: '-30px' }}
              variant='determinate'
              thickness={4.5}
              value={75}
            />
          </div>
          <Grid container item className={styles.heading}>
            <Grid item>
              <Typography variant='h4'>
                Lista zadań: {todoList !== null ? todoList.name : ''}
              </Typography>
            </Grid>
            <Grid
              item
              style={{
                flexBasis: '100%',
                display: 'inline-flex',
                marginTop: 10,
              }}
            >
              <Typography style={{ marginRight: 10 }} variant='h5'>
                Tagi:
              </Typography>
              <Grid container>
                {todoList !== null ? (
                  todoList.tags.map((tag) => (
                    <Link
                      style={{
                        textDecoration: 'none',
                        marginLeft: 5,
                        marginRight: 5,
                        marginBottom: 5,
                      }}
                      to={{ pathname: `/tag/${tag.id}` }}
                    >
                      <Chip
                        style={{ cursor: 'pointer' }}
                        avatar={<Avatar>#</Avatar>}
                        label={tag.name}
                      />
                    </Link>
                  ))
                ) : (
                  <Typography>Brak informacji</Typography>
                )}
              </Grid>
            </Grid>
            <Grid
              container
              item
              style={{
                paddingTop: 50,
                paddingLeft: 20,
                paddingRight: 20,
                paddingBottom: 100,
              }}
            >
              {todoList !== null ? (
                <>
                  {todoList.todos.map((todo, index) => (
                    <Grid
                      key={index}
                      container
                      item
                      alignItems='center'
                      style={{
                        marginTop: 10,
                        marginBottom: 10,
                      }}
                    >
                      <Grid container item xs={3} justify='space-evenly'>
                        <Button
                          variant='contained'
                          className={styles.successBtn}
                          onClick={() => setTodoChecked(todo)}
                        >
                          <CheckCircleOutlineIcon />
                        </Button>
                        <Button
                          variant='contained'
                          onClick={() => setTodoRemoved(todo)}
                          color='primary'
                        >
                          <HighlightOffIcon />
                        </Button>
                      </Grid>
                      <Grid
                        item
                        xs={9}
                        className={styles.todoField}
                        container
                        justify='space-between'
                      >
                        <TextField
                          type='text'
                          ref={modifiedTodo}
                          fullWidth
                          className={todo.checked ? styles.successInput : ''}
                          value={todo.name}
                          onChange={(e) => {}}
                          onKeyDown={(e) => {}}
                          autoComplete={false}
                          placeholder='Wprowadź opis zadania'
                          required
                          variant='outlined'
                        />
                      </Grid>
                    </Grid>
                  ))}
                </>
              ) : (
                'Brak todosów :('
              )}
              <Grid
                container
                item
                style={{ marginTop: 10, marginBottom: 20 }}
                className={styles.todoField}
                justify='flex-end'
              >
                <Grid item xs={9}>
                  <TextField
                    type='text'
                    fullWidth
                    value={addedTodo}
                    onChange={(e) => setAddedTodo(e.target.value)}
                    onKeyDown={(e) => {
                      if (e.key === 'Enter') {
                        addNewTodo();
                      }
                    }}
                    autoComplete={false}
                    placeholder='Wprowadź opis nowego zadania i naciśnij Enter'
                    required
                    variant='outlined'
                  />
                </Grid>
              </Grid>
            </Grid>
          </Grid>
        </div>
      </div>
    </Grid>
  );
};

export default ShowTodo;
