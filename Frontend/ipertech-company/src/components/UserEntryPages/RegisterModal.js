import React, { Component } from "react";
import { Modal, Form, InputGroup, Button, Image, Col } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { setRegisterModalVisibility } from "../../redux/actions/modalsActions/actionCreators";
import { insertUser } from "../../redux/actions/userActions/actionCreators";
import { BACKEND_URL } from "../../redux/actions/backendServerSettings";
import { Formik } from "formik";
import { registerValidationSchema as schema } from "./../../shared/validation";

export class RegisterModal extends Component {
  state = {
    showBillExample: false
  };
  handleOnSubmitRegisterForm = form => {
    this.props.insertUser({
      userContract: {
        userContractId: form.userContractId
      },
      firstName: form.firstName,
      lastName: form.lastName,
      gender: form.gender,
      email: form.email,
      phoneNumber: form.phoneNumber,
      password: form.password,
      passwordConfirm: form.passwordConfirm
    });
    this.props.setRegisterModalVisibility(false);
  };
  render() {
    const genders = ["Muški", "Ženski"];
    return (
      <Modal
        aria-labelledby="contained-modal-title-vcenter"
        centered
        show={this.props.modalsVisibility.registerModalVisibility}
        onHide={() => this.props.setRegisterModalVisibility(false)}
      >
        <Modal.Header closeButton className="bg-success text-white">
          <Modal.Title id="contained-modal-title-vcenter">
            Registracija
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Formik
            validationSchema={schema}
            onSubmit={this.handleOnSubmitRegisterForm}
            initialValues={{
              userContractId: "082c90f1-f513-4b2d-acf1-14b277d6d6c8",
              firstName: "",
              lastName: "",
              gender: "",
              email: "",
              phoneNumber: "",
              password: "",
              passwordConfirm: ""
            }}
          >
            {({
              handleSubmit,
              handleChange,
              handleBlur,
              values,
              touched,
              isValid,
              errors,
              dirty
            }) => (
              <Form onSubmit={handleSubmit}>
                <Form.Group controlId="formRegisterContractID">
                  <Form.Label>Broj ugovora</Form.Label>
                  <InputGroup>
                    <InputGroup.Prepend>
                      <InputGroup.Text
                        id="btnGroupAddon"
                        className="text-success"
                      >
                        #
                      </InputGroup.Text>
                    </InputGroup.Prepend>
                    <Form.Control
                      onChange={handleChange}
                      onFocus={() => this.setState({ showBillExample: true })}
                      onBlur={() => this.setState({ showBillExample: false })}
                      type="text"
                      name="userContractId"
                      placeholder="Unesite broj vašeg ugovora"
                      value={values.userContractId}
                      isValid={touched.userContractId && !errors.userContractId}
                      isInvalid={!!errors.userContractId}
                    />
                    {errors.userContractId && (
                      <Form.Control.Feedback type="invalid">
                        {errors.userContractId}
                      </Form.Control.Feedback>
                    )}
                  </InputGroup>
                  <Form.Text className="text-muted">
                    Kliknite na polje za prikaz broja ugovora na računu
                  </Form.Text>
                </Form.Group>

                {this.state.showBillExample && (
                  <Image src={BACKEND_URL + "account/bill-example.svg"} fluid />
                )}

                <Form.Group controlId="formRegisterFirstName">
                  <Form.Label>Ime</Form.Label>
                  <InputGroup>
                    <InputGroup.Prepend>
                      <InputGroup.Text
                        id="btnGroupAddon"
                        className="text-success"
                      >
                        I
                      </InputGroup.Text>
                    </InputGroup.Prepend>
                    <Form.Control
                      onChange={handleChange}
                      type="text"
                      name="firstName"
                      placeholder="Unesite vaše ime"
                      value={values.firstName}
                      isValid={touched.firstName && !errors.firstName}
                      isInvalid={!!errors.firstName}
                    />
                    {errors.firstName && (
                      <Form.Control.Feedback type="invalid">
                        {errors.firstName}
                      </Form.Control.Feedback>
                    )}
                  </InputGroup>
                </Form.Group>

                <Form.Group controlId="formRegisterLastName">
                  <Form.Label>Prezime</Form.Label>
                  <InputGroup>
                    <InputGroup.Prepend>
                      <InputGroup.Text
                        id="btnGroupAddon"
                        className="text-success"
                      >
                        P
                      </InputGroup.Text>
                    </InputGroup.Prepend>
                    <Form.Control
                      onChange={handleChange}
                      type="text"
                      name="lastName"
                      placeholder="Unesite vaše prezime"
                      value={values.lastName}
                      isValid={touched.lastName && !errors.lastName}
                      isInvalid={!!errors.lastName}
                    />
                    {errors.lastName && (
                      <Form.Control.Feedback type="invalid">
                        {errors.lastName}
                      </Form.Control.Feedback>
                    )}
                  </InputGroup>
                </Form.Group>

                <Form.Group controlId="formRegisterEmail">
                  <Form.Label>Imejl</Form.Label>
                  <InputGroup>
                    <InputGroup.Prepend>
                      <InputGroup.Text
                        id="btnGroupAddon"
                        className="text-success"
                      >
                        @
                      </InputGroup.Text>
                    </InputGroup.Prepend>
                    <Form.Control
                      onChange={handleChange}
                      type="email"
                      name="email"
                      placeholder="Unesite vašu imejl adresu"
                      value={values.email}
                      isValid={touched.email && !errors.email}
                      isInvalid={!!errors.email}
                    />
                    {errors.email && (
                      <Form.Control.Feedback type="invalid">
                        {errors.email}
                      </Form.Control.Feedback>
                    )}
                  </InputGroup>
                </Form.Group>

                <Form.Group controlId="formRegisterPhoneNumber">
                  <Form.Label>Broj telefona</Form.Label>
                  <InputGroup>
                    <InputGroup.Prepend>
                      <InputGroup.Text
                        id="btnGroupAddon"
                        className="text-success"
                      >
                        <Icon.Phone />
                      </InputGroup.Text>
                    </InputGroup.Prepend>
                    <Form.Control
                      onChange={handleChange}
                      type="text"
                      name="phoneNumber"
                      placeholder="Unesite vaš broj telefona"
                      value={values.phoneNumber}
                      isValid={touched.phoneNumber && !errors.phoneNumber}
                      isInvalid={!!errors.phoneNumber}
                    />
                    {errors.phoneNumber && (
                      <Form.Control.Feedback type="invalid">
                        {errors.phoneNumber}
                      </Form.Control.Feedback>
                    )}
                  </InputGroup>
                </Form.Group>

                <Form.Group controlId="formRegisterPassword">
                  <Form.Label>Lozinka</Form.Label>
                  <InputGroup>
                    <InputGroup.Prepend>
                      <InputGroup.Text id="btnGroupAddon">
                        <Icon.Lock className="text-success" />
                      </InputGroup.Text>
                    </InputGroup.Prepend>
                    <Form.Control
                      onChange={handleChange}
                      type="password"
                      name="password"
                      placeholder="Lozinka"
                      value={values.password}
                      isValid={touched.password && !errors.password}
                      isInvalid={!!errors.password}
                    />
                    {errors.password && (
                      <Form.Control.Feedback type="invalid">
                        {errors.password}
                      </Form.Control.Feedback>
                    )}
                  </InputGroup>
                </Form.Group>

                <Form.Group controlId="formRegisterPasswordConfirm">
                  <Form.Label>Potvrda lozinke</Form.Label>
                  <InputGroup>
                    <InputGroup.Prepend>
                      <InputGroup.Text id="btnGroupAddon">
                        <Icon.LockFill className="text-success" />
                      </InputGroup.Text>
                    </InputGroup.Prepend>
                    <Form.Control
                      onChange={handleChange}
                      type="password"
                      name="passwordConfirm"
                      placeholder="Potvrdite lozniku"
                      value={values.passwordConfirm}
                      isValid={
                        touched.passwordConfirm && !errors.passwordConfirm
                      }
                      isInvalid={!!errors.passwordConfirm}
                    />
                    {errors.passwordConfirm && (
                      <Form.Control.Feedback type="invalid">
                        {errors.passwordConfirm}
                      </Form.Control.Feedback>
                    )}
                  </InputGroup>
                </Form.Group>

                <Form.Group controlId="formRegisterGender">
                  <Form.Label>Pol</Form.Label>

                  <Col>
                    {genders.map((gender, i) => {
                      return (
                        <Form.Check
                          key={i}
                          onChange={handleChange}
                          type="radio"
                          label={gender}
                          name="gender"
                          id={"formRegisterGenderRadio" + i}
                          value={gender}
                          inline
                          isValid={touched.gender && !errors.gender}
                          isInvalid={!!errors.gender}
                        />
                      );
                    })}
                  </Col>
                </Form.Group>

                <hr />
                <Button
                  className="float-right ml-2"
                  variant="light"
                  onClick={() => this.props.setRegisterModalVisibility(false)}
                >
                  Odustani
                </Button>
                <Button
                  className="float-right"
                  type="submit"
                  variant="success"
                  disabled={!(isValid && dirty)}
                >
                  Potvrdi
                </Button>
              </Form>
            )}
          </Formik>
        </Modal.Body>
      </Modal>
    );
  }
}

const mapStateToProps = state => {
  return {
    modalsVisibility: state.modalsVisibility
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators(
    { setRegisterModalVisibility, insertUser },
    dispatch
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(RegisterModal);
