import { makeStyles, Grid } from '@material-ui/core';
import React, { useState } from 'react';
import Navbar from './Navbar/Navbar';
import Drawer from './Drawer/Drawer';
import AddTodo from './Todo/AddTodo';
import { Switch, useRouteMatch } from 'react-router-dom';
import ShowTodo from './Todo/ShowTodo';
import ShowTag from './Tag/ShowTag';
import ShowDate from './Date/ShowDate';
import MyData from './User/MyData';
import AuthRoute from '../AuthRoute';
import QuickPreview from './Aside/QuickPreview';

const useStyles = makeStyles((theme) => ({
  root: {
    display: 'flex',
    overflow: 'hidden',
  },
  drawer: {
    width: 300,
    flexShrink: 0,
    [theme.breakpoints.down('md')]: {
      width: 0,
    },
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
    overflowX: 'hidden',
    padding: '120px 40px 0 40px',
    [theme.breakpoints.down('sm')]: {
      height: '100%',
      overflow: 'hidden',
      padding: 30,
    },
    width: 200,
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
            md={8}
            sm={7}
            xs={12}
            container
          >
            <Switch>
              <AuthRoute exact path={path} component={AddTodo} />
              <AuthRoute path={`${path}edit/:id`} component={ShowTodo} />
              <AuthRoute path={`${path}tag/:id`} component={ShowTag} />
              <AuthRoute path={`${path}date/:id`} component={ShowDate} />
              <AuthRoute path={`${path}me`} component={MyData} />
            </Switch>
          </Grid>
          <Grid
            item
            md={4}
            sm={5}
            xs={12}
            className={styles.aside}
            container
            direction='column'
            justify='space-between'
          >
            <aside>
              <QuickPreview />
            </aside>
          </Grid>
        </Grid>
      </main>
    </div>
  );
};

export default Dashboard;
