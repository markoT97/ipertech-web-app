import React, { Component } from "react";
import { Image, Table } from "react-bootstrap";
import { BACKEND_URL } from "../../shared/constants";
import "./../App.scss";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import fetchInternetPackets from "../../redux/actions/internetPacketsActions/actionCreators";

export class Internet extends Component {
  componentDidMount() {
    this.props.fetchInternetPackets();
  }
  render() {
    const { internetPackets } = this.props;
    return (
      <div className="m-2">
        <h5 className="text-danger text-uppercase">Internet paketi</h5>

        <Image
          src={BACKEND_URL + "/packets/internet/cover-photo.jpg"}
          className="page-cover"
        />

        <Table bordered hover responsive className="text-center">
          <thead className="text-uppercase">
            <tr>
              <th>Naziv</th>
              <th>Brzina</th>
              <th>Ruter</th>
              <th>Cena</th>
            </tr>
          </thead>
          <tbody>
            {internetPackets.map((ip, i) => {
              return (
                <tr key={i}>
                  <td className="align-middle text-danger text-uppercase">
                    {ip.name}
                  </td>
                  <td className="align-middle">{ip.speed}</td>
                  <td className="align-middle">
                    <Image
                      src={BACKEND_URL + ip.internetRouter.imageLocation}
                      alt={ip.internetRouter.name}
                      className="img-parent"
                      fluid
                    />
                  </td>
                  <td className="align-middle text-danger">{ip.price}</td>
                </tr>
              );
            })}
          </tbody>
        </Table>
      </div>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    internetPackets: state.internetPackets,
  };
};

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({ fetchInternetPackets }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(Internet);
