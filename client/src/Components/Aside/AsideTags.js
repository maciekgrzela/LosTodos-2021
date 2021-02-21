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

const AsideTags = () => {
  const styles = useStyles();

  const { myTags } = useContext(LosTodosContext);

  const [popularTags, setPopularTags] = useState([]);

  useEffect(() => {
    let tags = myTags.sort((a, b) => b.taskSets.length - a.taskSets.length);
    console.log(tags);
    setPopularTags(tags);
  }, [myTags]);

  return (
    <Grid container item>
      <Typography variant='h5' className={styles.title}>
        Popularne tagi
      </Typography>
      <Divider className={styles.divider} />
      <Grid container className={styles.list}>
        {popularTags.map((tag, index) => {
          if (index < 10) {
            return (
              <Link
                style={{ textDecoration: 'none' }}
                to={{ pathname: `/tag/${tag.id}` }}
              >
                <Chip
                  avatar={<Avatar>#</Avatar>}
                  label={tag.name}
                  onClick={() => {}}
                />
              </Link>
            );
          } else {
            return <></>;
          }
        })}
      </Grid>
    </Grid>
  );
};

export default AsideTags;
