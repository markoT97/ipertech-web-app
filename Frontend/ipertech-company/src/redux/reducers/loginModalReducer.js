import { SET_LOGIN_MODAL_VISIBILITY } from "../actions/loginModalActions/actionTypes";

function loginModalVisibilityReducer(visibility = false, action) {
  switch (action.type) {
    case SET_LOGIN_MODAL_VISIBILITY:
      return action.visibility;
    default:
      return visibility;
  }
}

export default loginModalVisibilityReducer;
