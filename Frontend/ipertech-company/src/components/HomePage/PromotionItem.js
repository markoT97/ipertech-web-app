import React, { Component } from "react";
import { Carousel, Image } from "react-bootstrap";

export class PromotionItem extends Component {
  render() {
    const { item } = this.props;
    return (
      <Carousel.Item>
        <Image src={item.imageLocation} height="240em" />
        <Carousel.Caption>
          <h3>{item.title}</h3>
          <p>{item.content}</p>
        </Carousel.Caption>
      </Carousel.Item>

      /*
      <Carousel.Item key={i}>
              <Image src={BACKEND_URL + "/" + p.imageLocation} height="240em" />
              <Carousel.Caption>
                <h3>{p.title}</h3>
                <p>{p.content}</p>
              </Carousel.Caption>
            </Carousel.Item>
      */
    );
  }
}

export default PromotionItem;
