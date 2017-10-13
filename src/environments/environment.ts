// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  production: false,
  firebaseConfig: {
    apiKey: "AIzaSyCDrxpmQHxwx0suUQq5iJFa1QpieaMyaHw",
    authDomain: "code-study.firebaseapp.com",
    databaseURL: "https://code-study.firebaseio.com",
    projectId: "code-study",
    storageBucket: "code-study.appspot.com",
    messagingSenderId: "885033443870"
  }
};
