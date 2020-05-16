import {
  LOGGIN_START,
  LOGGIN_START_SUCCESS,
  LOGGIN_START_ERROR,
  LOGGIN_OUT,
  LOGGIN_IN,
} from "../types";
import clienteAxios from "../config/axios";
import { history } from "../helpers";
export function userAccountAction(UserName, Password) {
  const data = {
    UserName,
    Password,
  };
  return (dispatch) => {
    dispatch(enviarDataInicio());
    clienteAxios
      .post("/api/AccountUser/Login", data)
      .then((result) => {
        //aplicas logica al result
        //result.data.Resultado..

        dispatch(enviarDataInicioSucess(result.data));

        if (result.data.EntityID > 0) {
          history.push("/");
          dispatch(logginSucess(result.data.EntityID));
          //logica para la navegacion..
          //ruta localhost:300/ al index
        } else {
          dispatch(logout());
          //noselogeo
          //ruta localhost:300/login
          history.push("/login");
          dispatch(enviarDataInicioError(error));
        }
      })
      .catch((error) => {
        dispatch(enviarDataInicioError(error));
      });
  };
}

//({}) funcion anonima y retorna un objeto explicito
export const enviarDataInicio = () => ({
  type: LOGGIN_START,
});

export const enviarDataInicioSucess = (data) => ({
  type: LOGGIN_START_SUCCESS,
  payload: data,
});
export const enviarDataInicioError = (error) => ({
  type: LOGGIN_START_ERROR,
  payload: error,
});

export const logginSucess = (token) => ({
  type: LOGGIN_IN,
  token,
});

export function logoutActions() {
  return (dispatch) => {
    history.push("/login");
    dispatch(logout());
  };
}
export const logout = () => ({
  type: LOGGIN_OUT,
});
