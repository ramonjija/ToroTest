import React from "react";
import "./Login.css";

function Login() {
	return (
		<div className="login">
			<h1>Login</h1>
			<form>
				<label>
					<p>CPF</p>
					<input type="text" />
				</label>
				<label>
					<p>Password</p>
					<input type="password" />
				</label>
				<div>
					<button type="submit">Submit</button>
				</div>
			</form>
		</div>
	);
}

export default Login;
