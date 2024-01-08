import { Box, Container, Typography } from '@mui/material';
import { styled } from '@mui/material/styles';
import { FormProvider, SubmitHandler, useForm } from 'react-hook-form';
import { object, string, TypeOf } from 'zod';
import { zodResolver } from '@hookform/resolvers/zod';
import FormInput from '../components/FormInput';
import { useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useRegisterUserMutation } from '../redux/api/authApi';
import { LoadingButton as _LoadingButton } from '@mui/lab';
import { toast } from 'react-toastify';

const LoadingButton = styled(_LoadingButton)`
  padding: 0.6rem 0;
  background-color: #f9d13e;
  color: #2363eb;
  font-weight: 500;

  &:hover {
    background-color: #ebc22c;
    transform: translateY(-2px);
  }
`;

const LinkItem = styled(Link)`
  text-decoration: none;
  color: #2363eb;
  &:hover {
    text-decoration: underline;
  }
`;

const registerSchema = object({
    username: string(),
    email: string(),
    password: string(),
    confirmPassword: string()
});

export type RegisterInput = TypeOf<typeof registerSchema>;

const RegisterPage = () => {
  const methods = useForm<RegisterInput>({
    resolver: zodResolver(registerSchema),
  });

  // ? Calling the Register Mutation
  const [registerUser, { isLoading, isSuccess, error, isError }] =
    useRegisterUserMutation();

  const navigate = useNavigate();

  const {
    reset,
    handleSubmit,
    formState: { isSubmitSuccessful },
  } = methods;

  useEffect(() => {
    if (isSuccess) {
      toast.success('User registered successfully');
      navigate("/login")
    }

    if (isError) {
      console.log(error);
      if (Array.isArray((error as any).data.error)) {
        (error as any).data.error.forEach((el: any) =>
          toast.error(el.message, {
            position: 'top-right',
          })
        );
      } else {
        toast.error((error as any).data.message, {
          position: 'top-right',
        });
      }
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isLoading]);

  useEffect(() => {
    if (isSubmitSuccessful) {
      reset();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isSubmitSuccessful]);

  const onSubmitHandler: SubmitHandler<RegisterInput> = (values) => {
    console.log(values);
    // ? Executing the RegisterUser Mutation
    registerUser(values).then((result: any) => {
      console.log(JSON.stringify(result));
      if (!result.data?.success) {
        toast.error(JSON.stringify(result), {
          position: 'top-right',
        })
      }
    });
  };

  return (
    <Container
      maxWidth={false}
      sx={{
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        height: '100vh',
        backgroundColor: '#000',
      }}
    >
      <Box
        sx={{
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
          flexDirection: 'column',
        }}
      >
        <Typography
          textAlign='center'
          component='h1'
          sx={{
            color: '#f9d13e',
            fontSize: { xs: '1.5rem', md: '2rem' },
            fontWeight: 600,
            mb: 2,
            letterSpacing: 1,
          }}
        >
          Welcome to movie hub!
        </Typography>
        <Typography component='h2' sx={{ color: '#e5e7eb', mb: 2 }}>
          Register new account
        </Typography>

        <FormProvider {...methods}>
          <Box
            component='form'
            onSubmit={handleSubmit(onSubmitHandler)}
            noValidate
            autoComplete='off'
            maxWidth='27rem'
            width='100%'
            sx={{
              backgroundColor: '#e5e7eb',
              p: { xs: '1rem', sm: '2rem' },
              borderRadius: 2,
            }}
          >
            <FormInput name='username' label='Username' />
            <FormInput name='email' label='Email Address' type='email' />
            <FormInput name='password' label='Password' type='password' />
            <FormInput
              name='confirmPassword'
              label='Confirm Password'
              type='password'
            />
            <Typography sx={{ fontSize: '0.9rem', mb: '1rem' }}>
              Already have an account?{' '}
              <LinkItem to='/login'>Login Here</LinkItem>
            </Typography>

            <LoadingButton
              variant='contained'
              sx={{ mt: 1 }}
              fullWidth
              disableElevation
              type='submit'
              loading={isLoading}
            >
              Sign Up
            </LoadingButton>
          </Box>
        </FormProvider>
      </Box>
    </Container>
  );
};

export default RegisterPage;
