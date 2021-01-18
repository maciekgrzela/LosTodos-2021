import React from 'react';
import {
  makeStyles,
  Grid,
  Typography,
  IconButton,
  Hidden,
} from '@material-ui/core';
import MenuIcon from '@material-ui/icons/Menu';

const useStyles = makeStyles((theme) => ({
  root: {},
  logoImg: {
    maxWidth: 50,
    height: 'auto',
    transform: 'rotate(25deg)',
  },
  logoText: {
    textTransform: 'uppercase',
    marginLeft: 10,
    fontSize: '2rem',
  },
  logoBtn: {
    [theme.breakpoints.up('sm')]: {
      display: 'none',
    },
  },
}));

const NavbarLogo = ({ toggledOnMobile, setToggledOnMobile }) => {
  const styles = useStyles();

  return (
    <Grid container className={styles.root} alignItems='center'>
      <Grid item>
        <IconButton
          color='inherit'
          aria-label='open drawer'
          edge='start'
          onClick={() => setToggledOnMobile(!toggledOnMobile)}
          className={styles.logoBtn}
        >
          <MenuIcon />
        </IconButton>
      </Grid>
      <Grid item>
        <img
          className={styles.logoImg}
          src='/assets/img/logo.svg'
          alt='LosTodosLogo'
        />
      </Grid>
      <Grid item>
        <Typography variant='h6' className={styles.logoText}>
          Los Todos
        </Typography>
      </Grid>
    </Grid>
  );
};

export default NavbarLogo;
