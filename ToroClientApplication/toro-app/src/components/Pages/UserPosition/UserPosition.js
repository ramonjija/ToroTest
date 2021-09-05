import React, { useEffect, useState } from "react";
import { getUserName } from "../../../Utils/useToken";
import UserAccount from "./UserAccount";
import Position from "./Position";
import { Box } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import { getUserPositions } from "../../../Services/UserPositionServices";
import useToken from "../../../Utils/useToken";

const useStyles = makeStyles(() => ({
	userPosition: {
		display: "flex",
		flexDirection: "row",
	},
}));
export default function UserPosition() {
	const classes = useStyles();
	const userName = getUserName();
	const { token, setToken } = useToken();
	const [userPositions, setPositions] = useState();

	useEffect(() => {
		async function getPosition() {
			var positions = await getUserPositions(token);
			setPositions(positions);
		}
		getPosition();
	}, []);

	return (
		<div>
			<h1>User Position</h1>
			{userPositions ? (
				<UserAccount
					userName={userName}
					checkingAccountAmount={userPositions.checkingAccountAmount}
					consolidated={userPositions.consolidated}
				/>
			) : (
				<UserAccount
					userName={userName}
					checkingAccountAmount={0}
					consolidated={0}
				/>
			)}
			<Box className={classes.userPosition}>
				{userPositions &&
					userPositions.positions.map((userPosition, i) => {
						return (
							<Position
								symbol={userPosition.symbol}
								amount={userPosition.amount}
								currentPrice={userPosition.currentPrice}
							/>
						);
					})}
			</Box>
		</div>
	);
}
