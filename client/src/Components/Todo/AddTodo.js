import React from 'react';
import {
  makeStyles,
  Grid,
  TextField,
  Typography,
  Divider,
  Button,
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
}));

const AddTodo = () => {
  const styles = useStyles();

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
            <TextField
              type='text'
              autoComplete={false}
              fullWidth={true}
              label='Tagi'
              placeholder='Wprowadź tagi opisujące listę zadań'
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
            Zadania:
          </Grid>
          <Grid item xs={9} container justify='space-between'>
            <Button variant='contained' color='primary'>
              <HighlightOffIcon />
            </Button>
            <TextField
              type='text'
              style={{ flexBasis: '80%' }}
              autoComplete={false}
              label='Zadanie'
              placeholder='Wprowadź opis zadania'
              variant='outlined'
            />
          </Grid>
        </Grid>
        <Grid style={{ marginTop: 30 }} container justify='center'>
          <Button
            style={{ flexBasis: '40%' }}
            variant='outlined'
            color='secondary'
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
