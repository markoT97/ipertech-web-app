import React from "react";
import { Table as BootstrapTable } from "react-bootstrap";
import { Button } from "react-bootstrap";
import { Image } from "react-bootstrap";
import * as Icon from "react-bootstrap-icons";
import { BACKEND_URL } from "../shared/constants";

const KEYS_DELIMITER = ".";

/**
 * Table component
 *
 * @param {string} [title] - Title above the table
 * @param {string} [imagePoster]  - Path to the image above the table
 * @param {Array<Object>} columns - Columns to show with options (columnKey must match key from data), eg. {name: {title: "Display title", higlighted: true, uppercase: true, image: true}, {...}, {...}}
 * @param {Array<Object>} data - Data rows to display, eg. [{name: "Example name", "description: "Example description", ...}, {...}]
 * @param {JSX.Element} children - Nested component which will be shown in column which has 'nestedComponent' property
 * @param {boolean} hover - Change color of table row on hover
 * @param {boolean} striped - Every second row will have dark shade
 * @param {Object} crud - Show button for CRUD operations (create, delete)
 * @param {string} keysColumn - Name of the column which has ids
 *
 * @return Table component
 */
const Table = ({
  title,
  imagePoster,
  columns,
  data,
  hover,
  striped,
  children,
  crud,
  keysColumn,
}) => {
  const getColumnValue = (item, columnKeys) => {
    const splittedColumnKeys = columnKeys.split(KEYS_DELIMITER);

    if (splittedColumnKeys.length === 1) {
      return item[columnKeys];
    }

    let nestedColumnValue = undefined;
    splittedColumnKeys.forEach((key) => {
      if (!nestedColumnValue) {
        nestedColumnValue = item[key] ? item[key] : "-";
      } else {
        nestedColumnValue = nestedColumnValue[key]
          ? nestedColumnValue[key]
          : "-";
      }
    });
    return nestedColumnValue;
  };

  const renderTableRow = (item, index) => {
    return (
      <tr key={item[keysColumn]}>
        {Object.keys(columns).map((columnKeys) => {
          const {
            highlighted,
            uppercase,
            image,
            icon,
            nestedComponent,
            total,
            format,
          } = columns[columnKeys];
          return (
            <React.Fragment key={columnKeys}>
              <td
                className={`align-middle ${highlighted && "text-danger"} ${
                  uppercase && "text-uppercase"
                }`}
              >
                {nestedComponent ? (
                  children
                ) : image ? (
                  <Image
                    src={BACKEND_URL + getColumnValue(item, columnKeys)}
                    alt={item[columnKeys]}
                    className="img-parent"
                    fluid
                  />
                ) : icon ? (
                  (getColumnValue(item, columnKeys) && (
                    <Icon.CheckBox className="text-success" />
                  )) || <Icon.XCircle className="text-danger" />
                ) : total ? (
                  total[index]
                ) : format ? (
                  format(getColumnValue(item, columnKeys))
                ) : (
                  getColumnValue(item, columnKeys)
                )}
              </td>
            </React.Fragment>
          );
        })}
        {crud && crud?.delete && (
          <td>
            <Button
              variant="outline-danger"
              onClick={() => crud.delete.action(item)}
            >
              {crud.delete.content}
            </Button>
          </td>
        )}
      </tr>
    );
  };

  return (
    <div className="m-2">
      {title && <h5 className="text-danger text-uppercase">{title}</h5>}

      {imagePoster && <Image src={imagePoster} className="page-cover" />}

      <BootstrapTable
        bordered
        hover={hover ? true : false}
        striped={striped ? true : false}
        responsive
        className="text-center"
      >
        <thead className="text-uppercase">
          <tr>
            {Object.keys(columns).map((columnName) => {
              return <th key={columnName}>{columns[columnName].title}</th>;
            })}
          </tr>
        </thead>
        <tbody>
          {data &&
            data.map((item, index) => {
              if (item.length) {
                return item.map((nestedItem) => {
                  return renderTableRow(nestedItem, index);
                });
              } else {
                return renderTableRow(item, index);
              }
            })}
          {crud && crud?.create && (
            <tr>
              <td colSpan="4">
                <Button
                  variant="success"
                  className="text-center"
                  onClick={crud.create.action}
                >
                  {crud.create.content}
                </Button>
              </td>
            </tr>
          )}
        </tbody>
      </BootstrapTable>
    </div>
  );
};

export default Table;
