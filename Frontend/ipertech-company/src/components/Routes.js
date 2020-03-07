import React, { Component } from "react";
import { Route, Switch } from "react-router-dom";
import Home from "./HomePage/Home";
import About from "./AboutPage/About";
import Internet from "./InternetPage/Internet";
import Tv from "./TvPage/Tv";
import Phone from "./PhonePage/Phone";

export class Routes extends Component {
  render() {
    return (
      <Switch>
        <Route exact path="/" component={Home}></Route>
        <Route path="/about" component={About}></Route>
        <Route path="/internet" component={Internet}></Route>
        <Route path="/tv" component={Tv}></Route>
        <Route path="/phone" component={Phone}></Route>
      </Switch>
    );
  }
}

export default Routes;
