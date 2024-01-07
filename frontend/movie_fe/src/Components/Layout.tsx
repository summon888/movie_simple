import { Outlet } from 'react-router-dom';
import Header from './Header';
import BottomNavbar from './BottomNavbar';

const Layout = () => {
  return (
    <>
      <Header />
      <Outlet />
      <BottomNavbar />
    </>
  );
};

export default Layout;