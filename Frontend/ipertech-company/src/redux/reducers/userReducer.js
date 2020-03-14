import { FETCH_USER_BY_ID } from "../actions/userActions/actionTypes";

const initialUser = {
  userId: null,
  userContract: {
    userContractId: "",
    packetCombination: {
      packetCombinationId: "",
      name: "",
      internetPacket: {
        internetPacketId: "",
        internetRouter: {
          internetRouterId: "",
          name: null,
          imageLocation: null
        },
        name: "",
        speed: "",
        price: 0
      },
      tvPacket: null,
      phonePacket: null
    },
    contractDurationMonths: 0
  },
  role: "",
  firstName: "",
  lastName: "",
  gender: "",
  email: "",
  phoneNumber: "",
  password: "",
  imageLocation: null,
  bills: []
};

function userReducer(user = initialUser, action) {
  switch (action.type) {
    case FETCH_USER_BY_ID:
      return action.user;
    default:
      return user;
  }
}

export default userReducer;
