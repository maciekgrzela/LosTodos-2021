import React, { useState, useEffect, useContext } from 'react';
import {
  Grid,
  Typography,
  Divider,
  TextField,
  makeStyles,
  Button,
} from '@material-ui/core';
import httpClient from '../../API/httpClient';
import AddCircleOutlineIcon from '@material-ui/icons/AddCircleOutline';
import CheckCircleOutlineIcon from '@material-ui/icons/CheckCircleOutline';
import { LosTodosContext } from '../../App';

const useStyles = makeStyles((theme) => ({
  inputField: {
    '& .MuiOutlinedInput-input': {
      padding: '10px 14px',
    },
    '& label': {
      transform: 'translate(14px, 12px) scale(1)',
    },
  },
}));

const AsideStats = () => {
  const [statDays, setStatDays] = useState(7);
  const [todosAdded, setTodosAdded] = useState(null);
  const [todosChecked, setTodosChecked] = useState(null);
  const styles = useStyles();

  const { myTodoLists } = useContext(LosTodosContext);

  useEffect(() => {
    const fetchData = async () => {
      try {
        let stats = await httpClient.Tasks.stats(Number(statDays));
        setTodosAdded(stats.taskCreated);
        setTodosChecked(stats.taskChecked);
      } catch (e) {
        console.log(e);
      }
    };

    fetchData();
  }, [statDays, myTodoLists]);

  return (
    <Grid container item>
      <Typography style={{ marginTop: 10 }} variant='h5'>
        Wykresy produktywności
      </Typography>
      <Divider style={{ width: '100%', marginBottom: 20 }} />
      <TextField
        type='number'
        value={statDays}
        className={styles.inputField}
        onChange={(e) => {
          setStatDays(e.target.valueAsNumber);
        }}
        autoComplete={false}
        fullWidth={true}
        label='Liczba dni - statystyki'
        placeholder='Wprowadź liczbę dni dla statystyk'
        required
        variant='outlined'
        autoFocus
      />
      {todosAdded !== null && todosChecked !== null ? (
        <Grid
          style={{
            flexBasis: '100%',
            marginTop: 10,
            marginBottom: 20,
            backgroundColor: 'rgb(206,206,206)',
            borderRadius: '30px',
          }}
          container
          item
        >
          <Grid
            container
            item
            style={{
              flexBasis: '100%',
              margin: '20px 20px 5px 20px',
              alignItems: 'center',
            }}
          >
            <AddCircleOutlineIcon
              style={{ flexBasis: '20%', color: '#760D0D' }}
            />
            <div
              style={{
                flexGrow: 1,
                background: '#760D0D',
                color: '#fff',
                borderRadius: 30,
                display: 'flex',
                justifyContent: 'center',
              }}
            >
              <span>{todosAdded}</span>
            </div>
          </Grid>
          <Grid
            container
            item
            style={{
              flexBasis: '100%',
              margin: '5px 20px 20px 20px',
              alignItems: 'center',
            }}
          >
            <CheckCircleOutlineIcon
              style={{ flexBasis: '20%', color: '#760D0D' }}
            />
            <div
              style={{
                flexBasis: `${
                  todosAdded === 0 && todosChecked === 0
                    ? '80%'
                    : `${(todosChecked / todosAdded) * 100}%`
                }`,
                background: '#760D0D',
                color: '#fff',
                borderRadius: 30,
                display: 'flex',
                justifyContent: 'center',
              }}
            >
              <span>{todosChecked}</span>
            </div>
          </Grid>
        </Grid>
      ) : (
        <></>
      )}
    </Grid>
  );
};

export default AsideStats;
