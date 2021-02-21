import React from 'react';
import '../../Styles/checkmark.css';
import {
  Grid,
  makeStyles,
  Typography,
  Divider,
  Button,
} from '@material-ui/core';
import CheckmarkAnimation from '../Animation/CheckmarkAnimation';
import PlaylistAddIcon from '@material-ui/icons/PlaylistAdd';
import PlaylistAddCheckIcon from '@material-ui/icons/PlaylistAddCheck';
import TimelineIcon from '@material-ui/icons/Timeline';
import { Link } from 'react-router-dom';

const useStyles = makeStyles(() => ({
  root: {
    flexDirection: 'column',
    justifyContent: 'center',
    padding: '120px 40px 20px 40px',
  },
  animationWrapper: {},
  animation: {
    margin: '40px auto 0 auto',
    alignItems: 'center',
  },
  description: {
    fontSize: '1.15rem',
    display: 'block',
    padding: '0 60px',
  },
  status: {
    justifyContent: 'center',
    textAlign: 'center',
  },
  statusMsg: {
    marginBottom: 10,
  },
  actions: {
    marginTop: 20,
    justifyContent: 'center',
    '& button': {
      marginLeft: 10,
      marginRight: 10,
    },
  },
}));

const AddTodoResult = ({ result, refreshForm }) => {
  const styles = useStyles();

  return (
    <Grid container item className={styles.root}>
      <Grid item>
        <Typography variant='h4'>
          {result.status === 'ok' ? 'Tak jest!' : 'Coś poszło nie tak:('}
        </Typography>
        <Divider />
      </Grid>
      <Grid item container className={styles.animationWrapper}>
        {result.status === 'ok' ? (
          <Grid item className={styles.animation}>
            <CheckmarkAnimation />
          </Grid>
        ) : (
          <>{result.info}</>
        )}
      </Grid>
      <Grid item container className={styles.status}>
        <Grid item>
          <Typography variant='h4' className={styles.statusMsg}>
            {result.status === 'ok'
              ? 'Lista zadań została dodana!'
              : result.info}
          </Typography>
        </Grid>
        <Grid item>
          <Typography variant='p' className={styles.description}>
            {result.status === 'ok'
              ? 'Wybierz jedną z dostępnych poniżej opcji. Możesz stworzyć kolejną listę zadań, przeglądać wszystkie utworzone przez siebie listy, lub zerknąć na statystyki realizacji swoich todos!'
              : result.info}
          </Typography>
        </Grid>
      </Grid>
      <Grid item container className={styles.actions}>
        <Button variant='outlined' color='primary'>
          <Link
            style={{ textDecoration: 'none', color: '#760d0d' }}
            onClick={() => refreshForm()}
            to={{ pathname: '/' }}
          >
            <Grid item container direction='column'>
              <Grid item>
                <PlaylistAddIcon style={{ fontSize: '5.5rem' }} />
              </Grid>
              <Grid item>Nowa lista</Grid>
            </Grid>
          </Link>
        </Button>
        {result.status === 'ok' ? (
          <Button variant='outlined' color='primary'>
            <Link
              style={{ textDecoration: 'none', color: '#760d0d' }}
              to={{ pathname: `/edit/${result.info}` }}
            >
              <Grid item container direction='column'>
                <Grid item>
                  <PlaylistAddCheckIcon style={{ fontSize: '5.5rem' }} />
                </Grid>
                <Grid item>Zobacz listę</Grid>
              </Grid>
            </Link>
          </Button>
        ) : (
          <></>
        )}
      </Grid>
    </Grid>
  );
};

export default AddTodoResult;
