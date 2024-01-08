import { RootState } from "../store";

const BASE_URL = process.env.REACT_APP_SERVER_ENDPOINT || 'http://localhost:5003/api/v1/'

export const authHeader = () : any => {
  return {
    baseUrl: BASE_URL,
    credentials: "omit", //it will bypass CORS
    prepareHeaders: (headers: Headers, { getState }: any) => {
      const token = (getState() as RootState).userState.authToken;
      if (token) {
        headers.set("Authorization", `Bearer ${token}`);
        headers.set("X-API-VERSION", "1.0");
      }
      return headers;
    },
  };
};