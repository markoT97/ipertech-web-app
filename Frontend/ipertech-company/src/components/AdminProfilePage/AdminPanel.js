import React, { Component } from "react";
import { connect } from "react-redux";

export class AdminPanel extends Component {
  render() {
    return <div>ADMIN PANEL</div>;
  }
}

const mapStateToProps = (state) => ({});

const mapDispatchToProps = {};

export default connect(mapStateToProps, mapDispatchToProps)(AdminPanel);
