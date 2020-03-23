import React from "react";
import "./App.scss";
import Navigation from "./Navigation";
import Routes from "./Routes";
import Footer from "./Footer";
import { BrowserRouter as Router } from "react-router-dom";
import { Container, Row, Col } from "react-bootstrap";
import LoginModal from "./UserEntryPages/LoginModal";
import RegisterModal from "./UserEntryPages/RegisterModal";

function App() {
  return (
    <div className="App">
      <Router>
        <Navigation />
        <LoginModal />
        <RegisterModal />
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
