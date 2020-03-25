import axios from "axios";
import { BACKEND_URL } from "../backendServerSettings";

import { SET_CURRENT_USER, UNSET_CURRENT_USER } from "./actionTypes";

import setAuthorizationToken from "./../../../utils/setAuthorizationToken";
import jwtDecode from "jwt-decode";
import { fromUnixTime } from "date-fns";

let axiosInterceptor = null;

export function loginUser(email, password) {
  console.log("LOGIN USER");
  return dispatch => {
    axios
      .post(BACKEND_URL + "api/users/login", {
        email: email,
        password: password
      })
      .then(response => {
        if (response.status >= 400) {
          throw new Error("Bad response from server");
        }

        const token = response.data;
        const decodedToken = jwtDecode(token);

        localStorage.setItem("jwt", token);
        setAuthorizationToken(token);

        axiosInterceptor = axios.interceptors.request.use(
          function(config) {
            if (Date.now() > fromUnixTime(decodedToken.exp)) {
              console.log("Authentication token has expired");
              dispatch(logoutUser());
            }
            return config;
          },
          function(error) {
            return Promise.reject(error);
          }
        );

        dispatch({
          type: SET_CURRENT_USER,
          user: decodedToken
        });
      })
      .catch(err => {
        console.error(err);
      });
  };
}

export function logoutUser() {
  localStorage.removeItem("jwt");
  setAuthorizationToken(null);
  axios.interceptors.request.eject(axiosInterceptor);
  return { type: UNSET_CURRENT_USER, user: {} };
}