import {
  SET_CURRENT_USER,
  UNSET_CURRENT_USER
} from "../actions/authActions/actionTypes";

import jwtDecode from "jwt-decode";
import isEmpty from "lodash/isEmpty";

const initialAuthState = {
  isAuthenticated: localStorage.getItem("jwt") ? true : false,
  user: localStorage.getItem("jwt")
    ? jwtDecode(localStorage.getItem("jwt"))
    : {}
};

function authReducer(authState = initialAuthState, action) {
  switch (action.type) {
    case SET_CURRENT_USER:
      const { user } = action;
      return { user, isAuthenticated: !isEmpty(user) };
    case UNSET_CURRENT_USER:
      return { user: {}, isAuthenticated: false };
    default:
      return authState;
  }
}

export default authReducer;
