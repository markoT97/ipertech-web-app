import React, { Component } from "react";
import { Image } from "react-bootstrap";

export class Advert extends Component {
  render() {
    return (
      <div className="mt-2 mb-2">
        <h5 className="text-danger text-uppercase">Izdvajamo iz ponude</h5>
        <Image
          src="http://ipertech.somee.com/Content/images/home/animation.gif"
          fluid
        />
      </div>
    );
  }
}

export default Advert;
