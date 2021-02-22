import React, { Component } from "react";
import { Navbar, Nav, Image, Dropdown, DropdownButton } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import logo from "./../logo.svg";
import { Link } from "react-router-dom";
import {
  setLoginModalVisibility,
  setRegisterModalVisibility,
} from "../redux/actions/modalsActions/actionCreators";
import { logoutUser } from "../redux/actions/authActions/actionCreators";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";

export class Navigation extends Component {
  render() {
    const { auth } = this.props;

    const adminLinks = (
      <React.Fragment>
        <Nav.Link as={Link} to="/admin-panel" className="text-success">
          Admin Panel
        </Nav.Link>

        <Nav.Link
          onClick={() => this.props.logoutUser()}
          className="text-success"
        >
          Odjavi se
        </Nav.Link>
      </React.Fragment>
    );

    const userLinks = (
      <React.Fragment>
        <Nav.Link as={Link} to="/user-profile" className="text-success">
          {auth.user.firstName + " " + auth.user.lastName}
        </Nav.Link>

        <Nav.Link
          onClick={() => this.props.logoutUser()}
          className="text-success"
        >
          Odjavi se
        </Nav.Link>
      </React.Fragment>
    );

    const guestLinks = (
      <React.Fragment>
        <Nav.Link
          onClick={() => this.props.setRegisterModalVisibility(true)}
          className="text-danger"
        >
          Registruj se
        </Nav.Link>

        <Nav.Link
          onClick={() => this.props.setLoginModalVisibility(true)}
          className="text-danger"
        >
          Prijavi se
        </Nav.Link>
      </React.Fragment>
    );

    return (
      <Navbar collapseOnSelect expand="lg" bg="light" variant="light">
        <Navbar.Brand as={Link} to="/">
          <Image
            alt=""
            src={logo}
            width="50"
            height="50"
            className="d-inline-block align-top"
          />
        </Navbar.Brand>
        <div className="d-flex order-lg-1 ml-auto pr-2 d-block d-lg-none">
          <Dropdown>
            <DropdownButton
              title={
                <Icon.PersonFill
                  size={42}
                  className={
                    (auth.isAuthenticated ? "text-success" : "text-danger") +
                    " border rounded"
                  }
                />
              }
              variant="outline-light"
              drop="left"
            >
              {auth.isAuthenticated
                ? (auth.user.role === "User" && userLinks) ||
                  (auth.user.role === "Admin" && adminLinks)
                : guestLinks}
            </DropdownButton>
          </Dropdown>
        </div>

        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="mr-auto">
            <Nav.Link as={Link} to="/">
              Naslovna
            </Nav.Link>

            <Nav.Link as={Link} to="/about">
              O nama
            </Nav.Link>

            <Nav.Link as={Link} to="/internet">
              Internet
            </Nav.Link>

            <Nav.Link as={Link} to="/tv">
              Televizija
            </Nav.Link>

            <Nav.Link as={Link} to="/phone">
              Telefonija
            </Nav.Link>

            <Nav.Link as={Link} to="/packets">
              Paketi
            </Nav.Link>
          </Nav>
        </Navbar.Collapse>
        <Nav className="d-none d-lg-flex">
          {auth.isAuthenticated
            ? (auth.user.role === "User" && userLinks) ||
              (auth.user.role === "Admin" && adminLinks)
            : guestLinks}
        </Nav>
      </Navbar>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    modalsVisibility: state.modalsVisibility,
    auth: state.auth,
  };
};

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators(
    { setLoginModalVisibility, setRegisterModalVisibility, logoutUser },
    dispatch
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(Navigation);
