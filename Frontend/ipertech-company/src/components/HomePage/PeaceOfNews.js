import React, { Component } from "react";
import { Card } from "react-bootstrap";
import FadeIn from "react-fade-in";
import { BACKEND_URL } from "../../shared/constants";

export class PeaceOfNews extends Component {
  render() {
    const { piece } = this.props;
    return (
      <Card bg="secondary" text="white">
        <Card.Img
          src={BACKEND_URL + "/" + piece.imageLocation}
          style={{ opacity: 0.6 }}
        />
        <Card.ImgOverlay>
          <Card.Title>{piece.title}</Card.Title>
          <FadeIn>
            <Card.Text>{piece.content}</Card.Text>
          </FadeIn>
        </Card.ImgOverlay>
      </Card>
    );
  }
}

export default PeaceOfNews;
