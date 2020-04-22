import {
  SET_LOGIN_MODAL_VISIBILITY,
  SET_REGISTER_MODAL_VISIBILITY,
  SET_BILL_MODAL_VISIBILITY,
  SET_INSERT_MESSAGE_FORM_VISIBILITY,
  SET_INSERT_PACKET_COMBINATION_FORM_VISIBILITY,
} from "../actions/modalsActions/actionTypes";

const initialModalsVisibility = {
  loginModalVisibility: false,
  registerModalVisibility: false,
  billModalVisibility: false,
  insertMessageModalVisibility: false,
  insertPacketCombinationModalVisibility: false,
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
        registerModalVisibility: action.visibility,
      };
    case SET_BILL_MODAL_VISIBILITY:
      return {
        ...modalsVisibility,
        billModalVisibility: action.visibility,
      };
    case SET_INSERT_MESSAGE_FORM_VISIBILITY:
      return {
        ...modalsVisibility,
        insertMessageModalVisibility: action.visibility,
      };
    case SET_INSERT_PACKET_COMBINATION_FORM_VISIBILITY:
      return {
        ...modalsVisibility,
        insertPacketCombinationModalVisibility: action.visibility,
      };
    default:
      return modalsVisibility;
  }
}

export default modalsVisibilityReducer;
