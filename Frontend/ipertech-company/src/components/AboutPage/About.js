import React, { Component } from "react";
import { Image } from "react-bootstrap";
import { BACKEND_URL } from "../../redux/actions/backendServerSettings";

export class About extends Component {
  render() {
    return (
      <div className="m-2">
        <h5 className="text-danger text-uppercase">O Nama</h5>

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

        <Image src={BACKEND_URL + "/about/about-us-photo-1.jpg"} fluid />

        <p className="text-justify">
          U našem timu se nalaze mladi i talentovani ljudi koji su maksimalno
          posvećeni korisnicima i rešiće svaki problem u najkraćem roku.
        </p>

        <Image src={BACKEND_URL + "/about/about-us-photo-2.jpg"} fluid />
      </div>
    );
  }
}

export default About;
