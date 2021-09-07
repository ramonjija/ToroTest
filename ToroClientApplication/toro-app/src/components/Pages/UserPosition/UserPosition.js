import React, { useEffect, useState } from "react";
import { getUserName } from "../../../Utils/useToken";
import MenuAccount from "./MenuAccount";
import Position from "./Position";
import { Box } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import { getUserPositions } from "../../../Services/UserPositionServices";
import useToken from "../../../Utils/useToken";
import { formatCents } from "../../../Utils/index";

const useStyles = makeStyles(() => ({
	userPositionStyle: {
		display: "grid",
		padding: "20px",
		backgroundColor: "#f6f8fb",
		gridTemplateColumns: "200px 200px 200px 200px 200px",
		justifyContent: "center",
		height: "100vh",
	},
}));
export default function UserPosition() {
	const classes = useStyles();
	const { token } = useToken();

	const userName = getUserName();
	const [userPositions, setPositions] = useState(null);

	useEffect(() => {
		async function getPosition() {
			var positonAttempt = await getUserPositions(token);
			const { userPositionId } = positonAttempt;
			if (userPositionId) {
				setPositions(positonAttempt);
			}
		}
		getPosition();
	}, []);

	return (
		<div>
			{userPositions ? (
				<MenuAccount
					userName={userName}
					checkingAccountAmount={formatCents(
						userPositions.checkingAccountAmount
					)}
					consolidated={formatCents(userPositions.consolidated)}
				/>
			) : (
				<MenuAccount
					userName={userName}
					checkingAccountAmount={0}
					consolidated={0}
				/>
			)}
			<Box className={classes.userPositionStyle}>
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
