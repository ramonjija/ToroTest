export async function getUserPositions(token) {
	return fetch("http://localhost:5100/api/userPosition/login", {
		method: "GET",
		headers: {
			"Content-Type": "application/json",
			Bearer: token,
		},
	}).then((data) => data.json());
}
