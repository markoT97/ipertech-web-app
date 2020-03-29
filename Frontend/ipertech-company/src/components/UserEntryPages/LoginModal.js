import React, { Component } from "react";
import { Modal, Form, InputGroup, Button } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { setLoginModalVisibility } from "../../redux/actions/modalsActions/actionCreators";
import { loginUser } from "../../redux/actions/authActions/actionCreators";

export class LoginModal extends Component {
  state = {
    email: "",
    password: ""
  };
  handleChangeInputValue = e => {
    const { target } = e;
    this.setState({ [target.type]: target.value });
  };
  handleOnSubmitLoginForm = e => {
    e.preventDefault();
    this.props.loginUser(this.state.email, this.state.password);
    this.props.setLoginModalVisibility(false);

    this.setState({ email: "", password: "" });
  };
  render() {
    return (
      <Modal
        onSubmit={this.handleOnSubmitLoginForm}
        aria-labelledby="contained-modal-title-vcenter"
        centered
        show={this.props.modalsVisibility.loginModalVisibility}
        onHide={() => this.props.setLoginModalVisibility(false)}
      >
        <Modal.Header closeButton className="bg-success text-white">
          <Modal.Title id="contained-modal-title-vcenter">Prijava</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group controlId="formLoginEmail">
              <Form.Label>Imejl adresa</Form.Label>
              <InputGroup>
                <InputGroup.Prepend>
                  <InputGroup.Text id="btnGroupAddon" className="text-success">
                    @
                  </InputGroup.Text>
                </InputGroup.Prepend>
                <Form.Control
                  onChange={this.handleChangeInputValue}
                  type="email"
                  placeholder="Unesite imejl adresu"
                  value={this.state.email}
                />
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
                  onChange={this.handleChangeInputValue}
                  type="password"
                  placeholder="Lozinka"
                  value={this.state.password}
                />
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
            <Button className="float-right" type="submit" variant="success">
              Potvrdi
            </Button>
          </Form>
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
  return bindActionCreators({ setLoginModalVisibility, loginUser }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(LoginModal);
