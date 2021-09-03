import "./App.css";
import { BrowserRouter as Router, Route, Switch, Link } from "react-router-dom";
import Login from "./components/Pages/SignIn/Login";
import UserPosition from "./components/Pages/UserPosition/UserPosition";
import { useState } from "react";
import UserCreation from "./components/Pages/SignUp/UserCreation";

function setToken(userToken) {
	sessionStorage.setItem("token", JSON.stringify(userToken));
}

function getToken() {
	const tokenString = sessionStorage.getItem("token");
	const userToken = JSON.parse(tokenString);
	return userToken;
}

function App() {
	debugger;
	const token = getToken();
	//const [token, setToken] = useState();

	//if (!token) return <Login setToken={setToken} />;

	return (
		<div className="wrapper">
			<h1>Toro Ivestimentos</h1>
			<Router basename="/">
				<nav>
					<ul>
						<li>
							<Link to="/SignIn">Sign In</Link>
						</li>
						<li>
							<Link to="/SignUp">Sign Up</Link>
						</li>
						<li>
							<Link to="/UserPosition">User Position</Link>
						</li>
					</ul>
				</nav>
				<Switch>
					<Route path="/SignIn">
						<Login setToken={setToken} />
					</Route>
					<Route path="/SignUp">
						<UserCreation />
					</Route>
					{token && (
						<Route path="/UserPosition">
							<UserPosition />
						</Route>
					)}
				</Switch>
			</Router>
		</div>
	);
}

export default App;
