import axios from "axios";
import { tokenName, tokenType } from "../shared/constants";
import jwtDecode from "jwt-decode";

export function setAuthorizationToken(token) {
  if (!token) {
    delete axios.defaults.headers.common["Authorization"];
    localStorage.removeItem(tokenName);
  } else {
    axios.defaults.headers.common = { Authorization: tokenType + token };
    localStorage.setItem(tokenName, token);
  }
}

export function getAuthorizationToken() {
  return localStorage.getItem(tokenName);
}

export function isExpired(token) {
  if (!token) {
    return true;
  }

  if (jwtDecode(token).exp * 1000 < Date.now()) {
    return true;
  }

  return false;
}

export function decodeToken(token) {
  const user = jwtDecode(token);
  return user ? user : {};
}
