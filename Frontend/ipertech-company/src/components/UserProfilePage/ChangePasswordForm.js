import React, { Component } from "react";
import { Button, Table, Form } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";

import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { updateUserPassword } from "./../../redux/actions/userActions/actionCreators";
import { Formik } from "formik";
import { changePasswordValidationSchema as schema } from "./../../shared/validation";

export class ChangePasswordForm extends Component {
  handleOnSubmitChangePasswordForm = (values, { resetForm }) => {
    this.props.updateUserPassword({
      userId: this.props.user.userId,
      password: values.password
    });
    resetForm({});
  };
  render() {
    return (
      <Table striped responsive className="text-center">
        <thead>
          <tr>
            <th className="bg-dark text-white">
              <Icon.LockFill size={35} />
              Promena lozinke
            </th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>
              <Formik
                validationSchema={schema}
                onSubmit={this.handleOnSubmitChangePasswordForm}
                initialValues={{ password: "", passwordConfirmation: "" }}
              >
                {({
                  handleSubmit,
                  handleChange,
                  handleReset,
                  handleBlur,
                  setValues,
                  values,
                  touched,
                  isValid,
                  errors,
                  dirty,
                  status
                }) => (
                  <Form onSubmit={handleSubmit}>
                    <Form.Group controlId="formBasicResetPasswordOld">
                      <Form.Control
                        onChange={handleChange}
                        type="password"
                        name="password"
                        placeholder="Nova lozinka"
                        value={values.password}
                        isValid={touched.password && !errors.password}
                        isInvalid={!!errors.password}
                      />

                      {errors.password && (
                        <Form.Control.Feedback type="invalid">
                          {errors.password}
                        </Form.Control.Feedback>
                      )}
                    </Form.Group>
                    <Form.Group controlId="formBasicResetPasswordNew">
                      <Form.Control
                        onChange={handleChange}
                        type="password"
                        name="passwordConfirmation"
                        placeholder="Potvrda lozinke"
                        value={values.passwordConfirmation}
                        isValid={
                          touched.passwordConfirmation &&
                          !errors.passwordConfirmation
                        }
                        isInvalid={!!errors.passwordConfirmation}
                      />

                      {errors.passwordConfirmation && (
                        <Form.Control.Feedback type="invalid">
                          {errors.passwordConfirmation}
                        </Form.Control.Feedback>
                      )}
                    </Form.Group>
                    <Button
                      variant="outline-dark"
                      type="submit"
                      block
                      disabled={!(isValid && dirty)}
                    >
                      Sačuvaj
                    </Button>
                  </Form>
                )}
              </Formik>
            </td>
          </tr>
        </tbody>
      </Table>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.user
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({ updateUserPassword }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(ChangePasswordForm);
