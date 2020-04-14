import React, { Component } from "react";
import { Button, Table, Dropdown, DropdownButton } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import {
  fetchUserByContractId,
  fetchPacketCombinationByInternetAndTvAndPhonePacketId
} from "./../../redux/actions/userActions/actionCreators";
import fetchInternetPackets from "./../../redux/actions/internetPacketsActions/actionCreators";
import fetchTvPackets from "./../../redux/actions/tvPacketsActions/actionCreators";
import fetchPhonePackets from "./../../redux/actions/phonePacketsActions/actionCreators";

export class UserData extends Component {
  state = {
    isChangeInternetPacketActive: false,
    isChangeTvPacketActive: false,
    isChangePhonePacketActive: false,

    selectedInternetPacket: { internetPacketId: null, name: "" },
    selectedTvPacket: { tvPacketId: null, name: "" },
    selectedPhonePacket: { phonePacketId: null, name: "" }
  };
  render() {
    const { user, internetPackets, tvPackets, phonePackets } = this.props;
    const {
      internetPacket,
      tvPacket,
      phonePacket
    } = user.userContract.packetCombination;
    return (
      <Table striped responsive className="text-center">
        <thead>
          <tr>
            <th colSpan={4} className="bg-success text-white">
              <Icon.PersonFill size={35} />
              Podaci
            </th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td colSpan={2} className="align-middle">
              ID ugovora:
            </td>
            <td colSpan={2}>{user.userContract.userContractId}</td>
          </tr>
          <tr>
            <td colSpan={2}>Ime i prezime:</td>
            <td colSpan={2}>{user.firstName + " " + user.lastName}</td>
          </tr>
          <tr>
            <td colSpan={2}>Imejl adresa:</td>
            <td colSpan={2}>{user.email}</td>
          </tr>
          <tr>
            <td rowSpan={3} className="text-center align-middle">
              <Icon.Folder size={70} />
            </td>
            <td className="align-middle">
              <Icon.At size={25} />
              &nbsp; Internet
            </td>
            <td className="text-danger align-middle">
              {this.state.isChangeInternetPacketActive ? (
                <DropdownButton
                  id="dropdown-tvPackets-button"
                  size="sm"
                  title={this.state.selectedInternetPacket.name}
                  variant="outline-secondary"
                >
                  {internetPackets.map((ip, i) => {
                    return (
                      <Dropdown.Item
                        onClick={() =>
                          this.setState({ selectedInternetPacket: ip })
                        }
                        key={i}
                        active={internetPacket.name === ip.name}
                      >
                        {ip.name}
                      </Dropdown.Item>
                    );
                  })}
                </DropdownButton>
              ) : (
                internetPacket.name
              )}
            </td>
            <td className="align-middle">
              {this.state.isChangeInternetPacketActive ? (
                <Button
                  onClick={() => {
                    this.state.selectedInternetPacket.internetPacketId !==
                      (internetPacket
                        ? internetPacket.internetPacketId
                        : null) &&
                      this.props.fetchPacketCombinationByInternetAndTvAndPhonePacketId(
                        {
                          internetPacketId: this.state.selectedInternetPacket
                            .internetPacketId,
                          tvPacketId: tvPacket ? tvPacket.tvPacketId : null,
                          phonePacketId: phonePacket
                            ? phonePacket.phonePacketId
                            : null
                        },
                        user.userContract
                      );
                    this.setState({
                      internetPacketId: internetPacket,
                      isChangeInternetPacketActive: false,
                      selectedInternetPacket: {
                        internetPacketId: null,
                        name: ""
                      }
                    });
                  }}
                  variant="outline-warning mb-2"
                >
                  <Icon.BoxArrowDown size={25} />
                </Button>
              ) : (
                <Button
                  onClick={() => {
                    this.props.fetchInternetPackets();
                    this.setState({
                      isChangeInternetPacketActive: true,
                      selectedInternetPacket: internetPacket
                    });
                  }}
                  variant="outline-primary mb-2"
                >
                  <Icon.Pencil size={25} />
                </Button>
              )}
            </td>
          </tr>
          <tr>
            <td className="align-middle">
              <Icon.Tv size={25} />
              &nbsp; Televizija
            </td>
            <td className="text-danger align-middle">
              {this.state.isChangeTvPacketActive ? (
                <DropdownButton
                  id="dropdown-tvPackets-button"
                  size="sm"
                  title={this.state.selectedTvPacket.name}
                  variant="outline-secondary"
                >
                  {tvPackets.map((tv, i) => {
                    return (
                      <Dropdown.Item
                        onClick={() => this.setState({ selectedTvPacket: tv })}
                        key={i}
                        active={tvPacket ? tvPacket.name === tv.name : false}
                      >
                        {tv.name}
                      </Dropdown.Item>
                    );
                  })}
                </DropdownButton>
              ) : tvPacket ? (
                tvPacket.name
              ) : (
                ""
              )}
            </td>
            <td className="align-middle">
              {this.state.isChangeTvPacketActive ? (
                <Button
                  onClick={() => {
                    this.state.selectedTvPacket.tvPacketId !==
                      (tvPacket ? tvPacket.tvPacketId : null) &&
                      this.props.fetchPacketCombinationByInternetAndTvAndPhonePacketId(
                        {
                          internetPacketId: internetPacket.internetPacketId,
                          tvPacketId: this.state.selectedTvPacket.tvPacketId,
                          phonePacketId: phonePacket
                            ? phonePacket.phonePacketId
                            : null
                        },
                        user.userContract
                      );
                    this.setState({
                      tvPacketId: tvPacket ? tvPacket.tvPacketId : null,
                      isChangeTvPacketActive: false
                    });
                  }}
                  variant="outline-warning mb-2"
                >
                  <Icon.BoxArrowDown size={25} />
                </Button>
              ) : (
                <Button
                  onClick={() => {
                    this.props.fetchTvPackets();
                    this.setState({
                      isChangeTvPacketActive: true,
                      selectedTvPacket: tvPacket
                        ? tvPacket
                        : { tvPacketId: null, name: "" }
                    });
                  }}
                  variant="outline-primary mb-2 mr-1"
                >
                  <Icon.Pencil size={25} />
                </Button>
              )}

              {tvPacket ? (
                <Button
                  onClick={() => {
                    this.props.fetchPacketCombinationByInternetAndTvAndPhonePacketId(
                      {
                        internetPacketId: internetPacket.internetPacketId,
                        tvPacketId: null,
                        phonePacketId: phonePacket
                          ? phonePacket.phonePacketId
                          : null
                      },
                      user.userContract
                    );

                    this.setState({
                      tvPacketId: tvPacket ? tvPacket.tvPacketId : "",
                      isChangeTvPacketActive: false,
                      selectedTvPacket: { tvPacketId: null, name: "" }
                    });
                  }}
                  variant="outline-danger mb-2"
                >
                  <Icon.XCircle size={25} />
                </Button>
              ) : (
                ""
              )}
            </td>
          </tr>
          <tr>
            <td className="align-middle">
              <Icon.Phone size={25} />
              &nbsp; Telefonija
            </td>
            <td className="text-danger align-middle">
              {this.state.isChangePhonePacketActive ? (
                <DropdownButton
                  id="dropdown-tvPackets-button"
                  size="sm"
                  title={this.state.selectedPhonePacket.name}
                  variant="outline-secondary"
                >
                  {phonePackets.map((pp, i) => {
                    return (
                      <Dropdown.Item
                        onClick={() =>
                          this.setState({ selectedPhonePacket: pp })
                        }
                        key={i}
                        active={
                          phonePacket ? phonePacket.name === pp.name : false
                        }
                      >
                        {pp.name}
                      </Dropdown.Item>
                    );
                  })}
                </DropdownButton>
              ) : phonePacket ? (
                phonePacket.name
              ) : (
                ""
              )}
            </td>
            <td className="align-middle">
              {this.state.isChangePhonePacketActive ? (
                <Button
                  onClick={() => {
                    this.state.selectedPhonePacket.phonePacketId !==
                      (phonePacket ? phonePacket.phonePacketId : null) &&
                      this.props.fetchPacketCombinationByInternetAndTvAndPhonePacketId(
                        {
                          internetPacketId: internetPacket.internetPacketId,
                          tvPacketId: tvPacket ? tvPacket.tvPacketId : null,
                          phonePacketId: this.state.selectedPhonePacket
                            .phonePacketId
                        },
                        user.userContract
                      );
                    this.setState({
                      phonePacketId: phonePacket,
                      isChangePhonePacketActive: false,
                      selectedPhonePacket: { phonePacketId: null, name: "" }
                    });
                  }}
                  variant="outline-warning mb-2"
                >
                  <Icon.BoxArrowDown size={25} />
                </Button>
              ) : (
                <Button
                  onClick={() => {
                    this.props.fetchPhonePackets();
                    this.setState({
                      isChangePhonePacketActive: true,
                      selectedPhonePacket: phonePacket
                        ? phonePacket
                        : { phonePacketId: null, name: "" }
                    });
                  }}
                  variant="outline-primary mb-2 mr-1"
                >
                  <Icon.Pencil size={25} />
                </Button>
              )}
              {phonePacket ? (
                <Button variant="outline-danger mb-2">
                  <Icon.XCircle size={25} />
                </Button>
              ) : (
                ""
              )}
            </td>
          </tr>
        </tbody>
      </Table>
    );
  }
}

const mapStateToProps = state => {
  return {
    auth: state.auth,
    user: state.user,
    internetPackets: state.internetPackets,
    tvPackets: state.tvPackets,
    phonePackets: state.phonePackets
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators(
    {
      fetchUserByContractId,
      fetchInternetPackets,
      fetchTvPackets,
      fetchPhonePackets,
      fetchPacketCombinationByInternetAndTvAndPhonePacketId
    },
    dispatch
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(UserData);
