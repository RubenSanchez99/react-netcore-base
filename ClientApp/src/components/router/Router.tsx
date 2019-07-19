import React from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import AuthorizeRoute from "../api-authorization/AuthorizeRoute";
import { ApplicationPaths } from "../api-authorization/ApiAuthorizationConstants";
import ApiAuthorizationRoutes from "../api-authorization/ApiAuthorizationRoutes";
import Home from "../../areas/users/Home";

const Routes = () => (
  <Switch>
    <AuthorizeRoute path="/home" component={Home} />
    <Route
      path={ApplicationPaths.ApiAuthorizationPrefix}
      component={ApiAuthorizationRoutes}
    />
    <Redirect from="/" exact to="/home" />
  </Switch>
);

export default Routes;
