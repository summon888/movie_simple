import {
  AppBar,
  Avatar,
  Box,
  Container,
  IconButton,
  Toolbar,
  Tooltip,
  Typography,
} from '@mui/material';
import { styled } from '@mui/material/styles';
import { useNavigate } from 'react-router-dom';
import { useAppSelector } from '../redux/store';
import { useLogoutUserMutation } from '../redux/api/authApi';
import { useEffect } from 'react';
import { toast } from 'react-toastify';
import { LoadingButton as _LoadingButton } from '@mui/lab';
import { useDispatch } from 'react-redux';
import { logout } from '../redux/features/userSlice';
import { useGetCurrentMutation } from '../redux/api/userApi';

const LoadingButton = styled(_LoadingButton)`
    padding: 0.4rem;
    background-color: #f9d13e;
    color: #2363eb;
    font-weight: 500;
  
    &:hover {
      background-color: #ebc22c;
      transform: translateY(-2px);
    }
  `;

const Header = () => {
  const navigate = useNavigate();
  const user = useAppSelector((state) => state.userState.user);
  const dispatch = useDispatch();
  const [getCurrent, { data, isLoading: isLoading2, isSuccess: isSuccess2 }] = useGetCurrentMutation();

  const [logoutUser, { isLoading, isSuccess, error, isError }] =
    useLogoutUserMutation();

  useEffect(() => {
    if (isSuccess) {
      // window.location.href = '/login';
      navigate('/login');
    }

    if (isError) {
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
    getCurrent(null).then((result: any)=>{
      if (result.error){
        console.log(result.error);
        if (result.error.status === 401){
          dispatch(logout());
        }
      }
    })
  }, [])

  const onLogoutHandler = async () => {
    logoutUser();
    dispatch(logout());
  };

  return (
    <AppBar position='static'>
      <Container maxWidth='lg'>
        <Toolbar>
          <Typography
            variant='h6'
            onClick={() => navigate('/')}
            sx={{ cursor: 'pointer' }}
          >
            MovieHub Hi, {user}
          </Typography>
          <Box display='flex' sx={{ ml: 'auto' }}>
            {!user && (
              <>
                <LoadingButton
                  sx={{ mr: 2 }}
                  onClick={() => navigate('/register')}
                >
                  SignUp
                </LoadingButton>
                <LoadingButton onClick={() => navigate('/login')}>
                  Login
                </LoadingButton>
              </>
            )}
            {user && (
              <LoadingButton
                sx={{ backgroundColor: '#eee' }}
                onClick={onLogoutHandler}
                loading={isLoading}
              >
                Logout
              </LoadingButton>
            )}
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
  );
};

export default Header;

