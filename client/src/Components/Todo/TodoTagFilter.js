import React, { useState } from 'react';
import {
  Grid,
  List,
  ListItem,
  ListItemText,
  TextField,
  Chip,
  Avatar,
  makeStyles,
} from '@material-ui/core';

const useStyles = makeStyles((theme) => ({
  root: {
    '& .MuiGrid-item:nth-child(1)': {
      marginTop: 10,
      marginBottom: 10,
    },
  },
  tags: {
    '& .MuiChip-root': {
      margin: 5,
    },
  },
  foundList: {
    maxHeight: 200,
    overflow: 'auto',
    width: '100%',
    '& .MuiListItem-root': {
      borderRadius: 30,
    },
  },
}));

const TodoTagFilter = ({ options, selectedOptions, setSelectedOptions }) => {
  const styles = useStyles();
  const [foundOptions, setFoundOptions] = useState([]);
  const [inputText, setInputText] = useState(null);

  const handleFound = (event) => {
    const val = event.target.value;
    setInputText(
      val[val.length - 1] === ' ' ? val.substr(0, val.length - 1) : val
    );
    if (val === '') {
      setFoundOptions([]);
    } else {
      let found = options.filter((p) =>
        p.name.toLowerCase().includes(val.toLowerCase())
      );
      const currentValue = options.filter(
        (p) => p.name.toLowerCase() === val.toLowerCase()
      );
      let foundNames = [];
      found.forEach((p) => {
        foundNames.push(p.name);
      });
      if (currentValue.length === 0) {
        foundNames = [...foundNames, val];
      }

      setFoundOptions(foundNames);
    }
  };

  const handleFoundKey = (event) => {
    if (event.key === 'Enter') {
      const index = selectedOptions.findIndex((p) => p === foundOptions[0]);
      if (index === -1) {
        setSelectedOptions([...selectedOptions, foundOptions[0]]);
        setInputText('');
        setFoundOptions([]);
      }
    }
  };

  const removeTag = (option) => {
    const selectedTags = selectedOptions.filter((p) => p !== option);
    setSelectedOptions(selectedTags);
  };

  return (
    <Grid container direction='column' className={styles.root}>
      <Grid container item>
        {selectedOptions.length > 0 ? (
          <div className={styles.tags}>
            {selectedOptions.map((option) => (
              <Chip
                avatar={<Avatar>#</Avatar>}
                label={option}
                onClick={() => {
                  removeTag(option);
                }}
              />
            ))}
          </div>
        ) : (
          <></>
        )}
      </Grid>
      <Grid container item>
        <TextField
          fullWidth
          label='Wyszukaj tagi'
          variant='outlined'
          value={inputText}
          onKeyDown={(e) => handleFoundKey(e)}
          onChange={(e) => handleFound(e)}
        />
      </Grid>
      <Grid container item>
        <List component='nav' className={styles.foundList}>
          {foundOptions.map((option) => (
            <ListItem
              button
              onClick={() => {
                setSelectedOptions([...selectedOptions, option]);
                setInputText('');
                setFoundOptions([]);
              }}
            >
              <ListItemText primary={option} />
            </ListItem>
          ))}
        </List>
      </Grid>
    </Grid>
  );
};

export default TodoTagFilter;
