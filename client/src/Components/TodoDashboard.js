import {
  makeStyles,
  Typography,
  Button,
  TextField,
  Grid,
  Divider,
  Avatar,
  Chip,
} from '@material-ui/core';
import React, { useState } from 'react';
import Aside from './Aside/Aside';
import Navbar from './Navbar/Navbar';
import Drawer from './Drawer/Drawer';
import AddTodo from './Todo/AddTodo';

const useStyles = makeStyles((theme) => ({
  root: {
    display: 'flex',
  },
  drawer: {
    width: 300,
    flexShrink: 0,
  },
  searchBox: {
    width: '100%',
  },
  content: {
    flexGrow: 1,
    padding: theme.spacing(3),
  },
  mainContent: {
    flexGrow: 1,
  },
  addTodo: {
    padding: '120px 40px 0 40px',
  },
  aside: {
    backgroundColor: 'rgba(112,112,112, 0.13)',
    height: '100vh',
    overflow: 'auto',
    padding: '120px 40px 0 40px',
    paddingBottom: 0,
  },
}));

const TodoDashboard = ({ setLogged }) => {
  const styles = useStyles();

  const [user, setUser] = useState({
    firstName: 'Maciej',
    lastName: 'Grzela',
    dateOfBirth: '1997-11-07T00:00:00.000000',
    phoneNumber: null,
    eMail: 'maciekgrzela45@gmail.com',
  });

  const [toggledOnMobile, setToggledOnMobile] = useState(false);

  return (
    <div className={styles.root}>
      <header>
        <Navbar
          user={user}
          setLogged={setLogged}
          toggledOnMobile={toggledOnMobile}
          setToggledOnMobile={setToggledOnMobile}
        />
      </header>
      <nav className={styles.drawer} aria-label='todos search'>
        <Drawer
          setLogged={setLogged}
          toggledOnMobile={toggledOnMobile}
          setToggledOnMobile={setToggledOnMobile}
        />
      </nav>
      <main className={styles.mainContent}>
        <Grid container>
          <Grid className={styles.addTodo} item md={9} sm={7} xs={12} container>
            <AddTodo />
          </Grid>
          <Grid
            item
            md={3}
            sm={5}
            xs={12}
            className={styles.aside}
            container
            direction='column'
            justify='space-between'
          >
            <aside>
              <Aside />
            </aside>
          </Grid>
        </Grid>
      </main>
    </div>
  );
};

export default TodoDashboard;
