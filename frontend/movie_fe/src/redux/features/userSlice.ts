import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IAuthen, IUser } from '../api/types';

interface IUserState {
  user: IUser | null;
  authToken: string | null;
}

const initialState: IUserState = {
  user: null,
  authToken: null
};

export const userSlice = createSlice({
  initialState,
  name: 'userSlice',
  reducers: {
    logout: () => initialState,
    setUser: (state, action: PayloadAction<IUser>) => {
      state.user = action.payload;
    },
    setAuthenToken: (state, action: PayloadAction<IAuthen>) => {
      state.authToken = action.payload.data.accessToken;
    },
  },
});

export default userSlice.reducer;

export const { logout, setUser, setAuthenToken } = userSlice.actions;

