import React, { useState } from 'react';
import Main from './Main';

const App = () => {
  const [logged, setLogged] = useState(false);

  return <Main logged={logged} setLogged={setLogged} />;
};

export default App;
