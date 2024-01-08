import { CssBaseline } from '@mui/material';
import { Route, Routes } from 'react-router-dom';
import HomePage from './pages/home.page';
import LoginPage from './pages/login.page';
import RegisterPage from './pages/register.page';
import UnauthorizePage from './pages/unauthorize.page';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Layout from './components/Layout';
import ProtectedRoute from './components/protectedRoute';
import { useAppDispatch } from './redux/store';
import { useEffect } from 'react';
import { setAuthenToken, setUser } from './redux/features/userSlice';

function App() {
  const dispatch = useAppDispatch();
  const user = localStorage.getItem("user") || "";

  useEffect(() => {
    if (user){
      dispatch(setUser(user));
      const token = JSON.parse(localStorage.getItem("tokenData") || "{}")
      dispatch(setAuthenToken(token));

      //direct home
      //window.location.href = "/";
    }
  }, [])

  return (
    <>
      <CssBaseline />
      <ToastContainer />
      <Routes>
        <Route path='/' element={<Layout />}>
          {/* Private Route */}
          <Route path="/" element={<ProtectedRoute><HomePage /></ProtectedRoute>} />
          <Route path='unauthorized' element={<UnauthorizePage />} />
        </Route>
        <Route path='login' element={<LoginPage />} />
        <Route path='register' element={<RegisterPage />} />
      </Routes>
    </>
  );
}

export default App;
