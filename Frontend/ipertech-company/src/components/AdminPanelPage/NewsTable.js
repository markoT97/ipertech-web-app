import React, { Component } from "react";
import { connect } from "react-redux";
import { Table, Button, Image } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { bindActionCreators } from "redux";
import {
  fetchNews,
  deleteNews,
} from "../../redux/actions/newsActions/actionCreators";
import { BACKEND_URL } from "../../shared/constants";
import { format } from "date-fns";
import { setInsertNewsModalVisibility } from "./../../redux/actions/modalsActions/actionCreators";

export class NewsTable extends Component {
  componentDidMount() {
    this.props.fetchNews();
  }
  render() {
    const { news } = this.props;
    return (
      <Table striped size="sm" className="text-center " responsive>
        <thead>
          <tr className="text-uppercase">
            <th>Naslov</th>
            <th>Sadržaj</th>
            <th>Kreirano</th>
            <th>Slika</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {news.map((n, i) => {
            return (
              <tr key={i}>
                <td className="align-middle">{n.title}</td>
                <td className="align-middle">{n.content}</td>
                <td className="align-middle">
                  {format(new Date(n.createdAt), "dd.MM.yyyy")}
                </td>
                <td className="align-middle">
                  <Image src={BACKEND_URL + "/" + n.imageLocation} fluid />
                </td>

                <td className="align-middle">
                  <Button
                    variant="outline-danger"
                    onClick={() => this.props.deleteNews(n)}
                  >
                    <Icon.Trash size={20} />
                  </Button>
                </td>
              </tr>
            );
          })}
          <tr>
            <td colSpan="4">
              <Button
                variant="success"
                className="text-center"
                onClick={() => this.props.setInsertNewsModalVisibility(true)}
              >
                <Icon.Plus size={20} />
              </Button>
            </td>
          </tr>
        </tbody>
      </Table>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    news: state.news,
  };
};

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators(
    {
      fetchNews,
      setInsertNewsModalVisibility,
      deleteNews,
    },
    dispatch
  );
};

export default connect(mapStateToProps, mapDispatchToProps)(NewsTable);
