import React, { Component } from "react";
import { Carousel, Image } from "react-bootstrap";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import fetchPromotions from "../../redux/actions/promotionsActions/actionCreators";
import { BACKEND_URL } from "../../redux/actions/backendServerSettings";

export class Promotions extends Component {
  state = {
    index: 0,
    direction: null
  };

  componentDidMount() {
    this.props.fetchPromotions();
  }

  handleSelect = (selectedIndex, e) => {
    this.setState({ index: selectedIndex });
    this.setState({ direction: e ? e.direction : null });
  };

  render() {
    const { promotions } = this.props;
    return (
      <Carousel
        className="mt-2 mb-2"
        activeIndex={this.state.index}
        direction={this.state.direction}
        onSelect={this.handleSelect}
      >
        {promotions.map((p, i) => {
          return (
            <Carousel.Item key={i}>
              <Image src={BACKEND_URL + "/" + p.imageLocation} height="240em" />
              <Carousel.Caption>
                <h3>{p.title}</h3>
                <p>{p.content}</p>
              </Carousel.Caption>
            </Carousel.Item>
          );
        })}
      </Carousel>
    );
  }
}

const mapStateToProps = state => {
  return {
    promotions: state.promotions
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({ fetchPromotions }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(Promotions);
