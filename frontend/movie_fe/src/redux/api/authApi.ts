import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { LoginInput } from '../../pages/login.page';
import { RegisterInput } from '../../pages/register.page';
import { IAuthen, IGenericResponse } from './types';
import { userApi } from './userApi';
import { authHeader } from './authHeader';

export const authApi = createApi({
  reducerPath: 'authApi',
  baseQuery: fetchBaseQuery(authHeader()),
  endpoints: (builder) => ({
    registerUser: builder.mutation<IGenericResponse, RegisterInput>({
      query(data) {
        return {
          url: '/Account/register',
          method: 'POST',
          body: data,
        };
      },
    }),
    loginUser: builder.mutation<
      IAuthen,
      LoginInput
    >({
      query(data) {
        return {
          url: '/Account/login',
          method: 'POST',
          body: data,
          credentials: 'include',
        };
      },
      async onQueryStarted(args, { dispatch, queryFulfilled }) {
        try {
          await queryFulfilled;
          //await dispatch(userApi.endpoints.getMe.initiate(null));
        } catch (error) {}
      },
    }),
    logoutUser: builder.mutation<void, void>({
      query() {
        return {
          url: 'logout',
          credentials: 'include',
        };
      },
    }),
  }),
});

export const {
  useLoginUserMutation,
  useRegisterUserMutation,
  useLogoutUserMutation,
} = authApi;
