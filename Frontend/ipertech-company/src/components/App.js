import React from "react";
import "./App.scss";
import Navigation from "./Navigation";
import Routes from "./Routes";
import Footer from "./Footer";
import { BrowserRouter as Router } from "react-router-dom";
import { Container, Row, Col } from "react-bootstrap";
import LoginModal from "./UserLoginPage/LoginModal";

function App() {
  return (
    <div className="App">
      <Router>
        <Navigation />
        <LoginModal />
        <Container>
          <Row className="justify-content-md-center">
            <Col xs lg={10}>
              <Routes />
            </Col>
          </Row>
        </Container>
        <Footer />
      </Router>
    </div>
  );
}

export default App;
