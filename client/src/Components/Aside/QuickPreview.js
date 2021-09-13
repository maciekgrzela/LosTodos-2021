import React from 'react';
import { makeStyles, Grid } from '@material-ui/core';
import QuickPreviewCalendar from './QuickPreviewCalendar';
import QuickPreviewTags from './QuickPreviewTags';
import QuickPreviewStats from './QuickPreviewStats';

const useStyles = makeStyles((theme) => ({
  root: {},
}));

const QuickPreview = () => {
  const styles = useStyles();
  return (
    <Grid container item className={styles.root}>
      <Grid item>
        <QuickPreviewCalendar />
      </Grid>
      <Grid item>
        <QuickPreviewTags />
      </Grid>
      <Grid item>
        <QuickPreviewStats />
      </Grid>
    </Grid>
  );
};

export default QuickPreview;
