import React from "react";

export default function UserCreation() {
	return (
		<div>
			<h1>Novo Usuário</h1>
			<form>
				<label>
					<p>Nome Usuario</p>
					<input type="text" />
				</label>
				<label>
					<p>CPF</p>
					<input type="number" />
				</label>
				<label>
					<p>Password</p>
					<input type="password" />
				</label>
			</form>
		</div>
	);
}
