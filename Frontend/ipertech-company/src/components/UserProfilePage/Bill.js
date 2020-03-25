import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { setBillModalVisibility } from "../../redux/actions/modalsActions/actionCreators";
import { format } from "date-fns";

export class Bill extends Component {
  render() {
    const { selectedBill } = this.props.bills;
    const vat = (selectedBill.price * 20) / 10;
    return (
      <svg
        id="bill"
        version="1.1"
        xmlns="http://www.w3.org/2000/svg"
        xmlnsXlink="http://www.w3.org/1999/xlink"
        x="0px"
        y="0px"
        viewBox="0 0 459.416 225.126"
        enableBackground="new 0 0 459.416 225.126"
        xmlSpace="preserve"
      >
        <g id="layer-1">
          <rect
            x="0.5"
            y="0.542"
            fill="#FFFFFF"
            stroke="#000000"
            strokeMiterlimit="10"
            width="458.5"
            height="224"
          />

          <rect
            id="_x3C_Slice_x3E_"
            x="-0.167"
            y="-0.375"
            fill="none"
            width="457"
            height="226"
          />
        </g>
        <g id="logo">
          <g>
            <path
              id="router"
              fill="none"
              stroke="#373636"
              strokeWidth="2"
              strokeMiterlimit="10"
              d="M18.585,41.111h64.052
			c0.698,0,1.264,0.565,1.264,1.263V63.22c0,0.698-0.566,1.264-1.264,1.264H18.585c-0.698,0-1.263-0.566-1.263-1.264V42.375
			C17.322,41.677,17.887,41.111,18.585,41.111z"
            />

            <path
              id="leds-bar"
              fill="#373636"
              d="M26.544,54.25h48.512c0.698,0,1.264,0.565,1.264,1.264v2.021
			c0,0.698-0.566,1.264-1.264,1.264H26.544c-0.698,0-1.264-0.566-1.264-1.264v-2.021C25.281,54.815,25.846,54.25,26.544,54.25z"
            />

            <path
              id="leds"
              fill="#6ABD45"
              d="M33.493,54.629h0.632c0.698,0,1.263,0.566,1.263,1.264v0.631c0,0.698-0.565,1.264-1.263,1.264
			h-0.632c-0.698,0-1.263-0.565-1.263-1.264v-0.631C32.229,55.195,32.795,54.629,33.493,54.629z M39.81,54.629h0.631
			c0.698,0,1.264,0.566,1.264,1.264v0.631c0,0.698-0.566,1.264-1.264,1.264H39.81c-0.698,0-1.264-0.565-1.264-1.264v-0.631
			C38.546,55.195,39.111,54.629,39.81,54.629z M46.379,54.629h0.631c0.698,0,1.264,0.566,1.264,1.264v0.631
			c0,0.698-0.565,1.264-1.264,1.264h-0.631c-0.698,0-1.264-0.565-1.264-1.264v-0.631C45.115,55.195,45.681,54.629,46.379,54.629z
			 M52.948,54.629h0.632c0.698,0,1.263,0.566,1.263,1.264v0.631c0,0.698-0.565,1.264-1.263,1.264h-0.632
			c-0.698,0-1.263-0.565-1.263-1.264v-0.631C51.685,55.195,52.25,54.629,52.948,54.629z M59.518,54.629h0.632
			c0.698,0,1.263,0.566,1.263,1.264v0.631c0,0.698-0.565,1.264-1.263,1.264h-0.632c-0.698,0-1.263-0.565-1.263-1.264v-0.631
			C58.254,55.195,58.82,54.629,59.518,54.629z M66.087,54.629h0.632c0.698,0,1.263,0.566,1.263,1.264v0.631
			c0,0.698-0.565,1.264-1.263,1.264h-0.632c-0.698,0-1.263-0.565-1.263-1.264v-0.631C64.824,55.195,65.389,54.629,66.087,54.629z"
            />

            <path
              id="iper"
              fill="#373636"
              d="M77.074,28.84l8.09,5.626l-2.524,3.631l-8.091-5.626L77.074,28.84z M66.719,12.054h3.917v26.278
			h-3.917V12.054z M70.635,12.054h13.896v3.664H70.635V12.054z M70.635,23.551h13.896v3.664H70.635V23.551z M80.868,15.718h3.664
			v7.833h-3.664V15.718z M63.813,38.332h9.475v2.779h-9.475V38.332z M53.327,13.191h12.886v2.147H53.327V13.191z M53.327,15.339
			h1.895v19.456h-1.895V15.339z M53.327,34.794h12.381v1.769H53.327V34.794z M55.222,22.667h8.212v2.779h-8.212V22.667z"
            />

            <path
              id="arrow"
              fill="none"
              stroke="#EC2227"
              strokeWidth="0.3"
              strokeMiterlimit="10"
              d="M73.703,33.783l-3.068-6.567
			l7.201,0.618L73.703,33.783z"
            />

            <rect
              x="63.813"
              y="9.148"
              fill="#EC2227"
              width="9.475"
              height="2.906"
            />

            <path
              id="tech"
              fill="#EC2227"
              d="M41.705,46.923h1.263v4.548h-1.263V46.923z M45.621,46.923h1.264v4.042h-1.264V46.923z
			 M50.8,45.659h1.264v5.685H50.8V45.659z M55.98,43.891h1.263v9.223H55.98V43.891z M59.896,43.891h1.264v9.223h-1.264V43.891z
			 M45.621,45.659h3.917v1.264h-3.917V45.659z M46.884,48.313h2.653v1.263h-2.653V48.313z M57.244,48.313h2.653v1.263h-2.653V48.313
			z M45.621,50.965h3.917v1.264h-3.917V50.965z M50.8,51.344h3.917v1.264H50.8V51.344z M50.8,44.396h3.917v1.263H50.8V44.396z
			 M40.062,45.659h4.548v1.264h-4.548V45.659z"
            />
          </g>
        </g>
        <g id="logo-description">
          <rect x="97.333" y="21.502" fill="none" width="170" height="27" />

          <text transform="matrix(1 0 0 1 97.333 27.1816)">
            <tspan
              x="0"
              y="0"
              fill="#333333"
              fontFamily="'MyriadPro-Regular'"
              fontSize="8"
            >
              Ipertech{" "}
            </tspan>
            <tspan
              x="29.463"
              y="0"
              fill="#EB2529"
              fontFamily="'MyriadPro-Regular'"
              fontSize="8"
            >
              doo
            </tspan>
            <tspan
              x="42.76"
              y="0"
              fill="#333333"
              fontFamily="'MyriadPro-Regular'"
              fontSize="8"
            >
              ,{" "}
            </tspan>
            <tspan
              x="0"
              y="9.6"
              fill="#333333"
              fontFamily="'MyriadPro-Regular'"
              fontSize="8"
            >
              Bulevar Cara Lazara 1A,
            </tspan>
            <tspan
              x="0"
              y="19.2"
              fill="#333333"
              fontFamily="'MyriadPro-Regular'"
              fontSize="8"
            >
              21000 Novi Sad
            </tspan>
          </text>
        </g>
        <g id="idUgovora">
          <text
            transform="matrix(1 0 0 1 20.5913 92.292)"
            fill="#323232"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            ID ugovora:
          </text>
          <rect
            x="20.591"
            y="98.125"
            fill="none"
            stroke="#323232"
            strokeMiterlimit="10"
            width="53.624"
            height="15.333"
          />

          <text
            id="slp-contractId"
            transform="matrix(1 0 0 1 29 108.625)"
            fill="#EB2529"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            xxx-xxx-xxx
          </text>
          <text
            transform="matrix(1 0 0 1 94.0942 92.292)"
            fill="#323232"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            Ime i prezime:
          </text>
          <rect
            x="94.094"
            y="98.125"
            fill="none"
            stroke="#323232"
            strokeMiterlimit="10"
            width="75.168"
            height="15.333"
          />

          <text
            id="slp-first-and-last-name"
            transform="matrix(1 0 0 1 106.2627 108.625)"
            fill="#323232"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            {selectedBill.firstAndLastName}
          </text>
        </g>
        <g id="acc-period">
          <rect
            x="193.415"
            y="98.125"
            fill="none"
            stroke="#323232"
            strokeMiterlimit="10"
            width="94.333"
            height="15.333"
          />

          <text
            transform="matrix(1 0 0 1 192.8311 92.292)"
            fill="#323232"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            Obračunski period:
          </text>
          <text
            id="slp-period"
            transform="matrix(1 0 0 1 200.5 107.959)"
            fill="#323232"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            {format(new Date(selectedBill.startDate), "dd.MM.yyyy") +
              " - " +
              format(new Date(selectedBill.endDate), "dd.MM.yyyy")}
          </text>
        </g>
        <g id="packet">
          <rect
            x="320.915"
            y="98.125"
            fill="none"
            stroke="#323232"
            strokeMiterlimit="10"
            width="94.334"
            height="15.333"
          />

          <text
            transform="matrix(1 0 0 1 320.749 92.292)"
            fill="#323232"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            {" "}
            Paket:
          </text>
          <text
            id="slp-packet"
            transform="matrix(1 0 0 1 355 107.959)"
            fill="#323232"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            {selectedBill.userContract.packetCombination.name}
          </text>
        </g>
        <g id="acc=of-recipient">
          <rect
            x="320.915"
            y="145.645"
            fill="none"
            stroke="#323232"
            strokeMiterlimit="10"
            width="94.334"
            height="15.336"
          />

          <text
            transform="matrix(1 0 0 1 320.749 140.2695)"
            fill="#323232"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            {" "}
            Račun primaoca:
          </text>
          <text
            id="slp-acc-of-recipient"
            transform="matrix(1 0 0 1 336.9463 155.4785)"
            fill="#323232"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            {selectedBill.accOfRecipient}
          </text>
          <text
            id="slp-service-name"
            transform="matrix(1 0 0 1 32.2295 205.625)"
            fill="#323232"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            {selectedBill.userContract.packetCombination.name}
          </text>
          <text
            id="slp-base"
            transform="matrix(1 0 0 1 104.3853 205.292)"
            fill="#323232"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            {selectedBill.price}
          </text>
          <text
            id="slp-pdv"
            transform="matrix(1 0 0 1 191.583 205.292)"
            fill="#323232"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            {vat}
          </text>
          <text
            id="slp-total"
            transform="matrix(1 0 0 1 281.583 205.292)"
            fill="#323232"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            {(parseFloat(selectedBill.price) + parseFloat(vat)).toFixed(2) +
              " " +
              selectedBill.currency}
          </text>
        </g>
        <g id="ref-number">
          <rect
            x="193.099"
            y="146.614"
            fill="none"
            stroke="#323232"
            strokeMiterlimit="10"
            width="100.667"
            height="15.335"
          />

          <text
            transform="matrix(1 0 0 1 192.335 139.3008)"
            fill="#323232"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            {" "}
            Poziv na broj:
          </text>
          <text
            id="slp-ref-num"
            transform="matrix(1 0 0 1 202.4639 156.4482)"
            fill="#323232"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            {selectedBill.callNum}
          </text>
          <line
            fill="none"
            x1="431.498"
            y1="-8042.375"
            x2="431.498"
            y2="8340.625"
          />

          <line
            fill="none"
            x1="-7828.417"
            y1="160.625"
            x2="8554.583"
            y2="160.625"
          />
        </g>
        <g id="iznos">
          <rect
            x="20.591"
            y="145.313"
            fill="none"
            stroke="#323232"
            strokeMiterlimit="10"
            width="53.624"
            height="15.335"
          />

          <text
            transform="matrix(1 0 0 1 20.833 139.0615)"
            fill="#323232"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            {" "}
            Iznos:
          </text>
          <text
            id="slp-price"
            transform="matrix(1 0 0 1 27.6323 155.457)"
            fill="#323232"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            {selectedBill.price + " " + selectedBill.currency}
          </text>
        </g>
        <g id="purpose">
          <rect
            x="94.094"
            y="145.313"
            fill="none"
            stroke="#323232"
            strokeMiterlimit="10"
            width="77.489"
            height="15.335"
          />

          <text
            transform="matrix(1 0 0 1 94.2461 137.5811)"
            fill="#323232"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            {" "}
            Svrha:
          </text>
          <text
            transform="matrix(1 0 0 1 104.3853 155.457)"
            fill="#323232"
            fontFamily="'MyriadPro-Regular'"
            fontSize="8"
          >
            Uplata po računu
          </text>
        </g>
        <g id="header">
          <rect
            x="17.321"
            y="183.625"
            fill="#00A651"
            stroke="#323232"
            strokeMiterlimit="10"
            width="430.512"
            height="11.5"
          />

          <text transform="matrix(1 0 0 1 29.833 192.042)">
            <tspan
              x="0"
              y="0"
              fill="#F1F2F2"
              fontFamily="'MyriadPro-Regular'"
              fontSize="8"
            >
              Naziv usluge
            </tspan>
            <tspan
              x="42.311"
              y="0"
              fill="#F1F2F2"
              fontFamily="'MyriadPro-Regular'"
              fontSize="8"
              letterSpacing="27"
            >
              {" "}
            </tspan>
            <tspan
              x="71.999"
              y="0"
              fill="#F1F2F2"
              fontFamily="'MyriadPro-Regular'"
              fontSize="8"
            >
              Osnovica
            </tspan>
            <tspan
              x="102.671"
              y="0"
              fill="#F1F2F2"
              fontFamily="'MyriadPro-Regular'"
              fontSize="8"
              letterSpacing="3"
            >
              {" "}
            </tspan>
            <tspan
              x="108"
              y="0"
              fill="#F1F2F2"
              fontFamily="'MyriadPro-Regular'"
              fontSize="8"
              letterSpacing="34"
            >
              {" "}
            </tspan>
            <tspan
              x="144"
              y="0"
              fill="#F1F2F2"
              fontFamily="'MyriadPro-Regular'"
              fontSize="8"
            >
              Iznos PDV-a (20%)
            </tspan>
            <tspan
              x="204.175"
              y="0"
              fill="#F1F2F2"
              fontFamily="'MyriadPro-Regular'"
              fontSize="8"
              letterSpacing="10"
            >
              {" "}
            </tspan>
            <tspan
              x="216"
              y="0"
              fill="#F1F2F2"
              fontFamily="'MyriadPro-Regular'"
              fontSize="8"
              letterSpacing="34"
            >
              {" "}
            </tspan>
            <tspan
              x="252"
              y="0"
              fill="#F1F2F2"
              fontFamily="'MyriadPro-Regular'"
              fontSize="8"
            >
              Ukupno
            </tspan>
          </text>
        </g>
      </svg>
    );
  }
}

const mapStateToProps = state => {
  return {
    modalsVisibility: state.modalsVisibility,
    bills: state.bills
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({ setBillModalVisibility }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(Bill);
