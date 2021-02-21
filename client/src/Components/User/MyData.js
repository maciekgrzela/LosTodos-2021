import React, { useContext, useEffect, useState, useCallback } from 'react';
import {
  Grid,
  Typography,
  Divider,
  makeStyles,
  TextField,
  Button,
  Snackbar,
} from '@material-ui/core';
import { LosTodosContext } from '../../App';
import httpClient from '../../API/httpClient';
import { Alert } from '@material-ui/lab';

const useStyles = makeStyles((theme) => ({
  root: {
    padding: '120px 40px 20px 40px',
  },
  myDataForm: {
    marginTop: 40,
    '& .MuiOutlinedInput-input': {
      padding: '10px 14px',
    },
    '& label': {
      transform: 'translate(14px, 12px) scale(1)',
    },
  },
  myDataContainer: {
    margin: '10px auto',
  },
  sendBtn: {
    paddingLeft: 30,
    paddingRight: 30,
    marginLeft: 10,
  },
  resetBtn: {
    backgroundColor: '#ffc400',
    paddingLeft: 30,
    paddingRight: 30,
    color: 'white',
    '&:hover': {
      backgroundColor: '#CC9C00',
      color: 'white',
    },
  },
}));

const MyData = () => {
  const styles = useStyles();

  const { user } = useContext(LosTodosContext);

  const [firstName, setFirstName] = useState(null);
  const [lastName, setLastName] = useState(null);
  const [email, setEmail] = useState(null);
  const [dateOfBirth, setDateOfBirth] = useState(null);
  const [phoneNumber, setPhoneNumber] = useState(null);

  const [validation, setValidation] = useState([]);

  const [updateInfo, setUpdateInfo] = useState([]);

  const setUserData = useCallback(() => {
    setFirstName(user.firstName);
    setLastName(user.lastName);
    setEmail(user.email);
    setDateOfBirth(Date.parse(user.dateOfBirth) < 0 ? null : user.dateOfBirth);
    setPhoneNumber(user.phoneNumber);
    setValidation([]);
  }, [user]);

  const updateUserData = async () => {
    const updateResource = {
      firstName: firstName,
      lastName: lastName,
      email: email,
      dateOfBirth: dateOfBirth,
      phoneNumber: phoneNumber,
      oldEmail: user.email,
    };

    try {
      await httpClient.User.update(updateResource);
      setUpdateInfo([
        {
          status: 'ok',
          text:
            'Dane użytkownika zostały zaktualizowane pomyślnie. Zmiana widoczna będzie od następnego zalogowania',
        },
      ]);
    } catch (e) {
      setUpdateInfo([
        {
          status: 'ok',
          text: 'Wystąpił błąd podczas aktualizacji danych użytkownika',
        },
      ]);
      console.log(e);
    }
  };

  useEffect(() => {
    setUserData();
  }, [setUserData]);

  return (
    <Grid container item className={styles.root}>
      <Snackbar
        anchorOrigin={{ vertical: 'bottom', horizontal: 'left' }}
        open={updateInfo.length !== 0}
        autoHideDuration={3000}
        onClose={() => setUpdateInfo([])}
      >
        <Alert
          variant='filled'
          severity={
            updateInfo.length !== 0 && updateInfo[0].status === 'ok'
              ? 'success'
              : 'warning'
          }
        >
          {updateInfo.length !== 0 && updateInfo[0].text}
        </Alert>
      </Snackbar>
      <Grid item style={{ flexBasis: '100%' }}>
        <Typography variant='h4'>Moje dane</Typography>
        <Divider />
        <form
          className={styles.myDataForm}
          onSubmit={(e) => {
            e.preventDefault();
            updateUserData();
          }}
        >
          <Grid
            container
            alignItems='center'
            className={styles.myDataContainer}
          >
            <Grid item xs={3} style={{ fontSize: '1.2rem' }}>
              Imię:
            </Grid>
            <Grid item xs={9}>
              <TextField
                type='text'
                error={
                  validation.filter((p) => p.name === 'firstName').length > 0
                }
                helperText={
                  validation.filter((p) => p.name === 'firstName')[0]?.text
                }
                value={firstName}
                onChange={(e) => {
                  if (e.target.value.length <= 0) {
                    const newValidation = validation.filter(
                      (p) => p.name !== 'firstName'
                    );
                    const firstNameValidation = {
                      name: 'firstName',
                      text: 'Pole musi zawierać co najmniej jeden znak',
                    };
                    setValidation([...newValidation, firstNameValidation]);
                  } else {
                    setValidation(
                      validation.filter((p) => p.name !== 'firstName')
                    );
                  }
                  setFirstName(e.target.value);
                }}
                autoComplete={false}
                fullWidth={true}
                placeholder='Wprowadź imię'
                required
                variant='outlined'
                autoFocus
              />
            </Grid>
          </Grid>
          <Grid
            container
            alignItems='center'
            className={styles.myDataContainer}
          >
            <Grid item xs={3} style={{ fontSize: '1.2rem' }}>
              Nazwisko:
            </Grid>
            <Grid item xs={9}>
              <TextField
                type='text'
                error={
                  validation.filter((p) => p.name === 'lastName').length > 0
                }
                helperText={
                  validation.filter((p) => p.name === 'lastName')[0]?.text
                }
                value={lastName}
                onChange={(e) => {
                  if (e.target.value.length <= 0) {
                    const newValidation = validation.filter(
                      (p) => p.name !== 'lastName'
                    );
                    const lastNameValidation = {
                      name: 'lastName',
                      text: 'Pole musi zawierać co najmniej jeden znak',
                    };
                    setValidation([...newValidation, lastNameValidation]);
                  } else {
                    setValidation(
                      validation.filter((p) => p.name !== 'lastName')
                    );
                  }
                  setLastName(e.target.value);
                }}
                autoComplete={false}
                fullWidth={true}
                placeholder='Wprowadź nazwisko'
                required
                variant='outlined'
                autoFocus
              />
            </Grid>
          </Grid>
          <Grid
            container
            alignItems='center'
            className={styles.myDataContainer}
          >
            <Grid item xs={3} style={{ fontSize: '1.2rem' }}>
              E-mail:
            </Grid>
            <Grid item xs={9}>
              <TextField
                type='text'
                value={email}
                error={validation.filter((p) => p.name === 'email').length > 0}
                helperText={
                  validation.filter((p) => p.name === 'email')[0]?.text
                }
                onChange={(e) => {
                  if (
                    !e.target.value.match(
                      /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i
                    )
                  ) {
                    const newValidation = validation.filter(
                      (p) => p.name !== 'email'
                    );
                    const emailValidation = {
                      name: 'email',
                      text: 'Zawartość pola posiada nieprawidłową strukturę',
                    };
                    setValidation([...newValidation, emailValidation]);
                  } else {
                    setValidation(validation.filter((p) => p.name !== 'email'));
                  }
                  setEmail(e.target.value);
                }}
                autoComplete={false}
                fullWidth={true}
                placeholder='Wprowadź email'
                required
                variant='outlined'
                autoFocus
              />
            </Grid>
          </Grid>
          <Grid
            container
            alignItems='center'
            className={styles.myDataContainer}
          >
            <Grid item xs={3} style={{ fontSize: '1.2rem' }}>
              Data urodzenia:
            </Grid>
            <Grid item xs={9}>
              <TextField
                fullWidth
                error={
                  validation.filter((p) => p.name === 'dateOfBirth').length > 0
                }
                helperText={
                  validation.filter((p) => p.name === 'dateOfBirth')[0]?.text
                }
                variant='outlined'
                type='date'
                onChange={(e) => {
                  const selectedDate = new Date(e.target.valueAsDate);
                  const nowDate = new Date();
                  if (selectedDate.getDate() > nowDate.getDate()) {
                    const newValidation = validation.filter(
                      (p) => p.name !== 'dateOfBirth'
                    );
                    const dateOfBirthValidation = {
                      name: 'dateOfBirth',
                      text: 'Należy wybrać datę przeszłą',
                    };
                    setValidation([...newValidation, dateOfBirthValidation]);
                  } else {
                    setValidation(
                      validation.filter((p) => p.name !== 'dateOfBirth')
                    );
                  }
                  console.log(e.target.value);
                  setDateOfBirth(e.target.value);
                }}
                value={
                  dateOfBirth !== null ? dateOfBirth.substr(0, 10) : dateOfBirth
                }
                className={styles.textField}
                required
              />
            </Grid>
          </Grid>
          <Grid
            container
            alignItems='center'
            className={styles.myDataContainer}
          >
            <Grid item xs={3} style={{ fontSize: '1.2rem' }}>
              Numer telefonu:
            </Grid>
            <Grid item xs={9}>
              <TextField
                type='text'
                error={
                  validation.filter((p) => p.name === 'phoneNumber').length > 0
                }
                helperText={
                  validation.filter((p) => p.name === 'phoneNumber')[0]?.text
                }
                value={phoneNumber}
                autoComplete={false}
                fullWidth={true}
                onChange={(e) => {
                  if (!e.target.value.match(/^[-]?\d{3}[-]?\d{3}[-]?\d{3}$/i)) {
                    const newValidation = validation.filter(
                      (p) => p.name !== 'phoneNumber'
                    );
                    const phoneNumberValidation = {
                      name: 'phoneNumber',
                      text:
                        'Zawartość pola posiada nieprawidłową strukturę (Nr. telefonu w formacie: XXX-XXX-XXX)',
                    };
                    setValidation([...newValidation, phoneNumberValidation]);
                  } else {
                    setValidation(
                      validation.filter((p) => p.name !== 'phoneNumber')
                    );
                  }
                  setPhoneNumber(e.target.value);
                }}
                placeholder='Wprowadź numer telefonu'
                required
                variant='outlined'
              />
            </Grid>
          </Grid>
          <Grid container justify='flex-end' className={styles.myDataContainer}>
            <Button
              variant='contained'
              className={styles.resetBtn}
              onClick={() => {
                setUserData();
              }}
            >
              Przywróć
            </Button>
            <Button
              className={styles.sendBtn}
              variant='contained'
              color='primary'
              type='submit'
              disabled={
                validation.length > 0 ||
                firstName === null ||
                lastName === null ||
                email === null ||
                dateOfBirth === null ||
                phoneNumber === null
              }
            >
              Aktualizuj
            </Button>
          </Grid>
        </form>
      </Grid>
    </Grid>
  );
};

export default MyData;
