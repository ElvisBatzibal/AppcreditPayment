import { combineReducers } from "redux";
import userAccountReducers from "./userAccountReducers";
export default combineReducers({
  //objeto reducers
  //NombreStateStore:OrigenStateReducers
  userAcount: userAccountReducers,
});
