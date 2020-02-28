import React, { Component } from "react";
import { Container, Row, Col, Nav, Navbar, Image } from "react-bootstrap";
import { Link } from "react-router-dom";
import footerLogo from "./../footerLogo.svg";

export class Footer extends Component {
  render() {
    return (
      <footer className="py-4 bg-danger text-white-50">
        <Container>
          <Row className="text-sm-left text-md-left">
            <Col>
              <Navbar sticky="bottom" className="border-top">
                <Navbar.Collapse id="responsive-navbar-nav">
                  <Nav className="mr-auto text-nowrap">
                    <Row>
                      <Col lg={2} sm={12}>
                        <Nav.Link as={Link} to="/">
                          Naslovna
                        </Nav.Link>
                      </Col>

                      <Col lg={2} sm={12}>
                        <Nav.Link as={Link} to="/about">
                          O nama
                        </Nav.Link>
                      </Col>

                      <Col lg={2} sm={12}>
                        <Nav.Link as={Link} to="/internet">
                          Internet
                        </Nav.Link>
                      </Col>

                      <Col lg={2} sm={12}>
                        <Nav.Link as={Link} to="/tv">
                          Televizija
                        </Nav.Link>
                      </Col>

                      <Col lg={2} sm={12}>
                        <Nav.Link as={Link} to="/phone">
                          Telefonija
                        </Nav.Link>
                      </Col>

                      <Col lg={2} sm={12}>
                        <Nav.Link as={Link} to="/about">
                          Paketi
                        </Nav.Link>
                      </Col>
                    </Row>
                  </Nav>
                </Navbar.Collapse>
              </Navbar>
            </Col>

            <Col lg={3} sm={12}>
              <Image
                src={footerLogo}
                width="300"
                className="p-3"
                style={{ transform: "rotate(30deg)", opacity: 0.3 }}
              />
            </Col>
          </Row>

          <Row>
            <Col className="text-center text-white">
              <p className="text-warning">Design by Marko Tričković</p>
              <h6>&copy; All rights Reserved. Ipertech Company</h6>
            </Col>
          </Row>
        </Container>
      </footer>
    );
  }
}

export default Footer;
