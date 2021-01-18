import React, { useState } from 'react';
import {
  makeStyles,
  Grid,
  TextField,
  Typography,
  Divider,
  Button,
  FormControl,
  MenuItem,
  Input,
  Chip,
  Select,
  InputLabel,
} from '@material-ui/core';
import HighlightOffIcon from '@material-ui/icons/HighlightOff';

const useStyles = makeStyles((theme) => ({
  root: {
    flexDirection: 'column',
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

const names = [
  'Oliver Hansen',
  'Van Henry',
  'April Tucker',
  'Ralph Hubbard',
  'Omar Alexander',
  'Carlos Abbott',
  'Miriam Wagner',
  'Bradley Wilkerson',
  'Virginia Andrews',
  'Kelly Snyder',
];

const AddTodo = () => {
  const styles = useStyles();

  const [todoListName, setTodoListName] = useState('');
  const [todoListTags, setTodoListTags] = useState([]);
  const [todos, setTodos] = useState(['']);
  const [personName, setPersonName] = React.useState([]);

  const addAnotherTodo = () => {
    setTodos([...todos, '']);
  };

  const removeTodo = (todo) => {
    const newTodos = todos.filter((p) => p !== todo);
    setTodos(newTodos);
  };

  const handleChange = (event) => {
    setPersonName(event.target.value);
  };

  const modifyTodo = (todo, updatedValue) => {
    const index = todos.findIndex((p) => p === todo);
    if (index !== -1) {
      let newTodos = [...todos];
      newTodos[index] = updatedValue;
      setTodos(newTodos);
    }
  };

  return (
    <Grid container item className={styles.root}>
      <Typography variant='h4'>Dodaj listę zadań</Typography>
      <Divider />
      <form
        className={styles.addTodoForm}
        onSubmit={(e) => {
          e.preventDefault();
        }}
      >
        <Grid container alignItems='center' className={styles.addTodoContainer}>
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
            <FormControl variant='outlined' className={styles.selectTags}>
              <Select
                multiple
                value={personName}
                onChange={handleChange}
                inputProps={{
                  id: 'outlined-age-native-simple',
                }}
                renderValue={(selected) => (
                  <div className={styles.chips}>
                    {selected.map((value) => (
                      <Chip key={value} label={value} className={styles.chip} />
                    ))}
                  </div>
                )}
              >
                {names.map((name) => (
                  <MenuItem key={name} value={name}>
                    {name}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
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
                  style={{ flexBasis: '80%' }}
                  autoComplete={false}
                  label='Zadanie'
                  placeholder='Wprowadź opis zadania'
                  required
                  variant='outlined'
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
        <Grid
          style={{ marginTop: 30, marginBottom: 30 }}
          container
          justify='flex-end'
        >
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
      <Divider />
    </Grid>
  );
};

export default AddTodo;
