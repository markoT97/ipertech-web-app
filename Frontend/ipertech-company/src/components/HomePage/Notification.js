import React, { Component } from "react";
import { CardDeck, Card } from "react-bootstrap";
import FadeIn from "react-fade-in";

export class Notification extends Component {
  render() {
    return (
      <React.Fragment>
        <h5 className="text-danger text-uppercase">Obaveštenja</h5>
        <CardDeck>
          <Card bg="secondary" text="white">
            <Card.Img
              src="https://images.unsplash.com/photo-1519389950473-47ba0277781c?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80"
              style={{ opacity: 0.6 }}
            />
            <Card.ImgOverlay>
              <Card.Title>Card Title</Card.Title>
              <FadeIn>
                <Card.Text>
                  Some quick example text to build on the card title and make up
                  the bulk of the card's content.
                </Card.Text>
              </FadeIn>
            </Card.ImgOverlay>
          </Card>

          <Card bg="secondary" text="white">
            <Card.Img
              src="https://images.unsplash.com/photo-1519389950473-47ba0277781c?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80"
              style={{ opacity: 0.6 }}
            />
            <Card.ImgOverlay>
              <Card.Title>Card Title</Card.Title>
              <FadeIn>
                <Card.Text>
                  Some quick example text to build on the card title and make up
                  the bulk of the card's content.
                </Card.Text>
              </FadeIn>
            </Card.ImgOverlay>
          </Card>

          <Card bg="secondary" text="white">
            <Card.Img
              src="https://images.unsplash.com/photo-1519389950473-47ba0277781c?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80"
              style={{ opacity: 0.6 }}
            />
            <Card.ImgOverlay>
              <Card.Title>Card Title</Card.Title>
              <FadeIn>
                <Card.Text>
                  Some quick example text to build on the card title and make up
                  the bulk of the card's content.
                </Card.Text>
              </FadeIn>
            </Card.ImgOverlay>
          </Card>
        </CardDeck>
      </React.Fragment>
    );
  }
}

export default Notification;
