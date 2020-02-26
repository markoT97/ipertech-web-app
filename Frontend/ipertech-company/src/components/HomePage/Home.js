import React, { Component } from "react";
import Promotion from "./Promotion";
import Notification from "./Notification";
import About from "./About";

export class Home extends Component {
  render() {
    return (
      <div>
        <Promotion />
        <Notification />
        <About />
      </div>
    );
  }
}

export default Home;
