import React from 'react';
import { makeStyles, Grid } from '@material-ui/core';
import AsideCalendar from './AsideCalendar';
import AsideStats from './AsideStats';
import AsideTags from './AsideTags';

const useStyles = makeStyles((theme) => ({
  root: {},
}));

const Aside = () => {
  const styles = useStyles();
  return (
    <Grid container item className={styles.root}>
      <Grid item>
        <AsideCalendar />
      </Grid>
      <Grid item>
        <AsideTags />
      </Grid>
      <Grid item>
        <AsideStats />
      </Grid>
    </Grid>
  );
};

export default Aside;
