import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import { Box, Avatar } from "@material-ui/core";
import AccountCircleIcon from "@material-ui/icons/AccountCircle";

const useStyles = makeStyles((theme) => ({
	root: {
		flexGrow: 1,
	},
	title: {
		marginRight: theme.spacing(1),
	},
	subgroup: {
		marginRight: theme.spacing(1),
	},
	icon: {
		backgroundColor: "transparent !important",
	},
}));

export default function UserAccount({
	userName,
	checkingAccountAmount,
	consolidated,
}) {
	const classes = useStyles();

	return (
		<div className={classes.root}>
			<AppBar position="static">
				<Toolbar>
					<Box
						display="flex"
						flexDirection="row"
						m={1}
						alignItems="center"
						width="100%"
						justifyContent="flex-start">
						<Box p={1}>
							<Typography variant="h6">Saldo</Typography>
						</Box>
						<Box p={1}>
							<Typography variant="subtitle1" className={classes.subgroup}>
								R$ {checkingAccountAmount}
							</Typography>
						</Box>
						<Box p={1}>
							<Typography variant="h6">Saldo Consolidado</Typography>
						</Box>
						<Box p={1}>
							<Typography variant="subtitle1" className={classes.subgroup}>
								R$ {consolidated}
							</Typography>
						</Box>
					</Box>

					<Box display="flex" flexDirection="row" alignItems="center" p={1}>
						<Avatar className={classes.icon}>
							<AccountCircleIcon />
						</Avatar>
						<Typography variant="h6" className={classes.title}>
							{userName}
						</Typography>
					</Box>
				</Toolbar>
			</AppBar>
		</div>
	);
}
