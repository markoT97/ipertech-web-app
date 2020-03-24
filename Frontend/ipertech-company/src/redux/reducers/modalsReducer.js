import {
  SET_LOGIN_MODAL_VISIBILITY,
  SET_REGISTER_MODAL_VISIBILITY,
  SET_BILL_MODAL_VISIBILITY
} from "../actions/modalsActions/actionTypes";

const initialModalsVisibility = {
  loginModalVisibility: false,
  registerModalVisibility: false,
  billModalVisibility: false
};

function modalsVisibilityReducer(
  modalsVisibility = initialModalsVisibility,
  action
) {
  switch (action.type) {
    case SET_LOGIN_MODAL_VISIBILITY:
      return { ...modalsVisibility, loginModalVisibility: action.visibility };
    case SET_REGISTER_MODAL_VISIBILITY:
      return {
        ...modalsVisibility,
        registerModalVisibility: action.visibility
      };
    case SET_BILL_MODAL_VISIBILITY:
      return {
        ...modalsVisibility,
        billModalVisibility: action.visibility
      };
    default:
      return modalsVisibility;
  }
}

export default modalsVisibilityReducer;
