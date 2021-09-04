import "./App.css";
import {
	BrowserRouter as Router,
	Route,
	Switch,
	Link,
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
					<Route exact path="/">
						{token ? (
							<Redirect to="/UserPosition" />
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
