import React, { useState, useContext, useEffect } from 'react';
import {
  Drawer as MaterialDrawer,
  Divider,
  Button,
  Grid,
} from '@material-ui/core';
import { LosTodosContext } from '../../App';
import { groupByDate } from '../../Helpers/Helpers';
import httpClient from '../../API/httpClient';
import DrawerSearch from './DrawerSearch';
import DrawerTodos from './DrawerTodos';
import HomeIcon from '@material-ui/icons/Home';
import NavbarUserButton from '../Navbar/NavbarUserButton';
import { Link, useLocation } from 'react-router-dom';

const MaterialDrawerResponsive = ({
  styles,
  toggledOnMobile,
  setToggledOnMobile,
  mobile,
}) => {
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
      await httpClient.TodoSets.remove(todo.id);
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
    <MaterialDrawer
      variant={mobile ? 'temporary' : 'permanent'}
      open={toggledOnMobile}
      onClose={() => setToggledOnMobile(!toggledOnMobile)}
      classes={{
        paper: styles.drawer,
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
      <DrawerTodos removeEntry={removeEntry} groupedTodos={groupedTodos} />
      {location.pathname !== '/' ? (
        <Button
          className={styles.backToAddTodo}
          startIcon={<HomeIcon className={styles.homeIcon} />}
          variant='contained'
          color='secondary'
        >
          <Link className={styles.backToAddTodoLink} to={{ pathname: '/' }}>
            Powrót do strony głównej
          </Link>
        </Button>
      ) : (
        <></>
      )}
      {mobile && (
        <Grid
          container
          alignItems='center'
          justify='center'
          className={styles.navbarUserButton}
        >
          <Grid item>
            <NavbarUserButton />
          </Grid>
        </Grid>
      )}
    </MaterialDrawer>
  );
};

export default MaterialDrawerResponsive;
