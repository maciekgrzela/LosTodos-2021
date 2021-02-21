import React from 'react';
import ReactDOM from 'react-dom';
import { App } from './App';
import { ThemeProvider, createMuiTheme } from '@material-ui/core/styles';
import { createBrowserHistory } from 'history';
import CssBaseline from '@material-ui/core/CssBaseline';
import { Router } from 'react-router-dom';
import './Styles/index.css';
import './Styles/scrollbar.css';

const losTodosTheme = createMuiTheme({
  typography: {
    fontFamily: 'Bahnschrift, Signika, sans-serif',
    allVariants: {
      fontStretch: 'condensed',
    },
  },
  palette: {
    primary: {
      main: '#760D0D',
      light: '#913D3D',
    },
    secondary: {
      main: '#C2C2C2',
    },
    white: {
      main: '#FFFFFF',
    },
  },
  shape: {
    borderRadius: 30,
  },
});

const history = createBrowserHistory();

ReactDOM.render(
  <React.StrictMode>
    <ThemeProvider theme={losTodosTheme}>
      <CssBaseline />
      <Router history={history}>
        <App />
      </Router>
    </ThemeProvider>
  </React.StrictMode>,
  document.getElementById('root')
);
