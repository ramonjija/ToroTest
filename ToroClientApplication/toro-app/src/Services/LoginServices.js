export async function loginUser(credentials) {
	debugger;
	return fetch("http://localhost:5100/api/user/login", {
		method: "POST",
		headers: {
			"Content-Type": "application/json",
		},
		body: JSON.stringify(credentials),
	}).then((data) => data.json());
}

export async function CreateUser(credentials) {
	debugger;
	return fetch("http://localhost:5100/api/user", {
		method: "POST",
		headers: {
			"Content-Type": "application/json",
		},
		body: JSON.stringify(credentials),
	}).then((data) => data.json());
}
