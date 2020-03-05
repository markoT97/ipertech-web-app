import React, { Component } from "react";
import { CardDeck } from "react-bootstrap";
import PeaceOfNews from "./PeaceOfNews";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import fetchNews from "../../redux/actions/newsActions/actionCreators";

export class Notification extends Component {
  componentDidMount() {
    this.props.fetchNews();
  }
  render() {
    const news = this.props.news;
    return (
      <React.Fragment>
        <h5 className="text-danger text-uppercase">Obaveštenja</h5>
        <CardDeck>
          {news.map((n, i) => {
            return <PeaceOfNews key={i} piece={n} />;
          })}
        </CardDeck>
      </React.Fragment>
    );
  }
}

const mapStateToProps = state => {
  return {
    news: state.news
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({ fetchNews }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(Notification);
