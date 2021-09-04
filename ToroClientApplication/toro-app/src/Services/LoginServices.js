export async function loginUser(credentials) {
	return fetch("http://localhost:5100/api/user/login", {
		method: "POST",
		headers: {
			"Content-Type": "application/json",
		},
		body: JSON.stringify(credentials),
	}).then((data) => data.json());
}

export async function createUser(credentials) {
	return fetch("http://localhost:5100/api/user", {
		method: "POST",
		headers: {
			"Content-Type": "application/json",
		},
		body: JSON.stringify(credentials),
	}).then((data) => data.json());
}
