import React from "react";
import "./App.scss";
import Navigation from "./Navigation";
import Routes from "../Routes";
import Footer from "./Footer";
import { BrowserRouter as Router } from "react-router-dom";
import { Container, Row, Col } from "react-bootstrap";
import Notifications from "./Notifications";
import FormModal from "../components/FormModal";
import {
  setLoginModalVisibility,
  setRegisterModalVisibility,
} from "../redux/actions/modalsActions/actionCreators";
import {
  loginValidationSchema,
  registerValidationSchema,
} from "../shared/validation-schemas";
import { useDispatch, useSelector } from "react-redux";
import { loginUser } from "../redux/actions/authActions/actionCreators";
import * as Icon from "react-bootstrap-icons";
import { insertUser } from "../redux/actions/userActions/actionCreators";

const App = () => {
  const dispatch = useDispatch();

  // Login
  const loginModalVisibility = useSelector(
    (state) => state.modalsVisibility.loginModalVisibility
  );

  const userLoginFormData = [
    {
      controlId: "formLoginEmail",
      type: "email",
      label: "Kombinacija paketa",
      prepend: <i className="text-success">@</i>,
      name: "email",
      placeholder: "Unesite imejl adresu",
      value: {
        email: "email",
      },
    },
    {
      controlId: "formLoginPassword",
      type: "password",
      label: "Lozinka",
      prepend: <Icon.LockFill className="text-success" />,
      name: "password",
      placeholder: "Lozinka",
      value: {
        password: "password",
      },
    },
  ];

  // Register
  const registerModalVisibility = useSelector(
    (state) => state.modalsVisibility.registerModalVisibility
  );

  const userRegisterFormData = [
    {
      controlId: "formRegisterContractID",
      type: "text",
      label: "Broj ugovora",
      prepend: <i className="text-success">#</i>,
      name: "userContractId",
      placeholder: "Unesite broj vašeg ugovora",
      value: {
        userContractId: "userContractId",
      },
    },
    {
      controlId: "formRegisterFirstName",
      type: "text",
      label: "Ime",
      prepend: <i className="text-success">I</i>,
      name: "firstName",
      placeholder: "Unesite vaše ime",
      value: {
        firstName: "firstName",
      },
    },
    {
      controlId: "formRegisterLastName",
      type: "text",
      label: "Prezime",
      prepend: <i className="text-success">P</i>,
      name: "lastName",
      placeholder: "Unesite vaše prezime",
      value: {
        lastName: "lastName",
      },
    },
    {
      controlId: "formRegisterEmail",
      type: "text",
      label: "Imejl",
      prepend: <i className="text-success">@</i>,
      name: "email",
      placeholder: "Unesite vašu imejl adresu",
      value: {
        email: "email",
      },
    },
    {
      controlId: "formRegisterPhoneNumber",
      type: "text",
      label: "Broj telefona",
      prepend: <Icon.Phone />,
      name: "phoneNumber",
      placeholder: "Unesite vaš broj telefona",
      value: {
        phoneNumber: "phoneNumber",
      },
    },
    {
      controlId: "formRegisterPassword",
      type: "password",
      label: "Lozinka",
      prepend: <Icon.Lock className="text-success" />,
      name: "password",
      placeholder: "Lozinka",
      value: {
        password: "password",
      },
    },
    {
      controlId: "formRegisterPasswordConfirm",
      type: "password",
      label: "Lozinka",
      prepend: <Icon.LockFill className="text-success" />,
      name: "passwordConfirm",
      placeholder: "Potvrdite lozniku",
      value: {
        passwordConfirm: "passwordConfirm",
      },
    },
    {
      controlId: "formRegisterGender",
      type: "radio",
      data: ["Muški", "Ženski"],
      label: "Pol",
      name: "gender",
      placeholder: "Pol",
      value: {
        gender: "gender",
      },
    },
  ];

  return (
    <div className="App">
      <Router>
        <Navigation />
        <Notifications />
        {/* Login form*/}
        <FormModal
          headerClassNames={"bg-success text-white"}
          modalTitle={"Prijava"}
          formData={userLoginFormData}
          validationSchema={loginValidationSchema}
          show={loginModalVisibility}
          onHide={() => dispatch(setLoginModalVisibility(false))}
          onSubmit={(value) => dispatch(loginUser(value))}
        />
        {/* Register form */}
        <FormModal
          headerClassNames={"bg-success text-white"}
          modalTitle={"Registracija"}
          formData={userRegisterFormData}
          validationSchema={registerValidationSchema}
          show={registerModalVisibility}
          onHide={() => dispatch(setRegisterModalVisibility(false))}
          onSubmit={(value) => dispatch(insertUser(value))}
        />
        <Container>
          <Row className="justify-content-md-center">
            <Col xs lg={10}>
              <Routes />
            </Col>
          </Row>
        </Container>
        <Footer />
      </Router>
    </div>
  );
};

export default App;
