import {
  SET_LOGIN_MODAL_VISIBILITY,
  SET_REGISTER_MODAL_VISIBILITY,
  SET_BILL_MODAL_VISIBILITY,
  SET_INSERT_MESSAGE_FORM_VISIBILITY,
  SET_INSERT_PACKET_COMBINATION_FORM_VISIBILITY,
} from "./actionTypes";

export function setLoginModalVisibility(visibility) {
  return {
    type: SET_LOGIN_MODAL_VISIBILITY,
    visibility,
  };
}

export function setRegisterModalVisibility(visibility) {
  return {
    type: SET_REGISTER_MODAL_VISIBILITY,
    visibility,
  };
}

export function setBillModalVisibility(visibility) {
  return {
    type: SET_BILL_MODAL_VISIBILITY,
    visibility,
  };
}

export function setInsertMessageModalVisibility(visibility) {
  return {
    type: SET_INSERT_MESSAGE_FORM_VISIBILITY,
    visibility,
  };
}

export function setInsertPacketCombinationModalVisibility(visibility) {
  return {
    type: SET_INSERT_PACKET_COMBINATION_FORM_VISIBILITY,
    visibility,
  };
}
