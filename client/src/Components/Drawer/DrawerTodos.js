import React from 'react';
import {
  makeStyles,
  Grid,
  Typography,
  Accordion,
  AccordionSummary,
  AccordionDetails,
} from '@material-ui/core';
import TodayIcon from '@material-ui/icons/Today';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';

const useStyles = makeStyles((theme) => ({
  root: {},
  heading: {
    textTransform: 'uppercase',
    marginTop: 15,
    marginBottom: 10,
    color: 'white',
  },
  todosContainer: {
    maxHeight: 400,
    overflowX: 'hidden',
    overflowY: 'auto',
  },
  accordion: {
    backgroundColor: 'transparent',
    boxShadow: 'none',
    color: 'white',
    '&::before': {
      display: 'none',
    },
    '& .MuiAccordionSummary-root': {
      paddingLeft: 0,
      paddingRight: 0,
    },
    '& .MuiAccordionSummary-expandIcon': {
      color: 'white',
    },
  },
  accordionHeading: {
    paddingLeft: 10,
  },
}));

const DrawerTodos = () => {
  const styles = useStyles();

  return (
    <Grid container item className={styles.root}>
      <Typography className={styles.heading} variant='h5'>
        Moje TODOS:
      </Typography>
      <Grid container className={styles.todosContainer}>
        <Accordion className={styles.accordion}>
          <AccordionSummary
            expandIcon={<ExpandMoreIcon />}
            aria-controls='panel1a-content'
            id='panel1a-header'
          >
            <Grid container alignItems='center'>
              <TodayIcon />
              <Typography className={styles.accordionHeading}>
                2021-01-17
              </Typography>
            </Grid>
          </AccordionSummary>
          <AccordionDetails>
            <Typography>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit.
              Suspendisse malesuada lacus ex, sit amet blandit leo lobortis
              eget.
            </Typography>
          </AccordionDetails>
        </Accordion>
        <Accordion className={styles.accordion}>
          <AccordionSummary
            expandIcon={<ExpandMoreIcon />}
            aria-controls='panel2a-content'
            id='panel2a-header'
          >
            <Grid container alignItems='center'>
              <TodayIcon />
              <Typography className={styles.accordionHeading}>
                2021-01-16
              </Typography>
            </Grid>
          </AccordionSummary>
          <AccordionDetails>
            <Typography>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit.
              Suspendisse malesuada lacus ex, sit amet blandit leo lobortis
              eget.
            </Typography>
          </AccordionDetails>
        </Accordion>
        <Accordion className={styles.accordion}>
          <AccordionSummary
            expandIcon={<ExpandMoreIcon />}
            aria-controls='panel3a-content'
            id='panel3a-header'
          >
            <Grid container alignItems='center'>
              <TodayIcon />
              <Typography className={styles.accordionHeading}>
                2021-01-15
              </Typography>
            </Grid>
          </AccordionSummary>
          <AccordionDetails>
            <Typography>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit.
              Suspendisse malesuada lacus ex, sit amet blandit leo lobortis
              eget.
            </Typography>
          </AccordionDetails>
        </Accordion>
      </Grid>
    </Grid>
  );
};

export default DrawerTodos;
