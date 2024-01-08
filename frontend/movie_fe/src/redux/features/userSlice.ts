import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IAuthen, IUser } from '../api/types';
import { RootState } from '../store';

interface IUserState {
  user: string | null;
  authToken: string | null;
  refreshToken: string | null;
}

const initialState: IUserState = {
  user: null,
  authToken: null,
  refreshToken: null
};

export const userSlice = createSlice({
  initialState,
  name: 'userSlice',
  reducers: {
    logout: (state) => {
      state.user = null;
      state.authToken = null;
      state.refreshToken = null;
      localStorage.removeItem("user");
      localStorage.removeItem("tokenData");
    },
    setUser: (state, action: PayloadAction<string>) => {
      state.user = action.payload;
      localStorage.setItem("user", action.payload);
    },
    setAuthenToken: (state, action: PayloadAction<IAuthen>) => {
      state.authToken = action.payload.data.accessToken;
      state.refreshToken = action.payload.data.refreshToken;
      localStorage.setItem("tokenData", JSON.stringify(action.payload));
    },
  },
});

export default userSlice.reducer;

export const { logout, setAuthenToken, setUser } = userSlice.actions;

//deriving data
export const selectUser = (state: RootState) => state!.userState;
