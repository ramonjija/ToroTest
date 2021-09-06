import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import { Typography } from "@material-ui/core";

const useStyles = makeStyles(() => ({
	title: {
		marginBottom: "10px",
	},
}));

export default function Share({ symbol, currentPrice, amount }) {
	const classes = useStyles();

	return (
		<>
			<Typography component="p" variant="h5" className={classes.title}>
				{symbol}
			</Typography>
			<Typography variant="subtitle2" component="p">
				Price: R$ {currentPrice}
			</Typography>
			<Typography variant="subtitle3" component="p">
				Amount: {amount}
			</Typography>
		</>
	);
}
