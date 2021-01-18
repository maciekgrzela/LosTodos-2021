import React from 'react';
import { Link } from 'react-router-dom';

const ForgetPassword = () => {
  return (
    <div>
      Zapomniałem hasła
      <Link to='/login'>Logowanie</Link>
    </div>
  );
};

export default ForgetPassword;
