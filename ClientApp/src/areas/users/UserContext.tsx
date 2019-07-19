import React from "react";

interface User {
  userName: string;
  access: { header: string; routes: { title: string; link: string }[] }[];
}

export const DefaultUser: User = {
  userName: "",
  access: [
    {
      header: "Test Header",
      routes: [{ title: "First route", link: "/first" }]
    }
  ]
};

const UserContext = React.createContext(DefaultUser);

export default UserContext;
