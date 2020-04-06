import React, { Component } from "react";
import { Button, Table, Form } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";

import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { updateUserPassword } from "./../../redux/actions/userActions/actionCreators";

export class ChangePasswordForm extends Component {
  state = {
    password: "",
    passwordConfirm: ""
  };
  handleChangeInputValue = e => {
    const { target } = e;
    this.setState({ [target.name]: target.value });
  };
  handleOnSubmitChangePasswordForm = e => {
    e.preventDefault();
    const { password, passwordConfirm } = this.state;
    password === passwordConfirm
      ? this.props.updateUserPassword({
          userId: this.props.user.userId,
          password: this.state.password
        })
      : console.error("Lozinke se ne poklapaju");

    this.setState({ password: "", passwordConfirm: "" });
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
              <Form onSubmit={this.handleOnSubmitChangePasswordForm}>
                <Form.Group controlId="formBasicResetPasswordOld">
                  <Form.Control
                    onChange={this.handleChangeInputValue}
                    type="password"
                    name="password"
                    placeholder="Nova lozinka"
                    value={this.state.password}
                    required
                  />
                </Form.Group>
                <Form.Group controlId="formBasicResetPasswordNew">
                  <Form.Control
                    onChange={this.handleChangeInputValue}
                    type="password"
                    name="passwordConfirm"
                    placeholder="Potvrda lozinke"
                    value={this.state.passwordConfirm}
                    required
                  />
                </Form.Group>
                <Button variant="outline-dark" type="submit" block>
                  Sačuvaj
                </Button>
              </Form>
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
