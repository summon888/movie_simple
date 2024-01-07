import {
    BottomNavigation,
    BottomNavigationAction,
  } from '@mui/material';
  import { useNavigate } from 'react-router-dom';
  import { useAppSelector } from '../redux/store';
  import { useLogoutUserMutation } from '../redux/api/authApi';
  import { useEffect, useState } from 'react';
  import { toast } from 'react-toastify';
  import { LoadingButton as _LoadingButton } from '@mui/lab';
  import CottageIcon from '@mui/icons-material/Cottage';
  import SearchIcon from '@mui/icons-material/Search';
  import EditNoteIcon from '@mui/icons-material/EditNote';
  import NotificationsActiveIcon from '@mui/icons-material/NotificationsActive';
  import AccountCircleIcon from '@mui/icons-material/AccountCircle';
  
  const BottomNavbar = () => {
    const navigate = useNavigate();
    // const user = useAppSelector((state) => state.userState.user);
    const [value, setValue] = useState(0)
  
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
  
    const onLogoutHandler = async () => {
      logoutUser();
    };
  
    return (
        <BottomNavigation
        showLabels
        style={{ position: 'fixed', bottom: 0, width: '100%' }}
        value={value}
        onChange={(event, newValue) => {
          setValue(newValue);
        }}
      >
        <BottomNavigationAction label="Home" icon={<CottageIcon />} />
        <BottomNavigationAction label="Search" icon={<SearchIcon />} />
        <BottomNavigationAction label="Note" icon={<EditNoteIcon />} />
        <BottomNavigationAction label="Notification" icon={<NotificationsActiveIcon />} />
        <BottomNavigationAction label="Profile" icon={<AccountCircleIcon />} />
      </BottomNavigation>
    );
  };
  
  export default BottomNavbar;
  
  