import React from "react";
import { Card, CardContent, Box } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import Share from "./Share";

const useStyles = makeStyles(() => ({
	paper: {
		width: "100%",
		margin: "10px",
		color: "#fff",
		backgroundColor: "#6131b4",
		height: "25%",
	},
	box: {
		display: "flex",
	},
}));

export default function Position({ symbol, currentPrice, amount }) {
	const classes = useStyles();
	return (
		<Card variant="outlined" className={classes.paper}>
			<Box className={classes.box}>
				<CardContent>
					<Share symbol={symbol} currentPrice={currentPrice} amount={amount} />
				</CardContent>
			</Box>
		</Card>
	);
}
