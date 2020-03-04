import React, { Component } from "react";
import { Promotions } from "./Promotions";
import News from "./News";
import About from "./About";
import Advert from "./Advert";

export class Home extends Component {
  render() {
    return (
      <React.Fragment>
        <Promotions />
        <News />
        <Advert />
        <About />
      </React.Fragment>
    );
  }
}

export default Home;
