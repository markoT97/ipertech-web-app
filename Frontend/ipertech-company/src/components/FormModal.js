import React from "react";
import { Form, Button, Modal, InputGroup, Col } from "react-bootstrap";
import { FilePicker } from "react-file-picker";
import { useFormik } from "formik";

const KEYS_DELIMITER = ".";

/**
 * FormModal component
 *
 * @param {string} headerClassNames
 * @param {string} modalTitle
 * @param {string} formData
 * @param {yup.ObjectsSchema<{}>} validationSchema
 * @param {boolean} show
 * @param {Function} onHide
 * @param {Function} onSubmit
 * @param {string} submitButtonText
 *
 * @return FormModal component
 */
const FormModal = ({
  headerClassNames,
  modalTitle,
  formData,
  validationSchema,
  show,
  onHide,
  onSubmit,
  submitButtonText,
}) => {
  let initialValues = {};

  // Make object with appropriate key value pairs and initialize each one
  formData.forEach((item) => {
    initialValues[item.select ? item.select.name : item.name] = "";
  });

  const formik = useFormik({
    initialValues: Object.assign(initialValues),
    validationSchema: validationSchema,
    onSubmit: (data) => {
      let payloadData = {};
      const columnsKeys = Object.keys(data);

      columnsKeys.forEach((columnKey) => {
        try {
          const parsedData = JSON.parse(data[columnKey]);
          return (payloadData[columnKey] = parsedData);
        } catch (e) {
          return (payloadData[columnKey] = data[columnKey]);
        }
      });
      onSubmit(payloadData);
      onHide();
      resetForm();
    },
  });

  const {
    handleSubmit,
    handleChange,
    setFieldValue,
    resetForm,
    values,
    touched,
    isValid,
    errors,
    dirty,
  } = formik;

  return (
    <Modal
      aria-labelledby="contained-modal-title-vcenter"
      centered
      show={show}
      onHide={onHide}
    >
      <Modal.Header closeButton className={headerClassNames}>
        <Modal.Title id="contained-modal-title-vcenter">
          {modalTitle}
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form onSubmit={handleSubmit}>
          {formData.map((input) => {
            if (input.select) {
              const {
                controlId,
                type,
                label,
                prepend,
                name,
                placeholder,
                data,
                value,
              } = input.select;

              return (
                <Form.Group key={controlId} controlId={controlId}>
                  <Form.Label>{label}</Form.Label>
                  {type === "file" ? (
                    <FilePicker
                      extensions={["jpg", "jpeg", "png"]}
                      dims={{
                        minWidth: 100,
                        maxWidth: 500,
                        minHeight: 100,
                        maxHeight: 500,
                      }}
                      onChange={(fileObject) =>
                        setFieldValue([name], fileObject)
                      }
                      onError={(errMsg) => setFieldValue([name], errMsg)}
                    >
                      <InputGroup className="mb-3">
                        <InputGroup.Prepend>
                          <Button variant="outline-primary" size="sm">
                            {prepend}
                          </Button>
                        </InputGroup.Prepend>
                        <Form.Control
                          placeholder={
                            values[name].name
                              ? values[name].name
                              : errors[name]
                              ? values[name]
                              : placeholder
                          }
                          aria-label={placeholder}
                          aria-describedby="basic-addon2"
                          readOnly
                          onChange={(fileObject) =>
                            setFieldValue([name], fileObject)
                          }
                          onError={(errMsg) => setFieldValue([name], errMsg)}
                          isValid={touched[name] && !errors[name]}
                          isInvalid={!!errors[name]}
                        />
                        {errors[name] && (
                          <Form.Control.Feedback type="invalid">
                            {errors[name]}
                          </Form.Control.Feedback>
                        )}
                      </InputGroup>
                    </FilePicker>
                  ) : (
                    <InputGroup>
                      {prepend && (
                        <InputGroup.Prepend>
                          <InputGroup.Text>{prepend}</InputGroup.Text>
                        </InputGroup.Prepend>
                      )}
                      <Form.Control
                        onChange={handleChange}
                        as="select"
                        name={name}
                        value={values[name]}
                      >
                        <option value="" label={placeholder} />
                        {data?.map((item, i) => {
                          let tempObject = {};

                          Object.keys(value).forEach((keys) => {
                            const splittedKeys = keys.split(KEYS_DELIMITER);

                            if (splittedKeys.length === 1) {
                              tempObject[keys] = item[keys];
                            } else {
                              let savedKey = undefined;
                              splittedKeys.forEach((key, index) => {
                                if (index < splittedKeys.length - 1) {
                                  if (savedKey) {
                                    tempObject[savedKey] = {
                                      ...tempObject[savedKey],
                                      [key]: {},
                                    };
                                  } else {
                                    tempObject[key] = {};
                                  }
                                } else {
                                  tempObject[savedKey] = {
                                    ...tempObject[savedKey],
                                    [key]: item[savedKey][key],
                                  };
                                }
                                savedKey = key;
                              });
                            }
                          });

                          return (
                            <option
                              key={i}
                              value={
                                name !== "contractDurationMonths"
                                  ? JSON.stringify(tempObject)
                                  : tempObject[name]
                              }
                              label={item.name}
                            />
                          );
                        })}
                      </Form.Control>

                      {errors[name] && (
                        <Form.Control.Feedback type="invalid">
                          {errors[name]}
                        </Form.Control.Feedback>
                      )}
                    </InputGroup>
                  )}
                </Form.Group>
              );
            } else {
              const {
                controlId,
                label,
                type,
                name,
                prepend,
                placeholder,
              } = input;
              return (
                <Form.Group
                  key={controlId}
                  controlId={type !== "radio" ? controlId : undefined}
                >
                  <Form.Label>{label}</Form.Label>
                  {prepend ? (
                    <InputGroup>
                      {prepend && (
                        <InputGroup.Prepend>
                          <InputGroup.Text>{prepend}</InputGroup.Text>
                        </InputGroup.Prepend>
                      )}
                      <Form.Control
                        onChange={handleChange}
                        type={type}
                        name={name}
                        placeholder={placeholder}
                        value={values[name]}
                        isValid={touched[name] && !errors[name]}
                        isInvalid={!!errors[name]}
                      />

                      {errors[name] && (
                        <Form.Control.Feedback type="invalid">
                          {errors[name]}
                        </Form.Control.Feedback>
                      )}
                    </InputGroup>
                  ) : (
                    <>
                      <Col>
                        {type === "radio" ? (
                          input.data.map((item, index) => {
                            return (
                              <Form.Check
                                key={`${name}-${index}`}
                                onChange={handleChange}
                                type={type}
                                name={name}
                                label={item}
                                placeholder={placeholder}
                                value={item}
                                isValid={touched[name] && !errors[name]}
                                isInvalid={!!errors[name]}
                                id={`${type}-${item}-${index}`}
                                inline
                              />
                            );
                          })
                        ) : (
                          <Form.Control
                            onChange={handleChange}
                            type={type}
                            name={name}
                            label={label}
                            placeholder={placeholder}
                            value={values[name]}
                            isValid={touched[name] && !errors[name]}
                            isInvalid={!!errors[name]}
                          />
                        )}
                      </Col>

                      {errors[name] && (
                        <Form.Control.Feedback type="invalid">
                          {errors[name]}
                        </Form.Control.Feedback>
                      )}
                    </>
                  )}
                </Form.Group>
              );
            }
          })}

          <Button onClick={onHide} className="float-right ml-2" variant="light">
            Odustani
          </Button>
          <Button
            className="float-right ml-2"
            type="submit"
            variant="success"
            disabled={!(isValid && dirty)}
          >
            {submitButtonText ? submitButtonText : "Potvrdi"}
          </Button>
        </Form>
      </Modal.Body>
    </Modal>
  );
};

export default FormModal;
