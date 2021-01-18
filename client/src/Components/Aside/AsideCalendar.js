import React, { useState } from 'react';
import { makeStyles, Grid, Typography, Divider } from '@material-ui/core';
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';

const useStyles = makeStyles((theme) => ({
  calendar: {
    width: '100%',
    background: 'transparent',
    border: 'none',
    fontFamily: 'Bahnschrift, Signika, sans-serif',
    '& .react-calendar__navigation__label': {
      fontFamily: 'Bahnschrift, Signika, sans-serif',
    },
    '& .react-calendar__month-view__days__day': {
      fontFamily: 'Bahnschrift, Signika, sans-serif',
    },
    '& .react-calendar__tile:enabled:hover, .react-calendar__tile:enabled:focus': {
      backgroundColor: '#ccc',
    },
    '& .react-calendar__tile': {
      borderRadius: 30,
    },
    '& .react-calendar__tile--now': {
      backgroundColor: 'white',
    },
    '& .react-calendar__tile--now:hover': {
      backgroundColor: 'white',
    },
    '& .react-calendar__tile--active': {
      backgroundColor: theme.palette.primary.main,
    },
    '& .react-calendar__tile--active:hover': {
      backgroundColor: theme.palette.primary.main,
    },
    '& .react-calendar__tile--active:focus': {
      backgroundColor: theme.palette.primary.main,
    },
  },
  divider: {
    width: '100%',
    marginBottom: 20,
    marginTop: 10,
  },
  title: {
    marginTop: 10,
  },
}));

const AsideCalendar = () => {
  const styles = useStyles();
  const [currentDate, setCurrentDate] = useState(new Date());

  return (
    <Grid container item>
      <Typography variant='h5' className={styles.title}>
        Zadania w kalendarzu
      </Typography>
      <Divider className={styles.divider} />
      <Calendar
        className={styles.calendar}
        value={currentDate}
        onChange={setCurrentDate}
      />
    </Grid>
  );
};

export default AsideCalendar;
