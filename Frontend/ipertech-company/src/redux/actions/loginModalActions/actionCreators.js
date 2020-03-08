import { SET_LOGIN_MODAL_VISIBILITY } from "./actionTypes";

function setVisibility(visibility) {
  return {
    type: SET_LOGIN_MODAL_VISIBILITY,
    visibility
  };
}

export default setVisibility;
