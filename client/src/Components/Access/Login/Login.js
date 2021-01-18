import React, { useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import {
  Grid,
  Typography,
  TextField,
  Button,
  InputAdornment,
} from '@material-ui/core';
import { Face, LockOpen } from '@material-ui/icons';
import {
  AiOutlineFacebook,
  AiOutlineGoogle,
  AiOutlineTwitter,
} from 'react-icons/ai';

import { Link } from 'react-router-dom';

const useStyles = makeStyles((theme) => ({
  wrapper: {
    height: '100vh',
    background: 'linear-gradient(173deg, #FD3F3F, #510000)',
  },
  loginContainer: {
    paddingLeft: 50,
    paddingRight: 50,
  },
  heading: {
    color: 'white',
    textTransform: 'uppercase',
  },
  subheading: {
    color: 'white',
    marginBottom: 30,
  },
  logo: {
    maxWidth: '200px',
    height: 'auto',
    transform: 'rotate(25deg)',
    alignSelf: 'center',
  },
  textField: {
    marginTop: 10,
    marginBottom: 10,
  },
  textFieldLabel: {
    color: '#fff',
  },
  socialBtn: {
    color: '#fff',
    minWidth: '30%',
  },
  facebookBtn: {
    backgroundColor: '#3B5998',
    '&:hover': {
      backgroundColor: '#2F4779',
    },
  },
  twitterBtn: {
    backgroundColor: '#00ACEE',
    '&:hover': {
      backgroundColor: '#0089BE',
    },
  },
  googleBtn: {
    backgroundColor: '#F44336',
    '&:hover': {
      backgroundColor: '#C3352B',
    },
  },
  form: {
    '& .MuiInputBase-root': {
      color: '#fff',
      borderWidth: 4,
      '&::placeholder': {
        color: 'blue',
      },
    },
    '& label.Mui-focused': {
      color: 'white',
    },
    '& .MuiOutlinedInput-root': {
      '& fieldset': {
        borderColor: 'white',
        color: 'white',
      },
      '&:hover fieldset': {
        borderColor: 'white',
        color: 'white',
      },
      '&.Mui-focused fieldset': {
        borderColor: 'white',
        color: 'white',
      },
    },
  },
  loginBtn: {
    marginTop: 10,
    width: '100%',
    backgroundColor: theme.palette.white.main,
    paddingTop: 15,
    paddingBottom: 15,
    color: theme.palette.primary.main,
  },
}));

const Login = ({ setLogged }) => {
  const styles = useStyles();

  const [email, setEmail] = useState(null);
  const [password, setPassword] = useState(null);

  const login = () => {
    if (email !== null && password !== null) {
      setLogged(true);
    }
  };

  return (
    <Grid
      className={styles.wrapper}
      justify='center'
      alignItems='center'
      container
    >
      <Grid
        item
        xs={12}
        sm={8}
        md={4}
        container
        justify='center'
        direction='column'
        className={styles.loginContainer}
      >
        <img
          className={styles.logo}
          src='/assets/img/logo.svg'
          alt='LosTodosLogo'
        />
        <Typography variant='h3' className={styles.heading} align='center'>
          Los Todos
        </Typography>
        <Typography variant='h5' className={styles.subheading} align='center'>
          System porządkowania Twojego dnia!
        </Typography>
        <form
          onSubmit={(e) => {
            e.preventDefault();
            login();
          }}
          className={styles.form}
          autoComplete='off'
        >
          <TextField
            className={styles.textField}
            InputLabelProps={{
              className: styles.textFieldLabel,
            }}
            fullWidth
            InputProps={{
              startAdornment: (
                <InputAdornment position='start'>
                  <Face />
                </InputAdornment>
              ),
            }}
            type='e-mail'
            placeholder='Wprowadź adres e-mail'
            label='Adres e-mail'
            variant='outlined'
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
          <TextField
            color='#fff'
            className={styles.textField}
            InputLabelProps={{
              className: styles.textFieldLabel,
            }}
            InputProps={{
              startAdornment: (
                <InputAdornment position='start'>
                  <LockOpen />
                </InputAdornment>
              ),
            }}
            fullWidth
            type='password'
            placeholder='Wprowadź hasło'
            label='Hasło'
            variant='outlined'
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
          <Button type='submit' variant='contained' className={styles.loginBtn}>
            Zaloguj
          </Button>
        </form>
        <Link style={{ color: '#fff', marginTop: 20 }} to='/register'>
          Zarejestruj się
        </Link>
        <Link style={{ color: '#fff' }} to='/forget/password'>
          Zapomniałem hasła
        </Link>
        <Grid item container justify='space-between' style={{ marginTop: 20 }}>
          <Button
            className={[styles.socialBtn, styles.facebookBtn]}
            variant='contained'
          >
            <AiOutlineFacebook size='1.5rem' />
          </Button>
          <Button
            className={[styles.socialBtn, styles.googleBtn]}
            variant='contained'
          >
            <AiOutlineGoogle size='1.5rem' />
          </Button>
          <Button
            className={[styles.socialBtn, styles.twitterBtn]}
            variant='contained'
          >
            <AiOutlineTwitter size='1.5rem' />
          </Button>
        </Grid>
      </Grid>
    </Grid>
  );
};

export default Login;
