import React, { Component } from "react";
import { Badge, Row, Col } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";

export class About extends Component {
  render() {
    return (
      <div className="m-2">
        <h5 className="text-danger text-uppercase">Ukratko o nama</h5>
        <p className="text-justify">
          Mi smo IperTech, najveći internet provajder u regionu. Naša misija je
          da pružimo korisnicima najbolje moguće iskustvo u kratkom vremenskom
          roku i po najpovoljnijoj ceni. Naša firma postoji 5 godina i za taj
          vremenski period je dosegla top 10 kompanija ovoga tipa u svetu!
          Postanite naš korisnik i osetite pravu brzinu, kvalitet i usluge
          interneta, televizije i telefonije! Još uvek nismo zadovoljni našom
          uslugom i nikada i nećemo biti jer je naš cilj konstantan napredak u
          tehnologiji, znanju i infrastrukturi. Svake godine menjamo
          insfrastrukturu i taj trend ćemo nastaviti i ubuduće! Postanite naš
          korisnik, popunjavanjem upitnika na našem sajtu. Procedura je da Vas
          nakon popunjavanja upitnika kontaktiraju naši operateri u roku od
          jednog sata!
        </p>
        <Row className="text-center">
          <Col md={3}>
            <h1>
              <Badge variant="secondary">
                <Icon.Alarm />
              </Badge>
            </h1>
            <p className="text-danger">Tačnost</p>
          </Col>

          <Col md={3}>
            <h1>
              <Badge variant="secondary">
                <Icon.BookHalfFill />
              </Badge>
            </h1>
            <p className="text-danger">Znanje</p>
          </Col>

          <Col md={3}>
            <h1>
              <Badge variant="secondary">
                <Icon.GearWideConnected />
              </Badge>
            </h1>
            <p className="text-danger">Iskustvo</p>
          </Col>

          <Col md={3}>
            <h1>
              <Badge variant="secondary">
                <Icon.People />
              </Badge>
            </h1>
            <p className="text-danger">Ljubaznost</p>
          </Col>
        </Row>
      </div>
    );
  }
}

export default About;
