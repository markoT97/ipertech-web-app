import axios from "axios";
import { BACKEND_URL } from "../redux/actions/backendServerSettings";
import {
  decodeToken,
  setAuthorizationToken,
} from "../utils/authorization-helper";

export async function getUserById(id) {
  try {
    const response = await axios.get(BACKEND_URL + "api/users/" + id);
    if (response.status >= 400) {
      throw new Error("Bad response from server");
    }
    return response.data;
  } catch (error) {}
}

export async function authenticateUser(email, password) {
  return axios
    .post(BACKEND_URL + "api/users/login", {
      email: email,
      password: password,
    })
    .then((response) => {
      const token = response.data;
      const decodedToken = decodeToken(token);

      setAuthorizationToken(token);

      return { success: { decodedToken } };
    })
    .catch((err) => {
      return { error: { message: "Netačan imejl i lozinka" } };
    });
}

export async function postUser(user) {
  try {
    const response = await axios.post(
      BACKEND_URL + "api/users",
      { ...user, role: "User" },
      {
        headers: { "Content-Type": "application/json" },
      }
    );
    if (response.status >= 400) {
      throw new Error("Bad response from server");
    }
    return {
      success: {
        insertedUser: response.data,
        message: "Uspešno ste se registrovali",
      },
    };
  } catch (err) {
    if (!err.response) {
      return { error: "Imejl se već koristi" };
    }
    const { status } = err.response;
    if (status === 404) {
      return {
        error: "Broj ugovora je nevažeći",
      };
    } else if (status === 409)
      return { error: "Već postoji Korisnik sa tim brojem ugovora" };
  }
}

export async function patchUserImage(userImage) {
  let data = new FormData();
  data.append("userId", userImage.userId);
  data.append("image", userImage.image);

  return axios
    .patch(BACKEND_URL + "api/users", data, {
      headers: { "Content-Type": "multipart/form-data" },
    })
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }
      return {
        success: {
          imageLocation: response.data + "?" + Date.now(),
          message: "Vaša slika je uspešno ažurirana",
        },
      };
    });
}

export async function patchUserPassword(newPassword) {
  return axios
    .patch(BACKEND_URL + "api/users/password", newPassword, {
      headers: { "Content-Type": "application/json" },
    })
    .then((response) => {
      if (response.status >= 400) {
        throw new Error("Bad response from server");
      }
      return {
        success: {
          data: response.data,
          message: "Vaša lozinka je uspešno ažurirana",
        },
      };
    });
}
