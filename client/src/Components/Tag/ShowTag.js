import React, { useContext, useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import {
  makeStyles,
  Grid,
  Typography,
  Divider,
  List,
  ListItem,
  ListItemAvatar,
  Avatar,
  ListItemText,
} from '@material-ui/core';
import { LosTodosContext } from '../../App';
import PlaylistAddCheckIcon from '@material-ui/icons/PlaylistAddCheck';

const useStyles = makeStyles((theme) => ({
  root: {
    padding: '120px 40px 20px 40px',
  },
  list: {
    flexBasis: '100%',
    '& .MuiListItem-root:hover': {
      backgroundColor: 'rgba(112,112,112, 0.13)',
      borderRadius: 40,
    },
    '& .MuiAvatar-root': {
      backgroundColor: theme.palette.primary.main,
    },
  },
}));

const ShowTag = () => {
  const { id } = useParams();
  const styles = useStyles();

  const { myTags } = useContext(LosTodosContext);

  const [currentTag, setCurrentTag] = useState(null);

  useEffect(() => {
    const index = myTags.findIndex((p) => p.id === id);
    if (index !== -1) {
      setCurrentTag(myTags[index]);
    } else {
      console.log('Zgłoś błąd');
    }
  }, [myTags, id]);

  return (
    <Grid container item className={styles.root}>
      <Grid item style={{ flexBasis: '100%' }}>
        <Typography variant='h4'>
          Listy zadań dla: {currentTag !== null ? `#${currentTag.name}` : ''}
        </Typography>
        <Divider />
        {currentTag !== null ? (
          <List className={styles.list}>
            {currentTag.todoSets.map((todoSet) => (
              <Link
                style={{ textDecoration: 'none', color: '#000' }}
                to={{ pathname: `/edit/${todoSet.id}` }}
              >
                <ListItem>
                  <ListItemAvatar>
                    <Avatar>
                      <PlaylistAddCheckIcon />
                    </Avatar>
                  </ListItemAvatar>
                  <ListItemText
                    primary={todoSet.name}
                    secondary={`Utworzona: ${new Intl.DateTimeFormat('pl-PL', {
                      year: 'numeric',
                      month: 'long',
                      day: '2-digit',
                      hour: 'numeric',
                      minute: 'numeric',
                      second: 'numeric',
                    }).format(new Date(todoSet.created))}`}
                  />
                </ListItem>
              </Link>
            ))}
          </List>
        ) : (
          <span>Przecież tu nikogo nie ma :(</span>
        )}
      </Grid>
    </Grid>
  );
};

export default ShowTag;
