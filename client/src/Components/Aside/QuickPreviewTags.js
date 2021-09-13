import React, { useContext, useEffect, useState } from 'react';
import {
  makeStyles,
  Grid,
  Chip,
  Divider,
  Avatar,
  Typography,
} from '@material-ui/core';
import { LosTodosContext } from '../../App';
import { Link } from 'react-router-dom';

const useStyles = makeStyles((theme) => ({
  list: {
    '& .MuiChip-root': {
      marginTop: 5,
      marginBottom: 5,
    },
  },
  divider: {
    width: '100%',
    marginBottom: 20,
    marginTop: 10,
  },
  title: {
    marginTop: 10,
  },
}));

const QuickPreviewTags = () => {
  const styles = useStyles();

  const { myTags } = useContext(LosTodosContext);

  const [popularTags, setPopularTags] = useState([]);

  useEffect(() => {
    let tags = myTags.sort((a, b) => b.todoSets.length - a.todoSets.length);
    setPopularTags(tags);
  }, [myTags]);

  return (
    <Grid container item>
      <Typography variant='h5' className={styles.title}>
        Popularne tagi
      </Typography>
      <Divider className={styles.divider} />
      <Grid container className={styles.list}>
        {popularTags.map((tag, index) => (
          <>
            {index < 10 ? (
              <Link
                style={{ textDecoration: 'none' }}
                to={{ pathname: `/tag/${tag.id}` }}
              >
                <Chip
                  avatar={<Avatar>#</Avatar>}
                  label={tag.name}
                  onClick={(e) => e.preventDefault}
                />
              </Link>
            ) : (
              <></>
            )}
          </>
        ))}
      </Grid>
    </Grid>
  );
};

export default QuickPreviewTags;
