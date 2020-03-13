import React, { Component } from "react";
import { Navbar, Nav, Image } from "react-bootstrap";
import logo from "./../logo.svg";
import { Link } from "react-router-dom";
import setVisibility from "./../redux/actions/loginModalActions/actionCreators";
import { logoutUser } from "../redux/actions/authActions/actionCreators";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";

export class Navigation extends Component {
  render() {
    const { auth } = this.props;

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
        <Nav.Link as={Link} to="/register" className="text-danger">
          Registruj se
        </Nav.Link>

        <Nav.Link
          onClick={() => this.props.setVisibility(true)}
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

          <Nav className="ml-auto">
            {auth.isAuthenticated ? userLinks : guestLinks}
          </Nav>
        </Navbar.Collapse>
      </Navbar>
    );
  }
}

const mapStateToProps = state => {
  return {
    visibility: state.loginModalVisibility,
    auth: state.auth
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({ setVisibility, logoutUser }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(Navigation);
