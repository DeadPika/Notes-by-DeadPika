import React from 'react';
import AuthForm from '../components/AuthForm';

const LoginPage = () => {
  return (
    <div className="container mx-auto p-4">
      <AuthForm type="login" />
    </div>
  );
};

export default LoginPage;