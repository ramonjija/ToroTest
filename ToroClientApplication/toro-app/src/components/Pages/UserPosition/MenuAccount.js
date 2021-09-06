import React, { useState } from "react";
import { useHistory } from "react-router";
import { makeStyles } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import { Box, Avatar, Button } from "@material-ui/core";
import AccountCircleIcon from "@material-ui/icons/AccountCircle";
import ExitToApp from "@material-ui/icons/ExitToApp";
import ModalShares from "./ModalShares";
import ModalBalance from "./ModalBalance";
import useToken from "../../../Utils/useToken";

const useStyles = makeStyles((theme) => ({
	root: {
		flexGrow: 1,
	},
	app: {
		backgroundColor: "#6131b4",
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
	login: {
		display: "flex",
		flexDirection: "row",
		alignItems: "center",
		width: "20%",
	},
	userAccount: {
		display: "flex",
		flexDirection: "row",
		alignItems: "center",
		width: "100%",
		justifyContent: "flex-start",
	},
	balanceText: {
		display: "flex",
		flexDirection: "row",
		alignItems: "center",
		width: "100px",
	},
	iconLogout: {
		backgroundColor: "transparent !important",
		color: "#fff",
	},
	button: {
		fontSize: "10px",
		backgroundColor: "#fff",
		color: "#6131b4",
	},
	buttonLogout: {
		backgroundColor: "transparent !important",
	},
}));

export default function MenuAccount({
	userName,
	checkingAccountAmount,
	consolidated,
}) {
	const { logOut } = useToken();

	const classes = useStyles();
	const history = useHistory();
	const [openModal, setOpenModal] = useState(false);
	const [openModalBalance, setOpenModalBalance] = useState(false);

	const handleOpenModal = () => {
		setOpenModal(!openModal);
	};
	const handleCloseModal = () => {
		setOpenModal(false);
	};

	const handleOpenBalanceModal = () => {
		setOpenModalBalance(!openModalBalance);
	};

	const handleCloseBalanceModal = () => {
		setOpenModalBalance(false);
	};

	const handleLogOut = () => {
		logOut();
		history.push("/signIn");
	};

	return (
		<div className={classes.root}>
			<AppBar position="static" className={classes.app}>
				<Toolbar>
					<Box m={1} className={classes.userAccount}>
						<Box p={1}>
							<Typography variant="h6">Balance</Typography>
						</Box>
						<Box p={1}>
							<Typography variant="subtitle2" className={classes.subgroup}>
								R$ {checkingAccountAmount}
							</Typography>
						</Box>
						<Box p={1}>
							<Typography variant="h6">Consolidated Balance</Typography>
						</Box>
						<Box p={1}>
							<Typography variant="subtitle2" className={classes.subgroup}>
								R$ {consolidated}
							</Typography>
						</Box>
						<Box p={1}>
							<Button
								variant="contained"
								className={classes.button}
								onClick={handleOpenModal}>
								Buy shares
							</Button>
							<ModalShares
								openModal={openModal}
								closeModal={handleCloseModal}
							/>
						</Box>
						<Box p={1}>
							<Button
								variant="contained"
								className={classes.button}
								onClick={handleOpenBalanceModal}>
								Add Balance
							</Button>
							<ModalBalance
								openModal={openModalBalance}
								closeModal={handleCloseBalanceModal}
							/>
						</Box>
					</Box>

					<Box p={2} class={classes.login}>
						<Avatar className={classes.icon}>
							<AccountCircleIcon />
						</Avatar>
						<Typography variant="h8" className={classes.title}>
							{userName}
						</Typography>
						<Button className={classes.buttonLogout} onClick={handleLogOut}>
							<Avatar className={classes.iconLogout}>
								<ExitToApp />
							</Avatar>
						</Button>
					</Box>
				</Toolbar>
			</AppBar>
		</div>
	);
}
