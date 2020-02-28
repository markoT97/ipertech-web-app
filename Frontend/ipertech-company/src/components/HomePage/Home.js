import React, { Component } from "react";
import { Promotions } from "./Promotions";
import Notification from "./Notification";
import About from "./About";
import Advert from "./Advert";

export class Home extends Component {
  render() {
    return (
      <React.Fragment>
        <Promotions />
        <Notification />
        <Advert />
        <About />
      </React.Fragment>
    );
  }
}

export default Home;
