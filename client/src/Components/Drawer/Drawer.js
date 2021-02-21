import React, { useState, useContext, useEffect } from 'react';
import {
  makeStyles,
  Grid,
  Hidden,
  Drawer as MaterialDrawer,
  Toolbar,
  Divider,
  Button,
} from '@material-ui/core';
import DrawerSearch from './DrawerSearch';
import DrawerTodos from './DrawerTodos';
import HomeIcon from '@material-ui/icons/Home';
import { Link, useLocation } from 'react-router-dom';
import { LosTodosContext } from '../../App';
import { groupByDate } from '../../Helpers/Helpers';
import httpClient from '../../API/httpClient';

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
}));

const Drawer = ({ toggledOnMobile, setToggledOnMobile }) => {
  const styles = useStyles();
  const location = useLocation();

  const [groupedTodos, setGroupedTodos] = useState({});
  const { myTodoLists, removeTodoList } = useContext(LosTodosContext);
  const [filteredTodos, setFilteredTodos] = useState([]);
  const [sortType, setSortType] = useState('date');
  const [sortContent, setSortContent] = useState('');

  useEffect(() => {
    setFilteredTodos(myTodoLists);
  }, [myTodoLists]);

  useEffect(() => {
    setGroupedTodos(groupByDate(filteredTodos));
  }, [filteredTodos]);

  const removeEntry = async (todo) => {
    try {
      await httpClient.TaskSets.remove(todo.id);
      removeTodoList(todo.id);
      setSortType('date');
      setSortContent('');
    } catch (e) {
      console.log(e);
    }
  };

  const filterTodos = () => {
    let filtered = [];
    if (sortContent === '') {
      setFilteredTodos(myTodoLists);
    } else {
      switch (sortType) {
        case 'date':
          filtered = myTodoLists.filter(
            (p) =>
              p.created.includes(sortContent) ||
              p.lastModified.includes(sortContent)
          );
          setFilteredTodos(filtered);
          break;
        case 'tag':
          filtered = myTodoLists.filter(
            (p) => p.tags.filter((t) => t.name === sortContent).length > 0
          );
          setFilteredTodos(filtered);
          break;
        case 'title':
          filtered = myTodoLists.filter((p) => p.name.includes(sortContent));
          setFilteredTodos(filtered);
          break;
        default:
          setFilteredTodos(myTodoLists);
          break;
      }
    }
  };

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
            <DrawerSearch
              sortContent={sortContent}
              setSortContent={setSortContent}
              sortType={sortType}
              setSortType={setSortType}
              filterTodos={filterTodos}
            />
            <Divider className={styles.divider} />
            <DrawerTodos
              removeEntry={removeEntry}
              groupedTodos={groupedTodos}
            />
            {location.pathname !== '/' ? (
              <Button
                className={styles.backToAddTodo}
                startIcon={<HomeIcon className={styles.homeIcon} />}
                variant='contained'
                color='secondary'
              >
                <Link
                  className={styles.backToAddTodoLink}
                  to={{ pathname: '/' }}
                >
                  Powrót do strony głównej
                </Link>
              </Button>
            ) : (
              <></>
            )}
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
            <DrawerSearch
              sortContent={sortContent}
              setSortContent={setSortContent}
              sortType={sortType}
              setSortType={setSortType}
              filterTodos={filterTodos}
            />
            <Divider className={styles.divider} />
            <DrawerTodos
              removeEntry={removeEntry}
              groupedTodos={groupedTodos}
            />
            {location.pathname !== '/' ? (
              <Button
                className={styles.backToAddTodo}
                variant='contained'
                color='secondary'
                startIcon={<HomeIcon className={styles.homeIcon} />}
              >
                <Link
                  className={styles.backToAddTodoLink}
                  to={{ pathname: '/' }}
                >
                  Powrót do strony głównej
                </Link>
              </Button>
            ) : (
              <></>
            )}
          </MaterialDrawer>
        </Hidden>
      </Grid>
    </Grid>
  );
};

export default Drawer;
