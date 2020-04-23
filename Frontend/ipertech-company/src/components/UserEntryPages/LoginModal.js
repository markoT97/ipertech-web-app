import React, { Component } from "react";
import { Modal, Form, InputGroup, Button } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { setLoginModalVisibility } from "../../redux/actions/modalsActions/actionCreators";
import { loginUser } from "../../redux/actions/authActions/actionCreators";
import { Formik } from "formik";
import { loginValidationSchema as schema } from "./../../shared/validation-schemas";

export class LoginModal extends Component {
  handleOnSubmitLoginForm = (form) => {
    this.props.loginUser(form.email, form.password);
    this.props.setLoginModalVisibility(false);
  };
  render() {
    return (
      <Modal
        aria-labelledby="contained-modal-title-vcenter"
        centered
        show={this.props.modalsVisibility.loginModalVisibility}
        onHide={() => this.props.setLoginModalVisibility(false)}
      >
        <Modal.Header closeButton className="bg-success text-white">
          <Modal.Title id="contained-modal-title-vcenter">Prijava</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Formik
            validationSchema={schema}
            onSubmit={this.handleOnSubmitLoginForm}
            initialValues={{ email: "", password: "" }}
          >
            {({
              handleSubmit,
              handleChange,
              handleBlur,
              values,
              touched,
              isValid,
              errors,
              dirty,
            }) => (
              <Form noValidate onSubmit={handleSubmit}>
                <Form.Group controlId="formLoginEmail">
                  <Form.Label>Imejl adresa</Form.Label>
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
                      placeholder="Unesite imejl adresu"
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

                <Form.Group controlId="formLoginPassword">
                  <Form.Label>Lozinka</Form.Label>
                  <InputGroup>
                    <InputGroup.Prepend>
                      <InputGroup.Text id="btnGroupAddon">
                        <Icon.LockFill className="text-success" />
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
                <hr />
                <Button
                  className="float-right ml-2"
                  variant="light"
                  onClick={() => this.props.setLoginModalVisibility(false)}
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

const mapStateToProps = (state) => {
  return {
    modalsVisibility: state.modalsVisibility,
  };
};

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({ setLoginModalVisibility, loginUser }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(LoginModal);
