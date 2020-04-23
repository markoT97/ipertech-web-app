import {
  FETCH_USER_CONTRACTS,
  INSERT_USER_CONTRACT,
  DELETE_USER_CONTRACT,
} from "../actions/userContractsActions/actionTypes";

function userContractsReducer(userContracts = [], action) {
  switch (action.type) {
    case FETCH_USER_CONTRACTS:
      return action.userContracts;
    case INSERT_USER_CONTRACT:
      return [...userContracts, action.newUserContract];
    case DELETE_USER_CONTRACT:
      return [
        ...userContracts.filter(
          (userContract) =>
            userContract.userContractId !== action.userContractId
        ),
      ];
    default:
      return userContracts;
  }
}

export default userContractsReducer;
