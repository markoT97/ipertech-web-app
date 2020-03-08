import React, { Component } from "react";
import { Modal, Form, InputGroup, Button } from "react-bootstrap";
import setVisibility from "./../../redux/actions/loginModalActions/actionCreators";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import * as Icon from "react-bootstrap-icons";

export class LoginModal extends Component {
  render() {
    return (
      <Modal
        aria-labelledby="contained-modal-title-vcenter"
        centered
        show={this.props.visibility}
        onHide={() => this.props.setVisibility(false)}
      >
        <Modal.Header closeButton className="bg-success text-white">
          <Modal.Title id="contained-modal-title-vcenter">Prijava</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group controlId="formBasicEmail">
              <Form.Label>Imejl adresa</Form.Label>
              <InputGroup>
                <InputGroup.Prepend>
                  <InputGroup.Text id="btnGroupAddon" className="text-success">
                    @
                  </InputGroup.Text>
                </InputGroup.Prepend>
                <Form.Control type="email" placeholder="Unesite imejl adresu" />
              </InputGroup>
            </Form.Group>

            <Form.Group controlId="formBasicPassword">
              <Form.Label>Lozinka</Form.Label>
              <InputGroup>
                <InputGroup.Prepend>
                  <InputGroup.Text id="btnGroupAddon">
                    <Icon.LockFill className="text-success" />
                  </InputGroup.Text>
                </InputGroup.Prepend>
                <Form.Control type="password" placeholder="Lozinka" />
              </InputGroup>
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="success">Potvrdi</Button>
          <Button
            variant="light"
            onClick={() => this.props.setVisibility(false)}
          >
            Odustani
          </Button>
        </Modal.Footer>
      </Modal>
    );
  }
}

const mapStateToProps = state => {
  return {
    visibility: state.loginModalVisibility
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({ setVisibility }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(LoginModal);
