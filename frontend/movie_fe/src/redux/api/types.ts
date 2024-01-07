export interface IUser {
    name: string;
    email: string;
    role: string;
    _id: string;
    createdAt: Date;
    updatedAt: Date;
    __v: number;
  }
  
  export interface IGenericResponse {
    status: string;
    message: string;
  }

  export interface IAuthen {
    success: boolean;
    data: {
      accessToken: string;
      refreshToken: string;
    };
  }
  
  