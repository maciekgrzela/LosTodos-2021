import React from 'react';
import { Grid, Typography, Divider } from '@material-ui/core';

const AsideStats = () => {
  return (
    <Grid container item>
      <Typography style={{ marginTop: 10 }} variant='h5'>
        Wykresy produktywności
      </Typography>
      <Divider style={{ width: '100%', marginBottom: 20 }} />
    </Grid>
  );
};

export default AsideStats;
