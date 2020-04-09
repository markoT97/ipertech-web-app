import * as yup from "yup";

yup.setLocale({
  mixed: {
    required: "Popunite polje da biste nastavili"
  },
  string: {
    email: "Upišite validnu imejl adresu",
    min: ({ min }) => `Vrednost je prekratka (minimum ${min} karaktera)`,
    max: ({ max }) => `Vredunost je predugačka (maksimum ${max} karaktera)`
  },
  number: {
    min: ({ min }) =>
      `Vrednost nije validna (mora biti manja ili jednaka ${min})`,
    max: ({ max }) =>
      `Vrednost nije validna (mora biti veća ili jednaka ${max})`
  }
});

export const loginValidationSchema = yup.object({
  email: yup
    .string()
    .email()
    .required(),
  password: yup.string().required()
});

export const registerValidationSchema = yup.object({
  userContractId: yup.string().required(),
  firstName: yup.string().required(),
  lastName: yup.string().required(),
  gender: yup.string().required(),
  email: yup
    .string()
    .email()
    .required(),
  phoneNumber: yup.string().required(),
  password: yup
    .string()
    .min(8)
    .required(),
  passwordConfirm: yup
    .string()
    .min(8)
    .required()
});

export const insertMessageValidationSchema = yup.object({
  title: yup
    .string()
    .min(10)
    .max(15)
    .required(),
  content: yup
    .string()
    .min(30)
    .max(100)
    .required()
});
