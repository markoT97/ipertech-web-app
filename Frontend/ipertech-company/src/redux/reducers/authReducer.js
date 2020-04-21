import {
  SET_CURRENT_USER,
  UNSET_CURRENT_USER,
} from "../actions/authActions/actionTypes";

import isEmpty from "lodash/isEmpty";
import {
  setAuthorizationToken,
  getAuthorizationToken,
  decodeToken,
} from "../../utils/authorization-helper";

const initialAuthState = {
  isAuthenticated: getAuthorizationToken() ? true : false,
  user: getAuthorizationToken() ? decodeToken(getAuthorizationToken()) : {},
};

function authReducer(authState = initialAuthState, action) {
  switch (action.type) {
    case SET_CURRENT_USER:
      const { user } = action;
      return { user, isAuthenticated: !isEmpty(user) };
    case UNSET_CURRENT_USER:
      return { user: {}, isAuthenticated: false };
    default:
      if (authState.isAuthenticated) {
        setAuthorizationToken(getAuthorizationToken());
      }
      return authState;
  }
}

export default authReducer;
