import React from "react";
import { Route, Switch } from "react-router-dom";
import {
  Home,
  About,
  InternetPackets,
  TvPackets,
  PhonePackets,
  PacketCombinations,
  AdminPanel,
  UserProfile,
} from "./pages";
import AuthRoute from "./utils/AuthRoute";
import { userRoles } from "./shared/constants";

const Routes = () => {
  return (
    <Switch>
      <Route exact path="/" component={Home}></Route>
      <Route path="/about" component={About}></Route>
      <Route path="/internet" component={InternetPackets}></Route>
      <Route path="/tv" component={TvPackets}></Route>
      <Route path="/phone" component={PhonePackets}></Route>
      <Route path="/packets" component={PacketCombinations}></Route>
      <AuthRoute
        path="/user-profile"
        component={UserProfile}
        allowedRole={userRoles.USER}
      ></AuthRoute>
      <AuthRoute
        path="/admin-panel"
        component={AdminPanel}
        allowedRole={userRoles.ADMIN}
      ></AuthRoute>
    </Switch>
  );
};

export default Routes;
