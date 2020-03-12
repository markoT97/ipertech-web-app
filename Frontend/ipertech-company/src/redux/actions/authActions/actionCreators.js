import fetch from "cross-fetch";
import { BACKEND_URL } from "../backendServerSettings";

import { SET_CURRENT_USER, UNSET_CURRENT_USER } from "./actionTypes";

import setAuthorizationToken from "./../../../utils/setAuthorizationToken";
import jwtDecode from "jwt-decode";

export function loginUser(email, password) {
  return dispatch => {
    fetch(BACKEND_URL + "api/users/login", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },

      body: JSON.stringify({
        email: email,
        password: password
      })
    })
      .then(res => {
        if (res.status >= 400) {
          throw new Error("Bad response from server");
        }
        return res.json();
      })
      .then(token => {
        localStorage.setItem("jwt", token);
        setAuthorizationToken(token);

        dispatch({
          type: SET_CURRENT_USER,
          user: jwtDecode(token)
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
  return { type: UNSET_CURRENT_USER, user: {} };
}
