import React, { Component } from "react";
import { Modal, Form, InputGroup, Button, Image, Col } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { setRegisterModalVisibility } from "../../redux/actions/modalsActions/actionCreators";
import { insertUser } from "../../redux/actions/userActions/actionCreators";
import { BACKEND_URL } from "../../redux/actions/backendServerSettings";

export class RegisterModal extends Component {
  state = {
    userContractId: "082c90f1-f513-4b2d-acf1-14b277d6d6c8",
    firstName: "",
    lastName: "",
    gender: "",
    email: "",
    phoneNumber: "",
    password: "",
    passwordConfirm: "",

    showBillExample: false
  };
  handleChangeInputValue = e => {
    const { target } = e;
    this.setState({ [target.name]: target.value });
  };
  handleOnSubmitRegisterForm = e => {
    e.preventDefault();
    this.props.insertUser({
      userContract: {
        userContractId: this.state.userContractId
      },
      firstName: this.state.firstName,
      lastName: this.state.lastName,
      gender: this.state.gender,
      email: this.state.email,
      phoneNumber: this.state.phoneNumber,
      password: this.state.password,
      passwordConfirm: this.state.passwordConfirm
    });
    this.props.setRegisterModalVisibility(false);

    this.setState({
      userContractId: "",
      firstName: "",
      lastName: "",
      gender: "",
      email: "",
      phoneNumber: "",
      password: "",
      passwordConfirm: ""
    });
  };
  render() {
    return (
      <Modal
        onSubmit={this.handleOnSubmitRegisterForm}
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
          <Form>
            <Form.Group controlId="formRegisterContractID">
              <Form.Label>Broj ugovora</Form.Label>
              <InputGroup>
                <InputGroup.Prepend>
                  <InputGroup.Text id="btnGroupAddon" className="text-success">
                    #
                  </InputGroup.Text>
                </InputGroup.Prepend>
                <Form.Control
                  onChange={this.handleChangeInputValue}
                  onFocus={() => this.setState({ showBillExample: true })}
                  onBlur={() => this.setState({ showBillExample: false })}
                  type="text"
                  name="userContractId"
                  placeholder="Unesite broj vašeg ugovora"
                  value={this.state.userContractId}
                />
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
                  <InputGroup.Text id="btnGroupAddon" className="text-success">
                    I
                  </InputGroup.Text>
                </InputGroup.Prepend>
                <Form.Control
                  onChange={this.handleChangeInputValue}
                  type="text"
                  name="firstName"
                  placeholder="Unesite vaše ime"
                  value={this.state.firstName}
                />
              </InputGroup>
            </Form.Group>

            <Form.Group controlId="formRegisterLastName">
              <Form.Label>Prezime</Form.Label>
              <InputGroup>
                <InputGroup.Prepend>
                  <InputGroup.Text id="btnGroupAddon" className="text-success">
                    P
                  </InputGroup.Text>
                </InputGroup.Prepend>
                <Form.Control
                  onChange={this.handleChangeInputValue}
                  type="text"
                  name="lastName"
                  placeholder="Unesite vaše prezime"
                  value={this.state.lastName}
                />
              </InputGroup>
            </Form.Group>

            <Form.Group controlId="formRegisterEmail">
              <Form.Label>Imejl</Form.Label>
              <InputGroup>
                <InputGroup.Prepend>
                  <InputGroup.Text id="btnGroupAddon" className="text-success">
                    @
                  </InputGroup.Text>
                </InputGroup.Prepend>
                <Form.Control
                  onChange={this.handleChangeInputValue}
                  type="email"
                  name="email"
                  placeholder="Unesite vašu imejl adresu"
                  value={this.state.email}
                />
              </InputGroup>
            </Form.Group>

            <Form.Group controlId="formRegisterPhoneNumber">
              <Form.Label>Broj telefona</Form.Label>
              <InputGroup>
                <InputGroup.Prepend>
                  <InputGroup.Text id="btnGroupAddon" className="text-success">
                    <Icon.Phone />
                  </InputGroup.Text>
                </InputGroup.Prepend>
                <Form.Control
                  onChange={this.handleChangeInputValue}
                  type="text"
                  name="phoneNumber"
                  placeholder="Unesite vaš broj telefona"
                  value={this.state.phoneNumber}
                />
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
                  onChange={this.handleChangeInputValue}
                  type="password"
                  name="password"
                  placeholder="Lozinka"
                  value={this.state.password}
                />
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
                  onChange={this.handleChangeInputValue}
                  type="password"
                  name="passwordConfirm"
                  placeholder="Potvrdite lozniku"
                  value={this.state.passwordConfirm}
                />
              </InputGroup>
            </Form.Group>

            <Form.Group controlId="formRegisterGender">
              <Form.Label>Pol</Form.Label>

              <Col>
                <Form.Check
                  onChange={this.handleChangeInputValue}
                  type="radio"
                  label="Muški"
                  name="gender"
                  id="formRegisterGenderRadio1"
                  value="Muški"
                  inline
                />
                <Form.Check
                  onChange={this.handleChangeInputValue}
                  type="radio"
                  label="Ženski"
                  name="gender"
                  id="formRegisterGenderRadio2"
                  value="Ženski"
                  inline
                />
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
  return bindActionCreators(
    { setRegisterModalVisibility, insertUser },
    dispatch
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(RegisterModal);
