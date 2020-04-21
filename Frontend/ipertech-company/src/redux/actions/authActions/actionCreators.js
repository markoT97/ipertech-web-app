import axios from "axios";
import { BACKEND_URL } from "../backendServerSettings";

import { SET_CURRENT_USER, UNSET_CURRENT_USER } from "./actionTypes";

import {
  setAuthorizationToken,
  decodeToken,
} from "../../../utils/authorization-helper";
import { addNotification } from "../notificationsActions/actionCreators";
import { notificationTypes } from "../../../shared/constants";

export function loginUser(email, password) {
  console.log("LOGIN USER");
  return (dispatch) => {
    axios
      .post(BACKEND_URL + "api/users/login", {
        email: email,
        password: password,
      })
      .then((response) => {
        const token = response.data;
        const decodedToken = decodeToken(token);

        setAuthorizationToken(token);

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
      })
      .catch((err) => {
        console.log(JSON.stringify(err.response.status));
        dispatch(
          addNotification({
            type: notificationTypes.ERROR,
            message: "Netačan imejl i lozinka",
            duration: 5000,
          })
        );
      });
  };
}

export function logoutUser() {
  setAuthorizationToken(null);
  return { type: UNSET_CURRENT_USER, user: {} };
}
