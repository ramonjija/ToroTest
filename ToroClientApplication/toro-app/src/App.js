import "./App.css";
import {
	BrowserRouter as Router,
	Route,
	Switch,
	Redirect,
} from "react-router-dom";
import Login from "./components/Pages/SignIn/Login";
import UserPosition from "./components/Pages/UserPosition/UserPosition";
import UserCreation from "./components/Pages/SignUp/UserCreation";
import useToken from "./Utils/useToken";

function App() {
	const { token, setToken } = useToken();

	return (
		<div className="wrapper">
			<Router basename="/">
				<Switch>
					<Route exact path="/">
						{token ? (
							<Redirect to="/userposition" />
						) : (
							<Login setToken={setToken} />
						)}
					</Route>
					<Route path="/SignIn">
						<Login setToken={setToken} />
					</Route>
					<Route path="/SignUp">
						<UserCreation />
					</Route>
					{token && (
						<Route path="/userposition">
							<UserPosition />
						</Route>
					)}
				</Switch>
			</Router>
		</div>
	);
}

export default App;
