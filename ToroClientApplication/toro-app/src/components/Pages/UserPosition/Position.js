import React from "react";
import { Card, CardContent, Box, Typography } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import Share from "./Share";

const useStyles = makeStyles((theme) => ({
	paper: {
		width: "200px",
	},
}));

export default function Position({ symbol, currentPrice, amount }) {
	const classes = useStyles();
	return (
		<Card variant="outlined" className={classes.paper}>
			<Box display="flex" justifyContent="center">
				<CardContent>
					<Share symbol={symbol} currentPrice={currentPrice} />
					<Typography variant="subtitle2" component="p">
						Quantidade: {amount}
					</Typography>
				</CardContent>
			</Box>
		</Card>
	);
}
