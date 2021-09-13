import React from 'react';
import { makeStyles, Grid, Hidden } from '@material-ui/core';
import MaterialDrawerResponsive from './MaterialDrawerResponsive';

const useStyles = makeStyles((theme) => ({
  root: {},
  drawer: {
    width: 300,
    backgroundColor: '#760D0D',
    padding: '140px 20px 0 20px',
    [theme.breakpoints.down('md')]: {
      padding: 20,
      paddingBottom: 0,
    },
  },
  divider: {
    width: '100%',
    backgroundColor: 'white',
  },
  backToAddTodo: {
    backgroundColor: '#fff',
    marginTop: 10,
    '&:hover': {
      backgroundColor: 'rgba(255,255,255,0.9)',
    },
    '& .MuiButton-label': {
      display: 'flex',
      alignItems: 'end',
    },
  },
  backToAddTodoLink: {
    textDecoration: 'none',
    color: theme.palette.primary.dark,
  },
  homeIcon: {
    color: theme.palette.primary.dark,
  },
  navbarUserButton: {
    paddingTop: 30,
  },
}));

const Drawer = ({ toggledOnMobile, setToggledOnMobile }) => {
  const styles = useStyles();

  return (
    <Grid container className={styles.root}>
      <Grid item>
        <Hidden mdUp implementation='css'>
          <MaterialDrawerResponsive
            toggledOnMobile={toggledOnMobile}
            setToggledOnMobile={setToggledOnMobile}
            styles={styles}
            mobile={true}
          />
        </Hidden>
        <Hidden mdDown implementation='css'>
          <MaterialDrawerResponsive
            toggledOnMobile={toggledOnMobile}
            setToggledOnMobile={setToggledOnMobile}
            styles={styles}
            mobile={false}
          />
        </Hidden>
      </Grid>
    </Grid>
  );
};

export default Drawer;
