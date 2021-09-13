import React, { useContext, useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import {
  Grid,
  Typography,
  TextField,
  Button,
  InputAdornment,
  Snackbar,
} from '@material-ui/core';
import { Face, LockOpen, PersonAdd } from '@material-ui/icons';
import { Alert } from '@material-ui/lab';
import { AiOutlineFacebook } from 'react-icons/ai';
import httpClient from '../../../API/httpClient';
import { LosTodosContext } from '../../../App';
import FacebookLogin from 'react-facebook-login/dist/facebook-login-render-props';
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
  facebookBtn: {
    color: '#fff',
    backgroundColor: '#3B5998',
    '&:hover': {
      backgroundColor: '#2F4779',
    },
  },
  registerBtn: {
    '& a': {
      textDecoration: 'none',
      color: 'unset',
    },
    marginBottom: 10,
    color: theme.palette.primary.main,
    backgroundColor: theme.palette.white.main,
  },
  form: {
    '& .MuiOutlinedInput-input': {
      padding: '13px 14px',
    },
    '& label': {
      transform: 'translate(14px, 12px) scale(1)',
    },
    '& .MuiInputBase-root': {
      color: '#fff',
      borderWidth: 4,
      '&::placeholder': {
        color: 'blue',
      },
    },
    '& label.Mui-focused': {
      color: theme.palette.white.main,
    },
    '& .MuiOutlinedInput-root': {
      '& fieldset': {
        borderColor: theme.palette.white.main,
        color: theme.palette.white.main,
      },
      '&:hover fieldset': {
        borderColor: theme.palette.white.main,
        color: theme.palette.white.main,
      },
      '&.Mui-focused fieldset': {
        borderColor: theme.palette.white.main,
        color: theme.palette.white.main,
      },
    },
  },
  loginBtn: {
    marginTop: 10,
    width: '100%',
    backgroundColor: theme.palette.white.main,
    paddingTop: 10,
    paddingBottom: 10,
    color: theme.palette.primary.main,
  },
}));

const Login = () => {
  const styles = useStyles();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [emailError, setEmailError] = useState('');
  const [passwordError, setPasswordError] = useState('');
  const [accessError, setAccessError] = useState('');

  const { login } = useContext(LosTodosContext);

  const handleFacebookLogin = async (response) => {
    try {
      const resource = {
        accessToken: response.accessToken,
      };
      const user = await httpClient.User.facebookLogin(resource);
      login(user);
    } catch (error) {
      if (error.response) {
        setAccessError(
          `Błąd: ${error.response.status}. ${error.response.data}`
        );
      } else if (error.request) {
        setAccessError('Błąd: Nie udało się wysłać żądania. Spróbuj ponownie');
      } else {
        setAccessError('Wystąpił nieoczekiwany błąd. Spróbuj ponownie');
      }
    }
  };

  const handleLogin = async () => {
    const emailRegEx =
      /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
    const passwordRegEx =
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})/;

    if (!email.match(emailRegEx)) {
      setEmailError('Niepoprawna struktura adresu e-mail');
      return;
    }

    if (!password.match(passwordRegEx)) {
      setPasswordError('Niepoprawna struktura hasła');
      return;
    }

    const credentials = {
      email: email,
      password: password,
    };

    try {
      const user = await httpClient.User.login(credentials);
      login(user);
    } catch (error) {
      if (error.response) {
        setAccessError(
          `Błąd: ${error.response.status}. ${error.response.data}`
        );
      } else if (error.request) {
        setAccessError('Błąd: Nie udało się wysłać żądania. Spróbuj ponownie');
      } else {
        setAccessError('Wystąpił nieoczekiwany błąd. Spróbuj ponownie');
      }
    }
  };

  return (
    <>
      <Snackbar
        anchorOrigin={{ vertical: 'bottom', horizontal: 'left' }}
        open={accessError !== ''}
        autoHideDuration={3000}
        onClose={() => setAccessError('')}
      >
        <Alert variant='filled' severity='warning'>
          {accessError}
        </Alert>
      </Snackbar>
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
              handleLogin();
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
              error={emailError !== '' ? true : false}
              helperText={emailError}
              label='Adres e-mail'
              variant='outlined'
              value={email}
              onFocus={() => setEmailError('')}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
            <TextField
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
              error={passwordError !== '' ? true : false}
              helperText={passwordError}
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              onFocus={() => setPasswordError('')}
              required
            />
            <Button
              type='submit'
              variant='contained'
              disabled={email === '' || password === ''}
              className={styles.loginBtn}
            >
              Zaloguj
            </Button>
          </form>
          <Grid container item style={{ marginTop: 20 }}>
            <Button
              fullWidth
              onClick={() => {}}
              className={styles.registerBtn}
              variant='contained'
              startIcon={<PersonAdd />}
            >
              <Link to='/register'>Przejdź do strony rejestracji</Link>
            </Button>
            <FacebookLogin
              appId='5102215729852842'
              callback={handleFacebookLogin}
              fields='name,email,birthday,picture'
              render={(renderProps) => (
                <Button
                  fullWidth
                  onClick={renderProps.onClick}
                  className={styles.facebookBtn}
                  variant='contained'
                  startIcon={<AiOutlineFacebook size='1.5rem' />}
                >
                  Zaloguj się za pomocą facebooka
                </Button>
              )}
            />
          </Grid>
        </Grid>
      </Grid>
    </>
  );
};

export default Login;
