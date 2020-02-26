import React, { Component } from "react";
import { Navbar, Nav } from "react-bootstrap";
import logo from "./../../logo.svg";
import "bootstrap/dist/css/bootstrap.min.css";
import { Link } from "react-router-dom";

export class Navigation extends Component {
  render() {
    return (
      <Navbar collapseOnSelect expand="lg" bg="light" variant="light">
        <Navbar.Brand as={Link} to="">
          <img
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

            <Nav.Link as={Link} to="/about">
              Paketi
            </Nav.Link>
          </Nav>

          <Nav className="ml-auto">
            <Nav.Link as={Link} to="/register" className="text-danger">
              Registruj se
            </Nav.Link>

            <Nav.Link as={Link} to="/login" className="text-danger">
              Prijavi se
            </Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Navbar>
    );
  }
}

export default Navigation;
