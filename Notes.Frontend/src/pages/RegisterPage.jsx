import React from 'react';
import AuthForm from '../components/AuthForm';

const RegisterPage = () => {
  return (
    <div className="container mx-auto p-4">
      <AuthForm type="register" />
    </div>
  );
};

export default RegisterPage;