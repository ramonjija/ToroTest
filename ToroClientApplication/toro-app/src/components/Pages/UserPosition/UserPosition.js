import React from "react";
import { Typography } from "@material-ui/core";
import Container from "@material-ui/core/Container";
import useToken from "../../../Utils/useToken";
import UserAccount from "./UserAccount";
import Position from "./Position";

export default function UserPosition() {
	const userName = "Ramon";
	var positionTest = {
		symbol: "PETR4",
		amount: 1,
		currentPrice: 10.0,
	};

	const accountTest = {
		checkingAccountAmount: 100.0,
		consolidated: 110,
		positions: [],
	};
	return (
		<div>
			<h1>User Position</h1>
			<UserAccount
				userName={userName}
				checkingAccountAmount={accountTest.checkingAccountAmount}
				consolidated={accountTest.consolidated}
			/>
			<Position
				symbol={positionTest.symbol}
				amount={positionTest.amount}
				currentPrice={positionTest.currentPrice}
			/>
		</div>
	);
}
