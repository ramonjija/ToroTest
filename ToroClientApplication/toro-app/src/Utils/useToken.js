import { useState } from "react";
import jwt from "jwt-decode";

const getToken = () => {
	const tokenString = sessionStorage.getItem("token");
	const userToken = JSON.parse(tokenString);
	return userToken;
};

export default function useToken() {
	const [token, setToken] = useState(getToken());

	const saveToken = (userToken) => {
		sessionStorage.setItem("token", JSON.stringify(userToken));
		setToken(userToken);
	};

	return {
		setToken: saveToken,
		token,
	};
}

export function getUserName() {
	const { unique_name } = jwt(getToken());
	return unique_name;
}
