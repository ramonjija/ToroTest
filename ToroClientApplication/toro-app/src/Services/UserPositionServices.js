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

export async function buyShare(token, order) {
	return fetch("http://localhost:5100/api/order", {
		method: "POST",
		headers: {
			"Content-Type": "application/json",
			Authorization: `Bearer ${token}`,
		},
		body: JSON.stringify(order),
	}).then((data) => {
		return data.json();
	});
}

export async function addBalance(token, balance) {
	return fetch("http://localhost:5100/api/order/balance", {
		method: "POST",
		headers: {
			"Content-Type": "application/json",
			Authorization: `Bearer ${token}`,
		},
		body: JSON.stringify(balance),
	}).then((data) => {
		return data.json();
	});
}
