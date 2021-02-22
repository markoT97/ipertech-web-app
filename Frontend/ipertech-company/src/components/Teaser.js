import React, { useState } from "react";
import { Card, CardDeck, Carousel, Image } from "react-bootstrap";
import FadeIn from "react-fade-in";
import { BACKEND_URL } from "../shared/constants";

/**
 * Teaser component
 *
 * @param {Array<Object>} data - eg. [{notificationId: 1, imageLocation: "...location", title: "Example title", content: "Example content"}, {}, {...}]
 * @param {boolean} slider - Show data as slider
 *
 * @return Teaser component
 */
const Teaser = ({ data, slider }) => {
  const [slideIndex, setSlideIndex] = useState(0);

  const handleSelectPromotion = (selectedIndex, e) => {
    setSlideIndex(selectedIndex);
  };

  return slider ? (
    <Carousel
      className="mt-2 mb-2"
      activeIndex={slideIndex}
      direction={"right"}
      onSelect={handleSelectPromotion}
    >
      {data.map(({ notificationId, imageLocation, title, content }) => {
        return (
          <Carousel.Item key={notificationId}>
            <Image src={`${BACKEND_URL}/${imageLocation}`} height="240em" />
            <Carousel.Caption>
              <h3>{title}</h3>
              <p>{content}</p>
            </Carousel.Caption>
          </Carousel.Item>
        );
      })}
    </Carousel>
  ) : (
    <React.Fragment>
      <h5 className="text-danger text-uppercase">Obaveštenja</h5>
      <CardDeck>
        {data.map(({ notificationId, imageLocation, title, content }) => {
          return (
            <Card key={notificationId} bg="secondary" text="white">
              <Card.Img
                src={`${BACKEND_URL}/${imageLocation}`}
                style={{ opacity: 0.6 }}
              />
              <Card.ImgOverlay>
                <Card.Title>{title}</Card.Title>
                <FadeIn>
                  <Card.Text>{content}</Card.Text>
                </FadeIn>
              </Card.ImgOverlay>
            </Card>
          );
        })}
      </CardDeck>
    </React.Fragment>
  );
};

export default Teaser;
