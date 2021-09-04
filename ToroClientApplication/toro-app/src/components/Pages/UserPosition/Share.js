import React from "react";
import { Typography } from "@material-ui/core";

export default function Share({ symbol, currentPrice }) {
	return (
		<>
			<Typography component="p" variant="h6">
				{symbol}
			</Typography>
			<Typography variant="subtitle2" component="p">
				Preço: R$ {currentPrice}
			</Typography>
		</>
	);
}
