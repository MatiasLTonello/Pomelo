import Create from "./components/Create";
import Home from "./components/Home";
const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/create',
    element: <Create />
  },

];

export default AppRoutes;
