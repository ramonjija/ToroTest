export async function getUserPositions(token) {
	return fetch("http://localhost:5100/api/UserPosition", {
		method: "GET",
		headers: {
			"Content-Type": "application/json",
			Authorization: `Bearer ${token}`,
		},
	}).then((data) => {
		return data.json();
	});
}

export async function getShares(token) {
	return fetch("http://localhost:5100/api/share", {
		method: "GET",
		headers: {
			"Content-Type": "application/json",
			Authorization: `Bearer ${token}`,
		},
	}).then((data) => {
		return data.json();
	});
}
