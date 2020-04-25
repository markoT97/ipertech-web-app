import React, { Component } from "react";
import { Image, Table } from "react-bootstrap";
import { BACKEND_URL } from "../../shared/constants";
import "./../App.scss";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import fetchTvPackets from "../../redux/actions/tvPacketsActions/actionCreators";
import * as Icon from "react-bootstrap-icons";

export class Tv extends Component {
  componentDidMount() {
    this.props.fetchTvPackets();
  }
  render() {
    const { tvPackets } = this.props;
    return (
      <div className="m-2">
        <h5 className="text-danger text-uppercase">Tv paketi</h5>

        <Image
          src={BACKEND_URL + "/packets/tv/cover-photo.jpg"}
          className="page-cover"
        />

        <Table bordered responsive className="text-center text-uppercase">
          <thead>
            <tr>
              <th>Naziv</th>
              <th>Kanali</th>
              <th>Cena</th>
            </tr>
          </thead>
          <tbody>
            {tvPackets.map((tp, i) => {
              return (
                <tr key={i}>
                  <td className="align-middle text-danger">{tp.name}</td>
                  <td className="align-middle">
                    <Table hover>
                      <thead>
                        <tr>
                          <th>Pozicija</th>
                          <th>Logo</th>
                          <th>Naziv kanala</th>
                          <th>Tv unazad</th>
                        </tr>
                      </thead>
                      <tbody>
                        {tp.tvChannels.map((tc, i) => {
                          return (
                            <tr key={i}>
                              <td className="align-middle">
                                {tc.positionNumber}
                              </td>
                              <td>
                                <Image
                                  src={BACKEND_URL + tc.imageLocation}
                                  alt={tc.name}
                                  className="img-parent"
                                  fluid
                                />
                              </td>
                              <td className="align-middle">{tc.name}</td>
                              <td className="align-middle">
                                {(tc.tvBackwards && (
                                  <Icon.CheckBox className="text-success" />
                                )) || <Icon.XCircle className="text-danger" />}
                              </td>
                            </tr>
                          );
                        })}
                      </tbody>
                    </Table>
                  </td>
                  <td className="align-middle text-danger">{tp.price}</td>
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
    tvPackets: state.tvPackets,
  };
};

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({ fetchTvPackets }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(Tv);
