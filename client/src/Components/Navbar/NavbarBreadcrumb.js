import React from 'react';
import {
  makeStyles,
  Grid,
  Breadcrumbs,
  Link,
  Typography,
} from '@material-ui/core';
import NavigateNextIcon from '@material-ui/icons/NavigateNext';

const useStyles = makeStyles((theme) => ({
  root: {
    height: '100%',
    alignItems: 'center',
  },
  container: {
    justifyContent: 'center',
  },
  breadcrumbs: {
    backgroundColor: 'rgba(0,0,0,0.1)',
    padding: '10px 20px',
    borderRadius: 30,
    color: theme.palette.white.main,
  },
}));

const NavbarBreadcrumb = () => {
  const styles = useStyles();

  return (
    <Grid container className={styles.root}>
      <Grid item container className={styles.container}>
        <Breadcrumbs
          separator={<NavigateNextIcon fontSize='small' />}
          aria-label='breadcrumb'
          className={styles.breadcrumbs}
        >
          <Link color='inherit' href='/' onClick={() => {}}>
            Jesteś tutaj
          </Link>
          <Typography>Strona Główna</Typography>
        </Breadcrumbs>
      </Grid>
    </Grid>
  );
};

export default NavbarBreadcrumb;
