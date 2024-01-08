import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { IAuthen, ILike, IMovie } from './types';
import { authHeader } from './authHeader';
import { LoginInput } from '../../pages/login.page';

export const userApi = createApi({
  reducerPath: 'userApi',
  baseQuery: fetchBaseQuery(authHeader()),
  tagTypes: ['User'],
  endpoints: (builder) => ({
    getCurrent: builder.mutation<
      any,
      null
    >({
      query() {
        return {
          url: `/Account/current`,
          credentials: 'include',
        };
      },
      async onQueryStarted(args, { dispatch, queryFulfilled }) {
        try {
          await queryFulfilled;
          //await dispatch(userApi.endpoints.getMe.initiate(null));
        } catch (error) { }
      },
    }),
  }),
});

export const { useGetCurrentMutation } = userApi;