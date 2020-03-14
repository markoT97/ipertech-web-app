import React, { Component } from "react";
import { format } from "date-fns";
import { bindActionCreators } from "redux";
import {
  Jumbotron,
  Button,
  Image,
  Table,
  Row,
  Col,
  Form,
  Spinner,
  Toast
} from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { BACKEND_URL } from "../../redux/actions/backendServerSettings";
import "./../App.scss";
import { connect } from "react-redux";
import { fetchUserById } from "./../../redux/actions/userActions/actionCreators";
import { fetchLatestPoll } from "./../../redux/actions/pollActions/actionCreators";

export class UserProfile extends Component {
  componentDidMount() {
    this.props.fetchUserById(this.props.auth.user.userId);
    this.props.fetchLatestPoll();
  }
  render() {
    const { user, poll } = this.props;
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
                    {user.userContract.packetCombination.internetPacket.name}
                  </td>
                  <td className="align-middle">
                    <Button variant="outline-primary mb-2">
                      <Icon.Pencil size={25} />
                    </Button>
                  </td>
                </tr>
                <tr>
                  <td className="align-middle">
                    <Icon.Tv size={25} />
                    &nbsp; Televizija
                  </td>
                  <td className="text-danger align-middle">
                    {user.userContract.packetCombination.tvPacket
                      ? user.userContract.packetCombination.tvPacket.name
                      : ""}
                  </td>
                  <td className="align-middle">
                    <Button variant="outline-primary mb-2">
                      <Icon.Pencil size={25} />
                    </Button>
                    {user.userContract.packetCombination.tvPacket ? (
                      <Button variant="outline-danger">
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
                    {user.userContract.packetCombination.phonePacket
                      ? user.userContract.packetCombination.phonePacket.name
                      : ""}
                  </td>
                  <td className="align-middle">
                    <Button variant="outline-primary mb-2">
                      <Icon.Pencil size={25} />
                    </Button>
                    {user.userContract.packetCombination.phonePacket ? (
                      <Button variant="outline-danger">
                        <Icon.XCircle size={25} />
                      </Button>
                    ) : (
                      ""
                    )}
                  </td>
                </tr>
              </tbody>
            </Table>
          </Col>
          <Col lg={3}>
            <Table striped responsive className="text-center">
              <thead>
                <tr>
                  <th className="bg-danger text-white">
                    <Icon.DocumentText size={35} />
                    Računi
                  </th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td>Neplaćeni računi</td>
                </tr>
                {user.bills.map((b, i) => {
                  return (
                    <tr>
                      <td>
                        {format(new Date(b.startDate), "dd.MM.yyyy") +
                          " - " +
                          format(new Date(b.endDate), "dd.MM.yyyy")}
                      </td>
                    </tr>
                  );
                })}

                <tr>
                  <td className="bg-white">
                    <Button variant="outline-danger" block>
                      Pregledaj sve račune
                    </Button>
                  </td>
                </tr>
              </tbody>
            </Table>
          </Col>
          <Col lg={3}>
            <Table striped responsive className="text-center">
              <thead>
                <tr>
                  <th className="bg-dark text-white">
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
                    <Button variant="outline-dark" block>
                      Sačuvaj
                    </Button>
                  </td>
                </tr>
              </tbody>
            </Table>
          </Col>
        </Row>

        <Row>
          <Col lg={5} className="mt-2 mb-2">
            <h5 className="text-warning text-uppercase text-center">
              <Icon.Chat size={40} />
              &nbsp; Poruke
            </h5>
            <Toast>
              <Toast.Header>
                <img
                  src="holder.js/20x20?text=%20"
                  className="rounded mr-2"
                  alt=""
                />
                <strong className="mr-auto">Ime korisnika</strong>
                <small>11 mins ago</small>
              </Toast.Header>
              <Toast.Body>Tekst poruke</Toast.Body>
            </Toast>
            <Toast>
              <Toast.Header>
                <img
                  src="holder.js/20x20?text=%20"
                  className="rounded mr-2"
                  alt=""
                />
                <strong className="mr-auto">Ime korisnika</strong>
                <small>11 mins ago</small>
              </Toast.Header>
              <Toast.Body>Tekst poruke</Toast.Body>
            </Toast>
            <Toast>
              <Toast.Header>
                <img
                  src="holder.js/20x20?text=%20"
                  className="rounded mr-2"
                  alt=""
                />
                <strong className="mr-auto">Ime korisnika</strong>
                <small>11 mins ago</small>
              </Toast.Header>
              <Toast.Body>Tekst poruke</Toast.Body>
            </Toast>
          </Col>
          <Col className="text-center mt-2">
            <h5 className="text-danger text-uppercase">
              <Icon.QuestionFill size={40} />
              &nbsp; Anketa
            </h5>
            <Table>
              <thead>
                <tr>
                  <th>{poll.question}</th>
                </tr>
              </thead>
              <tbody>
                {poll.options.map((o, i) => {
                  return (
                    <tr>
                      <td>
                        <Form.Check
                          type="radio"
                          label={o.answerText}
                          name="formHorizontalRadios"
                        />
                      </td>
                    </tr>
                  );
                })}

                <tr>
                  <td>
                    <Button variant="danger" block>
                      Glasaj
                    </Button>
                  </td>
                  <td></td>
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
    user: state.user,
    poll: state.poll
  };
};

const mapDispatchToProps = dispatch => {
  return bindActionCreators({ fetchUserById, fetchLatestPoll }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(UserProfile);
