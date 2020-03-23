import {
  SET_LOGIN_MODAL_VISIBILITY,
  SET_REGISTER_MODAL_VISIBILITY
} from "./actionTypes";

export function setLoginModalVisibility(visibility) {
  return {
    type: SET_LOGIN_MODAL_VISIBILITY,
    visibility
  };
}

export function setRegisterModalVisibility(visibility) {
  return {
    type: SET_REGISTER_MODAL_VISIBILITY,
    visibility
  };
}
