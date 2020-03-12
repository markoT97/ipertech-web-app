import axios from "axios";

function setAuthorizationToken(token) {
  if (!token) {
    delete axios.defaults.headers.common["Authorization"];
  } else {
    axios.defaults.headers.common["Authorization"] = token;
  }
}

export default setAuthorizationToken;
