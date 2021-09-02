import "./App.css";
import { BrowserRouter, Route, Switch } from "react-router-dom";
import Login from "./components/Login";
import UserPosition from "./components/UserPosition/UserPosition";
import Preferences from "./components/Preferences/Preferences";
import { useState } from "react";

function App() {
	const [token, setToken] = useState();

	if (!token) return <Login setToken={setToken} />;

	return (
		<div className="wrapper">
			<h1>Toro Ivestimentos</h1>
			<BrowserRouter>
				<Switch>
					<Route path="/UserPosition">
						<UserPosition />
					</Route>
					<Route path="/preferences">
						<Preferences />
					</Route>
				</Switch>
			</BrowserRouter>
		</div>
	);
}

export default App;
