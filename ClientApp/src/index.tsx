import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
//import App from "./App";
import * as serviceWorker from "./serviceWorker";
import { BrowserRouter } from "react-router-dom";
import Dashboard from "./components/layout/Dashboard";
import Routes from "./components/router/Router";
import UserContext, { DefaultUser } from "./areas/users/UserContext";

ReactDOM.render(
  <BrowserRouter>
    <UserContext.Provider value={DefaultUser}>
      <Dashboard>
        <Routes />
      </Dashboard>
    </UserContext.Provider>
  </BrowserRouter>,
  document.getElementById("root")
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
