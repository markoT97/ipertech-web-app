import React, { Component } from "react";
import { Route, Switch } from "react-router-dom";
import Home from "./HomePage/Home";
import About from "./AboutPage/About";
import Internet from "./InternetPage/Internet";
import Tv from "./TvPage/Tv";
import Phone from "./PhonePage/Phone";
import PacketCombinations from "./PacketCombinationsPage/PacketCombinations";
import AuthRoute from "./../utils/AuthRoute";
import UserProfile from "./UserProfilePage/UserProfile";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";

export class Routes extends Component {
  render() {
    return (
      <Switch>
        <Route exact path="/" component={Home}></Route>
        <Route path="/about" component={About}></Route>
        <Route path="/internet" component={Internet}></Route>
        <Route path="/tv" component={Tv}></Route>
        <Route path="/phone" component={Phone}></Route>
        <Route path="/packets" component={PacketCombinations}></Route>
        <AuthRoute path="/user-profile" component={UserProfile}></AuthRoute>
      </Switch>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    user: state.user,
  };
};

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({}, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(Routes);
