import { CssBaseline } from '@mui/material';
import { Route, Routes } from 'react-router-dom';
import ProfilePage from './pages/profile.page';
import HomePage from './pages/home.page';
import LoginPage from './pages/login.page';
import RegisterPage from './pages/register.page';
import UnauthorizePage from './pages/unauthorize.page';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import AdminPage from './pages/admin.page';
import Layout from './components/Layout';
import RequireUser from './components/RequireUser';

function App() {
  return (
    <>
      <CssBaseline />
      <ToastContainer />
      <Routes>
        <Route path='/' element={<Layout />}>
        <Route index element={<HomePage />} />
          {/* Private Route */}
          <Route element={<RequireUser allowedRoles={['user', 'admin']} />}>
            <Route index element={<HomePage />} />
          </Route>
          <Route element={<RequireUser allowedRoles={['user', 'admin']} />}>
            <Route path='profile' element={<ProfilePage />} />
          </Route>
          <Route element={<RequireUser allowedRoles={['admin']} />}>
            <Route path='admin' element={<AdminPage />} />
          </Route>
          <Route path='unauthorized' element={<UnauthorizePage />} />
        </Route>
        <Route path='login' element={<LoginPage />} />
        <Route path='register' element={<RegisterPage />} />
      </Routes>
    </>
  );
}

export default App;
