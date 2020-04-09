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
