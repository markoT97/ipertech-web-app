import React, { Component } from "react";
import { bindActionCreators } from "redux";
import {
  Jumbotron,
  Button,
  Image,
  Table,
  Row,
  Col,
  Form,
  Spinner
} from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { BACKEND_URL } from "../../redux/actions/backendServerSettings";
import "./../App.scss";
import { connect } from "react-redux";
import { fetchUserById } from "./../../redux/actions/userActions/actionCreators";

export class UserProfile extends Component {
  componentDidMount() {
    this.props.fetchUserById(this.props.auth.user.userId);
  }
  render() {
    const { user } = this.props;
    console.log(user);
    return (
      <div className="m-2">
        <h5 className="text-danger text-uppercase">Moj nalog</h5>

        <Jumbotron
          fluid
          style={{
            backgroundImage: `url(${BACKEND_URL}account/cover-photo.jpg)`
          }}
        >
          <p className="text-center">
            {user.imageLocation ? (
              <Image
                className="border border-light"
                style={{ height: "10em" }}
                src={BACKEND_URL + user.imageLocation}
                roundedCircle
              />
            ) : (
              <Spinner
                variant="light"
                as="span"
                animation="border"
                style={{ width: "10em", height: "10em" }}
              />
            )}
          </p>
          <p className="text-center">
            <Button variant="light" size="sm">
              <Icon.Camera size={25} />
              &nbsp; Promeni sliku
            </Button>
          </p>
        </Jumbotron>

        <Row>
          <Col lg={6}>
            <Table striped responsive className="text-center">
              <thead>
                <tr>
                  <th colSpan={4}>
                    <Icon.PersonFill size={35} />
                    Podaci
                  </th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td colSpan={2}>ID ugovora:</td>
                  <td colSpan={2}>{user.userContract.userContractId}</td>
                </tr>
                <tr>
                  <td colSpan={2}>Ime i prezime:</td>
                  <td colSpan={2}>{user.firstName + " " + user.lastName}</td>
                </tr>
                <tr>
                  <td rowSpan={3} className="text-center align-middle">
                    <Icon.Folder size={70} />
                  </td>
                  <td>
                    <Icon.At size={25} />
                    &nbsp; Internet
                  </td>
                  <td>
                    {user.userContract.packetCombination.internetPacket.name}
                  </td>
                  <td>
                    <Icon.Pencil size={20} />
                    <Icon.XCircleFill size={20} />
                  </td>
                </tr>
                <tr>
                  <td>
                    <Icon.Tv size={25} />
                    &nbsp; Televizija
                  </td>
                  <td>
                    {user.userContract.packetCombination.tvPacket
                      ? user.userContract.packetCombination.tvPacket.name
                      : "-"}
                  </td>
                  <td>
                    <Icon.Pencil size={20} />
                    <Icon.XCircleFill size={20} />
                  </td>
                </tr>
                <tr>
                  <td>
                    <Icon.Phone size={25} />
                    &nbsp; Telefonija
                  </td>
                  <td>
                    {user.userContract.packetCombination.phonePacket
                      ? user.userContract.packetCombination.phonePacket.name
                      : "-"}
                  </td>
                  <td>
                    <Icon.Pencil size={20} />
                    <Icon.XCircleFill size={20} />
                  </td>
                </tr>
                <tr>
                  <td colSpan={2}>Imejl adresa:</td>
                  <td colSpan={2}>{user.email}</td>
                </tr>
              </tbody>
            </Table>
          </Col>
          <Col lg={3}>
            <Table striped responsive className="text-center">
              <thead>
                <tr>
                  <th>
                    <Icon.DocumentText size={35} />
                    Računi
                  </th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td>Neplaćeni računi</td>
                </tr>
                <tr>
                  <td>17.02.2018. - 17.03.2018.</td>
                </tr>
                <tr>
                  <td>Pregledaj sve račune</td>
                </tr>
              </tbody>
            </Table>
          </Col>
          <Col lg={3}>
            <Table striped responsive className="text-center">
              <thead>
                <tr>
                  <th>
                    <Icon.LockFill size={35} />
                    Promena lozinke
                  </th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td>
                    <Form>
                      <Form.Group controlId="formBasicResetPasswordOld">
                        <Form.Control
                          type="password"
                          placeholder="Stara lozinka"
                        />
                      </Form.Group>
                      <Form.Group controlId="formBasicResetPasswordNew">
                        <Form.Control
                          type="password"
                          placeholder="Nova lozinka"
                        />
                      </Form.Group>
                    </Form>
                  </td>
                </tr>
                <tr>
                  <td>
                    <Button variant="dark">Sačuvaj</Button>
                  </td>
                </tr>
              </tbody>
            </Table>
          </Col>
        </Row>
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    auth: state.auth,
    user: state.user
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({ fetchUserById }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(UserProfile);
