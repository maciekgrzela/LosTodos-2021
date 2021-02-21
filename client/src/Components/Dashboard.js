import { makeStyles, Grid } from '@material-ui/core';
import React, { useContext, useState } from 'react';
import Aside from './Aside/Aside';
import Navbar from './Navbar/Navbar';
import Drawer from './Drawer/Drawer';
import AddTodo from './Todo/AddTodo';
import { LosTodosContext } from '../App';
import { Route, Switch, useRouteMatch, Link } from 'react-router-dom';
import ShowTodo from './Todo/ShowTodo';
import ShowTag from './Tag/ShowTag';
import ShowDate from './Date/ShowDate';
import MyData from './User/MyData';

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
  todoMainView: {
    height: '100vh',
    overflow: 'auto',
  },
  aside: {
    backgroundColor: 'rgba(112,112,112, 0.13)',
    height: '100vh',
    overflow: 'auto',
    padding: '120px 40px 0 40px',
    paddingBottom: 0,
  },
}));

const Dashboard = () => {
  const styles = useStyles();
  const [toggledOnMobile, setToggledOnMobile] = useState(false);

  let { path } = useRouteMatch();

  return (
    <div className={styles.root}>
      <header>
        <Navbar
          toggledOnMobile={toggledOnMobile}
          setToggledOnMobile={setToggledOnMobile}
        />
      </header>
      <nav className={styles.drawer} aria-label='todos search'>
        <Drawer
          toggledOnMobile={toggledOnMobile}
          setToggledOnMobile={setToggledOnMobile}
        />
      </nav>
      <main className={styles.mainContent}>
        <Grid container>
          <Grid
            className={styles.todoMainView}
            item
            md={9}
            sm={7}
            xs={12}
            container
          >
            <Switch>
              <Route exact path={path}>
                <AddTodo />
              </Route>
              <Route path={`${path}edit/:id`}>
                <ShowTodo />
              </Route>
              <Route path={`${path}tag/:id`}>
                <ShowTag />
              </Route>
              <Route path={`${path}date/:id`}>
                <ShowDate />
              </Route>
              <Route path={`${path}me`}>
                <MyData />
              </Route>
            </Switch>
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

export default Dashboard;
