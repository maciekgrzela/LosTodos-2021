import React, { useContext, useState, useEffect } from 'react';
import {
  makeStyles,
  Grid,
  Typography,
  Accordion,
  AccordionSummary,
  AccordionDetails,
  List,
  ListItem,
  ListItemText,
  IconButton,
  Button,
} from '@material-ui/core';
import TodayIcon from '@material-ui/icons/Today';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import AccessAlarmIcon from '@material-ui/icons/AccessAlarm';
import HighlightOffIcon from '@material-ui/icons/HighlightOff';
import ArrowForwardIcon from '@material-ui/icons/ArrowForward';
import httpClient from '../../API/httpClient';
import { LosTodosContext } from '../../App';
import { Link } from 'react-router-dom';
import { groupByDate } from '../../Helpers/Helpers';

const useStyles = makeStyles((theme) => ({
  root: {},
  heading: {
    textTransform: 'uppercase',
    marginTop: 15,
    marginBottom: 10,
    color: 'white',
  },
  todosContainer: {
    maxHeight: 400,
    overflowX: 'hidden',
    overflowY: 'auto',
  },
  accordion: {
    backgroundColor: 'transparent',
    flexBasis: '100%',
    boxShadow: 'none',
    color: 'white',
    '&::before': {
      content: 'none',
    },
    '& .MuiAccordionSummary-root': {
      paddingLeft: 0,
      paddingRight: 0,
    },
    '& .MuiAccordionSummary-expandIcon': {
      color: 'white',
    },
  },
  accordionSummary: {
    flexGrow: '1',
  },
  accordionHeadingIcon: {
    marginRight: 10,
  },
  todos: {
    paddingTop: 0,
    paddingBottom: 0,
    width: '100%',
  },
  todoItem: {
    borderRadius: 30,
  },
  accordionDetails: {
    padding: 0,
  },
  todoName: {
    marginRight: 10,
    alignItems: 'center',
    justifyContent: 'space-between',
    position: 'relative',
    '& a': {
      color: '#fff',
      position: 'absolute',
    },
  },
}));

const DrawerTodos = () => {
  const styles = useStyles();
  const [groupedTodos, setGroupedTodos] = useState({});
  const { myTodoLists, removeTodoList } = useContext(LosTodosContext);

  useEffect(() => {
    setGroupedTodos(groupByDate(myTodoLists));
  }, [myTodoLists]);

  const removeEntry = async (todo) => {
    try {
      await httpClient.TaskSets.remove(todo.id);
      removeTodoList(todo.id);
    } catch (e) {
      console.log(e);
    }
  };

  return (
    <Grid container item className={styles.root}>
      <Typography className={styles.heading} variant='h5'>
        Moje TODOS:
      </Typography>
      <Grid container className={styles.todosContainer}>
        {Object.entries(groupedTodos).map(([key, value]) => (
          <Accordion className={styles.accordion}>
            <AccordionSummary
              className={styles.accordionSummary}
              expandIcon={<ExpandMoreIcon />}
              aria-controls='panel1a-content'
              id='panel1a-header'
            >
              <Grid container alignItems='center'>
                <TodayIcon className={styles.accordionHeadingIcon} />
                <Typography className={styles.accordionHeading}>
                  {key}
                </Typography>
              </Grid>
            </AccordionSummary>
            <AccordionDetails className={styles.accordionDetails}>
              <List className={styles.todos} component='nav'>
                {value.map((todo) => (
                  <ListItem button className={styles.todoItem}>
                    <ListItemText>
                      <Grid
                        item
                        container
                        alignItems='center'
                        justify='space-between'
                      >
                        <Grid container item className={styles.todoName}>
                          {todo.name.length > 20
                            ? `${todo.name.substr(0, 20)}...`
                            : todo.name}
                          <Link
                            style={{ display: 'flex', right: '35px' }}
                            to={{ pathname: '#' }}
                            onClick={(e) => {
                              e.preventDefault();
                              removeEntry(todo);
                            }}
                          >
                            <HighlightOffIcon style={{ fontSize: '1.2rem' }} />
                          </Link>
                          <Link
                            style={{ display: 'flex', right: '5px' }}
                            to={{ pathname: `/edit/${todo.id}` }}
                          >
                            <ArrowForwardIcon
                              style={{
                                fontSize: '1.2rem',
                              }}
                            />
                          </Link>
                        </Grid>
                      </Grid>
                    </ListItemText>
                  </ListItem>
                ))}
              </List>
            </AccordionDetails>
          </Accordion>
        ))}
      </Grid>
    </Grid>
  );
};

export default DrawerTodos;
