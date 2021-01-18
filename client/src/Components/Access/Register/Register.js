import React from 'react';
import { Link } from 'react-router-dom';

const Register = () => {
  return (
    <div>
      Rejestracja
      <Link to='/login'>Logowanie</Link>
    </div>
  );
};

export default Register;
