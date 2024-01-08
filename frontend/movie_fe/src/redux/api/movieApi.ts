import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { IAuthen, ILike, IMovie } from './types';
import { authHeader } from './authHeader';

export const movieApi = createApi({
  reducerPath: 'movieApi',
  baseQuery: fetchBaseQuery(authHeader()),
  tagTypes: ['Movie'],
  endpoints: (builder) => ({
    getMovie: builder.mutation<
      any,
      { page: number }
    >({
      query(data) {
        return {
          url: `/Movie/movie-management/pagination?skip=${data.page}&take=2`,
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
    like: builder.mutation<
      any,
      ILike
    >({
      query(data) {
        return {
          url: `/Movie/movie-management/like`,
          credentials: 'include',
          body: data,
          method: 'post'
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

export const { useGetMovieMutation, useLikeMutation } = movieApi;