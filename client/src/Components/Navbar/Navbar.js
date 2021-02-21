import React from 'react';
import { makeStyles, AppBar, Toolbar, Grid } from '@material-ui/core';
import NavbarLogo from './NavbarLogo';
import NavbarBreadcrumb from './NavbarBreadcrumb';
import NavbarUserButton from './NavbarUserButton';

const useStyles = makeStyles((theme) => ({
  root: {
    zIndex: theme.zIndex.drawer + 1,
    paddingTop: 10,
    paddingBottom: 10,
    background:
      'transparent linear-gradient(93deg, #FD3F3F 0%, #510000 100%) 0% 0% no-repeat padding-box',
  },
  navbarLogo: {},
  navbarBreadcrumb: {
    flexGrow: 1,
  },
  navbarUserButton: {},
}));

const Navbar = ({ toggledOnMobile, setToggledOnMobile }) => {
  const styles = useStyles();

  return (
    <AppBar position='fixed' className={styles.root}>
      <Toolbar>
        <Grid container>
          <Grid item className={styles.navbarLogo}>
            <NavbarLogo
              toggledOnMobile={toggledOnMobile}
              setToggledOnMobile={setToggledOnMobile}
            />
          </Grid>
          <Grid item className={styles.navbarBreadcrumb}>
            <NavbarBreadcrumb />
          </Grid>
          <Grid item className={styles.navbarUserButton}>
            <NavbarUserButton />
          </Grid>
        </Grid>
      </Toolbar>
    </AppBar>
  );
};

export default Navbar;
