import React from 'react';
import {
  makeStyles,
  Grid,
  Hidden,
  Drawer as MaterialDrawer,
  Toolbar,
  Divider,
} from '@material-ui/core';
import DrawerSearch from './DrawerSearch';
import DrawerTodos from './DrawerTodos';

const useStyles = makeStyles((theme) => ({
  root: {},
  drawer: {
    width: 300,
    backgroundColor: '#760D0D',
    padding: '70px 20px 0 20px',
  },
  divider: {
    width: '100%',
    backgroundColor: 'white',
  },
}));

const Drawer = ({ setLogged, toggledOnMobile, setToggledOnMobile }) => {
  const styles = useStyles();

  return (
    <Grid container className={styles.root}>
      <Grid item>
        <Hidden smUp implementation='css'>
          <MaterialDrawer
            container={window.document.body}
            variant='temporary'
            open={toggledOnMobile}
            onClose={() => setToggledOnMobile(!toggledOnMobile)}
            classes={{
              paper: styles.drawer,
            }}
            ModalProps={{
              keepMounted: true,
            }}
          >
            <DrawerSearch />
            <Divider className={styles.divider} />
            <DrawerTodos />
            {/* <DrawerContent setLogged={setLogged} styles={styles} /> */}
          </MaterialDrawer>
        </Hidden>
        <Hidden xsDown implementation='css'>
          <MaterialDrawer
            variant='permanent'
            classes={{
              paper: styles.drawer,
            }}
          >
            <Toolbar />
            <DrawerSearch />
            <Divider className={styles.divider} />
            <DrawerTodos />
            {/* <DrawerContent setLogged={setLogged} styles={styles} /> */}
          </MaterialDrawer>
        </Hidden>
      </Grid>
    </Grid>
  );
};

export default Drawer;
