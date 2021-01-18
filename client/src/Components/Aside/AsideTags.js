import React from 'react';
import {
  makeStyles,
  Grid,
  Chip,
  Divider,
  Avatar,
  Typography,
} from '@material-ui/core';

const useStyles = makeStyles((theme) => ({
  list: {
    '& .MuiChip-root': {
      marginTop: 5,
      marginBottom: 5,
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

const AsideTags = () => {
  const styles = useStyles();
  return (
    <Grid container item>
      <Typography variant='h5' className={styles.title}>
        Popularne tagi
      </Typography>
      <Divider className={styles.divider} />
      <Grid container className={styles.list}>
        <Chip
          avatar={<Avatar>#</Avatar>}
          label='Gotowanie'
          onClick={() => {}}
        />
        <Chip avatar={<Avatar>#</Avatar>} label='Pranie' onClick={() => {}} />
        <Chip
          avatar={<Avatar>#</Avatar>}
          label='Produktywność'
          onClick={() => {}}
        />
        <Chip avatar={<Avatar>#</Avatar>} label='Nauka' onClick={() => {}} />
        <Chip avatar={<Avatar>#</Avatar>} label='Uczelnia' onClick={() => {}} />
        <Chip avatar={<Avatar>#</Avatar>} label='Sport' onClick={() => {}} />
        <Chip avatar={<Avatar>#</Avatar>} label='Rozwój' onClick={() => {}} />
        <Chip avatar={<Avatar>#</Avatar>} label='Porządki' onClick={() => {}} />
      </Grid>
    </Grid>
  );
};

export default AsideTags;
