import { SET_CURRENT_USER, UNSET_CURRENT_USER } from "./actionTypes";

import { setAuthorizationToken } from "../../../utils/authorization-helper";
import { addNotification } from "../notificationsActions/actionCreators";
import { notificationTypes } from "../../../shared/constants";
import { authenticateUser } from "../../../services/userService";

export function loginUser(email, password) {
  console.log("LOGIN USER");
  return (dispatch) => {
    authenticateUser(email, password).then((data) => {
      if (data.error) {
        return dispatch(
          addNotification({
            type: notificationTypes.ERROR,
            message: data.error.message,
            duration: 5000,
          })
        );
      }
      const { decodedToken } = data.success;

      dispatch({
        type: SET_CURRENT_USER,
        user: decodedToken,
      });

      const tokenExpirationMillisecond = decodedToken.exp * 1000 - Date.now();

      setTimeout(() => {
        dispatch(logoutUser());
        return dispatch(
          addNotification({
            type: notificationTypes.ERROR,
            message: "Token je istekao, prijavite se ponovo",
            duration: 5000,
          })
        );
      }, tokenExpirationMillisecond);
    });
  };
}

export function logoutUser() {
  setAuthorizationToken(null);
  return { type: UNSET_CURRENT_USER, user: {} };
}
