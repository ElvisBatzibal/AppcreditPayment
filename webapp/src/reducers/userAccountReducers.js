import {
  LOGGIN_START,
  LOGGIN_START_SUCCESS,
  LOGGIN_START_ERROR,
  LOGGIN_IN,
  LOGGIN_OUT,
} from "../types";

const initialState = {
  loggedIn: false,
  user: {},
  loading: false,
  error: null,
  data: {},
  token: "",
};

export default function (state = initialState, actions) {
  //funcion que leee los eventos que pasan desde los Actions.
  //actions son palabras recervadas..
  switch (actions.types) {
    case LOGGIN_START:
      return {
        ...state,
        loading: true,
        data: {},
      };
    case LOGGIN_START_SUCCESS:
      return {
        ...state,
        loading: false,
        data: actions.payload,
        error: false,
      };
    case LOGGIN_START_ERROR:
      return {
        ...state,
        loading: false,
        data: {},
        error: actions.payload,
      };

    case LOGGIN_IN:
      //logica y crear un localstorage
      return {
        ...state,
        loggedIn: true,
        token: actions.token,
      };
    case LOGGIN_OUT:
      return {
        ...state,
        loggedIn: false,
        user: {},
        loading: false,
        error: null,
        data: {},
        token: "",
      };

    default:
      return state;
  }
}
