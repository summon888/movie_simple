import { Navigate, Outlet, useLocation } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { selectUser } from '../redux/features/userSlice';
import { IUser } from '../redux/api/types';
import { useAppSelector } from '../redux/store';
import { useGetCurrentMutation } from '../redux/api/userApi';
import { useEffect } from 'react';


type ProtectedRouteProps = {
  children: JSX.Element;
};

//This component will check if token exists or not. If not redirect user to home page
const ProtectedRoute = ({ children }: ProtectedRouteProps): JSX.Element => {
  const { user, authToken } = useAppSelector(selectUser);
  const [getCurrent] = useGetCurrentMutation();

  // useEffect(() => {
  //   //check xem con han khong
  //   getCurrent(null).then((result: any) => {
  //     console.log(result);
  //     if (!result.data) {

  //     }
  //   })
  // }, [isSuccess])

  const location = useLocation();
  if (!authToken) {
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  

  return children;
};

export default ProtectedRoute;

// const RequireUser = ({ allowedRoles }: { allowedRoles: string[] }) => {
//   const [cookies] = useCookies(['logged_in']);
//   const location = useLocation();

//   const { isLoading, isFetching } = userApi.endpoints.getMe.useQuery(null, {
//     skip: false,
//     refetchOnMountOrArgChange: true,
//   });

//   const loading = isLoading || isFetching;

//   const user = userApi.endpoints.getMe.useQueryState(null, {
//     selectFromResult: ( data ) => data,
//   });

//   if (loading) {
//     return <FullScreenLoader />;
//   }

//   return (cookies.logged_in || user) &&
//     allowedRoles.includes(user?.data?.role as string) ? (
//     <Outlet />
//   ) : cookies.logged_in && user ? (
//     <Navigate to='/unauthorized' state={{ from: location }} replace />
//   ) : (
//     <Navigate to='/login' state={{ from: location }} replace />
//   );
// };


