import React, { Component } from "react";
import { Button, Table, Form } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";

import { bindActionCreators } from "redux";
import { connect } from "react-redux";

export class ChangePasswordForm extends Component {
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
              <Form>
                <Form.Group controlId="formBasicResetPasswordOld">
                  <Form.Control type="password" placeholder="Stara lozinka" />
                </Form.Group>
                <Form.Group controlId="formBasicResetPasswordNew">
                  <Form.Control type="password" placeholder="Nova lozinka" />
                </Form.Group>
              </Form>
            </td>
          </tr>
          <tr>
            <td>
              <Button variant="outline-dark" block>
                Sačuvaj
              </Button>
            </td>
          </tr>
        </tbody>
      </Table>
    );
  }
}

const mapStateToProps = state => {
  return {};
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({}, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(ChangePasswordForm);
