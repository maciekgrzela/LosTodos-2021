import React, { useEffect, useContext, useState } from 'react';
import { useParams } from 'react-router-dom';
import {
  makeStyles,
  Grid,
  Typography,
  Divider,
  List,
  ListItem,
  ListItemAvatar,
  ListItemText,
  Avatar,
} from '@material-ui/core';
import { LosTodosContext } from '../../App';
import { Link } from 'react-router-dom';
import PlaylistAddCheckIcon from '@material-ui/icons/PlaylistAddCheck';
import IndeterminateCheckBoxIcon from '@material-ui/icons/IndeterminateCheckBox';
import CheckIcon from '@material-ui/icons/Check';

const useStyles = makeStyles((theme) => ({
  root: {
    padding: '120px 40px 20px 40px',
  },
  list: {
    flexBasis: '100%',
    '& .MuiListItem-root:hover': {
      backgroundColor: 'rgba(112,112,112, 0.13)',
      borderRadius: 40,
    },
    '& .MuiAvatar-root': {
      backgroundColor: theme.palette.primary.main,
    },
  },
  listLight: {
    flexBasis: '100%',
    '& .MuiListItem-root:hover': {
      backgroundColor: 'rgba(112,112,112, 0.13)',
      borderRadius: 40,
    },
    '& .MuiAvatar-root': {
      backgroundColor: '#FD3F3F',
    },
  },
  listMiddle: {
    flexBasis: '100%',
    '& .MuiListItem-root:hover': {
      backgroundColor: 'rgba(112,112,112, 0.13)',
      borderRadius: 40,
    },
    '& .MuiAvatar-root': {
      backgroundColor: '#CA3232',
    },
  },
}));

const ShowDate = () => {
  const { id } = useParams();
  const styles = useStyles();

  const { myTodoLists } = useContext(LosTodosContext);
  const [createdTodoLists, setCreatedTodoLists] = useState([]);
  const [checkedTodos, setCheckedTodos] = useState([]);
  const [createdTodos, setCreatedTodos] = useState([]);

  useEffect(() => {
    const currentDateString = new Date(id).toISOString();
    setCreatedTodoLists(
      myTodoLists.filter(
        (p) => p.created.substr(0, 10) === currentDateString.substr(0, 10)
      )
    );
    const allTodos = [];
    myTodoLists.forEach((list) => {
      list.tasks.forEach((task) => {
        task.taskSetId = list.id;
      });
      allTodos.push(...list.tasks);
    });
    setCheckedTodos(
      allTodos.filter(
        (p) =>
          p.lastChecked !== null &&
          p.lastChecked.substr(0, 10) === currentDateString.substr(0, 10)
      )
    );
    setCreatedTodos(
      allTodos.filter(
        (p) =>
          p.created.substr(0, 10) === currentDateString.substr(0, 10) &&
          p.lastChecked === null
      )
    );
  }, [myTodoLists, id]);

  return (
    <Grid container item className={styles.root}>
      <Grid item style={{ flexBasis: '100%' }}>
        <Typography variant='h4'>Dzień: {id}</Typography>
        <Divider />
        <Typography style={{ marginTop: 30 }} variant='h5'>
          Wykonane zadania: ({checkedTodos.length})
        </Typography>
        <Divider />
        {checkedTodos.length !== 0 ? (
          <List className={styles.listLight}>
            {checkedTodos.map((todo) => (
              <Link
                style={{ textDecoration: 'none', color: '#000' }}
                to={{ pathname: `/edit/${todo.taskSetId}` }}
              >
                {console.log(todo)}
                <ListItem>
                  <ListItemAvatar>
                    <Avatar>
                      <CheckIcon />
                    </Avatar>
                  </ListItemAvatar>
                  <ListItemText
                    primary={todo.name}
                    secondary={`Zaznaczono jako wykonane (lub odznaczono): ${new Intl.DateTimeFormat(
                      'pl-PL',
                      {
                        year: 'numeric',
                        month: 'long',
                        day: '2-digit',
                        hour: 'numeric',
                        minute: 'numeric',
                        second: 'numeric',
                      }
                    ).format(new Date(todo.lastChecked))}`}
                  />
                </ListItem>
              </Link>
            ))}
          </List>
        ) : (
          <span>Przecież tu nikogo nie ma :(</span>
        )}
        <Typography variant='h5'>
          Utworzone zadania niezrealizowane: ({createdTodos.length})
        </Typography>
        <Divider />
        {createdTodos.length !== 0 ? (
          <List className={styles.listMiddle}>
            {createdTodos.map((todo) => (
              <Link
                style={{ textDecoration: 'none', color: '#000' }}
                to={{ pathname: `/edit/${todo.taskSetId}` }}
              >
                <ListItem>
                  <ListItemAvatar>
                    <Avatar>
                      <IndeterminateCheckBoxIcon />
                    </Avatar>
                  </ListItemAvatar>
                  <ListItemText
                    primary={todo.name}
                    secondary={`Utworzono: ${new Intl.DateTimeFormat('pl-PL', {
                      year: 'numeric',
                      month: 'long',
                      day: '2-digit',
                      hour: 'numeric',
                      minute: 'numeric',
                      second: 'numeric',
                    }).format(new Date(todo.created))}`}
                  />
                </ListItem>
              </Link>
            ))}
          </List>
        ) : (
          <span>Przecież tu nikogo nie ma :(</span>
        )}
        <Typography variant='h5'>
          Utworzone listy zadań ({createdTodoLists.length}):
        </Typography>
        <Divider />
        {createdTodoLists.length !== 0 ? (
          <List className={styles.list}>
            {createdTodoLists.map((list) => (
              <Link
                style={{ textDecoration: 'none', color: '#000' }}
                to={{ pathname: `/edit/${list.id}` }}
              >
                <ListItem>
                  <ListItemAvatar>
                    <Avatar>
                      <PlaylistAddCheckIcon />
                    </Avatar>
                  </ListItemAvatar>
                  <ListItemText
                    primary={list.name}
                    secondary={`Utworzona: ${new Intl.DateTimeFormat('pl-PL', {
                      year: 'numeric',
                      month: 'long',
                      day: '2-digit',
                      hour: 'numeric',
                      minute: 'numeric',
                      second: 'numeric',
                    }).format(new Date(list.created))}`}
                  />
                </ListItem>
              </Link>
            ))}
          </List>
        ) : (
          <span>Przecież tu nikogo nie ma :(</span>
        )}
      </Grid>
    </Grid>
  );
};

export default ShowDate;
