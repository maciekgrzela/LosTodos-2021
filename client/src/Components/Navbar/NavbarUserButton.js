import React, { useState, useContext } from 'react';
import {
  makeStyles,
  Grid,
  Button,
  Avatar,
  Menu,
  MenuItem,
  ListItemIcon,
  Typography,
} from '@material-ui/core';
import ArrowDropDownIcon from '@material-ui/icons/ArrowDropDown';
import { AiOutlineLogout, AiOutlineUser } from 'react-icons/ai';
import { LosTodosContext } from '../../App';
import { Link } from 'react-router-dom';

const useStyles = makeStyles((theme) => ({
  root: {
    position: 'relative',
  },
  userButton: {
    width: 220,
    backgroundColor: 'white',
    padding: '5px 20px',
    '&:hover': {
      backgroundColor: 'white',
    },
  },
  userList: {
    '& .MuiMenu-paper': {
      width: 220,
    },
  },
}));

const NavbarUserButton = () => {
  const styles = useStyles();
  const { user, login } = useContext(LosTodosContext);
  const [userMenuAnchorElement, setUserMenuAnchorElement] = useState(null);

  return (
    <Grid container>
      <Grid item className={styles.root}>
        <Button
          className={styles.userButton}
          onClick={(e) => {
            setUserMenuAnchorElement(e.currentTarget);
          }}
          aria-controls='user-menu'
          aria-haspopup='true'
          endIcon={<ArrowDropDownIcon />}
          variant='contained'
        >
          <Avatar
            style={{ marginRight: 10, backgroundColor: '#FD3F3F' }}
            alt={`${user.firstName} ${user.lastName}`}
            src={user.photoUrl !== null ? user.photoUrl : ''}
          >
            {user.photoUrl === null
              ? `${user.firstName.substr(0, 1)}${user.lastName.substr(0, 1)}`
              : ''}
          </Avatar>
          <Typography>
            {user.firstName} {user.lastName}
          </Typography>
        </Button>
        <Menu
          id='user-menu'
          anchorEl={userMenuAnchorElement}
          keepMounted
          open={userMenuAnchorElement !== null}
          onClose={() => {
            setUserMenuAnchorElement(null);
          }}
          className={styles.userList}
        >
          <MenuItem
            onClick={() => {
              setUserMenuAnchorElement(null);
            }}
          >
            <ListItemIcon>
              <AiOutlineUser />
            </ListItemIcon>
            <Link
              style={{ textDecoration: 'none', color: '#000' }}
              to={{ pathname: '/me' }}
            >
              <Typography variant='inherit'>Moje konto</Typography>
            </Link>
          </MenuItem>
          <MenuItem
            onClick={() => {
              login(null);
            }}
          >
            <ListItemIcon>
              <AiOutlineLogout />
            </ListItemIcon>
            <Typography variant='inherit'>Wyloguj</Typography>
          </MenuItem>
        </Menu>
      </Grid>
    </Grid>
  );
};

export default NavbarUserButton;
