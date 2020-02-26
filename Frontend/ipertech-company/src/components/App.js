import React from "react";
import "./App.scss";
import Navigation from "./HomePage/Navigation";
import Routes from "./Routes";
import { BrowserRouter as Router } from "react-router-dom";

function App() {
  return (
    <div className="App">
      <Router>
        <Navigation />
        <Routes />
      </Router>
    </div>
  );
}

export default App;
