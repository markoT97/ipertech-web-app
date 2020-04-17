import axios from "axios";
import { BACKEND_URL } from "../backendServerSettings";

import { SET_CURRENT_USER, UNSET_CURRENT_USER } from "./actionTypes";

import setAuthorizationToken from "./../../../utils/setAuthorizationToken";
import jwtDecode from "jwt-decode";
import { addNotification } from "../notificationsActions/actionCreators";
import { notificationTypes } from "../../../shared/constants";

export function loginUser(email, password) {
  console.log("LOGIN USER");
  return dispatch => {
    axios
      .post(BACKEND_URL + "api/users/login", {
        email: email,
        password: password
      })
      .then(response => {
        const token = response.data;
        const decodedToken = jwtDecode(token);

        localStorage.setItem("jwt", token);
        setAuthorizationToken(token);

        dispatch({
          type: SET_CURRENT_USER,
          user: decodedToken
        });
      })
      .catch(err => {
        console.log(JSON.stringify(err.response.status));
        dispatch(
          addNotification({
            type: notificationTypes.ERROR,
            message: "Netačan imejl i lozinka",
            duration: 5000
          })
        );
      });
  };
}

export function logoutUser() {
  localStorage.removeItem("jwt");
  setAuthorizationToken(null);
  return { type: UNSET_CURRENT_USER, user: {} };
}
