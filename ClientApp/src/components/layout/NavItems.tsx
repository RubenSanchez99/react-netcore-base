import React from "react";
import UserContext from "../../areas/users/UserContext";
import { ListItem, ListItemText, List, makeStyles } from "@material-ui/core";
import { Link } from "react-router-dom";
import ExpandLess from "@material-ui/icons/ExpandLess";
import ExpandMore from "@material-ui/icons/ExpandMore";
import Collapse from "@material-ui/core/Collapse";

interface INavItemsProps {}

const useStyles = makeStyles(theme => ({
  root: {
    width: "100%",
    maxWidth: 360,
    backgroundColor: theme.palette.background.paper
  },
  nested: {
    paddingLeft: theme.spacing(4)
  }
}));

interface INavSubmenuProps {
  header: string;
  routes: { title: string; link: string }[];
}

const NavSubmenu: React.FC<INavSubmenuProps> = ({ header, routes }) => {
  const classes = useStyles();
  const [open, setOpen] = React.useState(true);

  function handleClick() {
    setOpen(!open);
  }

  return (
    <div>
      <ListItem button onClick={handleClick}>
        <ListItemText primary={header} />
        {open ? <ExpandLess /> : <ExpandMore />}
      </ListItem>
      <Collapse in={open} timeout="auto" unmountOnExit>
        <List component="div" disablePadding>
          {routes.map(route => (
            <ListItem
              button
              className={classes.nested}
              key={route.link}
              component={Link}
              to={route.link}
            >
              <ListItemText primary={route.title} />
            </ListItem>
          ))}
        </List>
      </Collapse>
    </div>
  );
};

const NavItems: React.FC<INavItemsProps> = props => {
  const user = React.useContext(UserContext);
  return (
    <List>
      {user.access.map(access => (
        <NavSubmenu
          header={access.header}
          routes={access.routes}
          key={access.header}
        />
      ))}
    </List>
  );
};

export default NavItems;
