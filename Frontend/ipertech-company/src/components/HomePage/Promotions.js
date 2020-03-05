import React, { Component } from "react";
import { Carousel, Image } from "react-bootstrap";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import fetchNotifications from "../../redux/actions/notificationsActions/actionCreators";

export class Promotions extends Component {
  state = {
    index: 0,
    direction: null
  };

  componentDidMount() {
    //this.props.fetchNotifications("promocije");
    console.log(this.props);
  }

  handleSelect = (selectedIndex, e) => {
    this.setState({ index: selectedIndex });
    this.setState({ direction: e.direction });
  };

  render() {
    return (
      <Carousel
        className="mt-2 mb-2"
        activeIndex={this.state.index}
        direction={this.state.direction}
        onSelect={this.handleSelect}
      >
        <Carousel.Item>
          <Image
            src="http://ipertech.somee.com/Content/images/news/vajrles.jpg"
            height="240em"
          />
          <Carousel.Caption>
            <h3>First slide label</h3>
            <p>Nulla vitae elit libero, a pharetra augue mollis interdum.</p>
          </Carousel.Caption>
        </Carousel.Item>
      </Carousel>
    );
  }
}

const mapStateToProps = state => {
  return {
    notifications: state.notifications
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({ fetchNotifications }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(Promotions);
