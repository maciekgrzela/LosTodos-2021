import React, { useState } from 'react';
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

const useStyles = makeStyles((theme) => ({
  root: {},
  userButton: {
    backgroundColor: 'white',
    padding: '5px 20px',
    '&:hover': {
      backgroundColor: 'white',
    },
  },
  userList: {
    '& .MuiMenu-paper': {
      top: '75px !important',
    },
    '& .MuiMenuItem-root': {
      paddingRight: 50,
    },
  },
}));

const NavbarUserButton = ({ setLogged, user }) => {
  const styles = useStyles();
  const [userMenuAnchorElement, setUserMenuAnchorElement] = useState(null);

  return (
    <Grid container className={styles.root}>
      <Grid item>
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
            style={{ marginRight: 10 }}
            alt={`${user.firstName} ${user.lastName}`}
            src='https://randomuser.me/api/portraits/men/75.jpg'
          />
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
            <Typography variant='inherit'>Moje konto</Typography>
          </MenuItem>
          <MenuItem
            onClick={() => {
              setLogged(false);
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
