import React, { useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import {
	Box,
	Avatar,
	Button,
	FormControl,
	InputLabel,
	TextField,
} from "@material-ui/core";
import AccountCircleIcon from "@material-ui/icons/AccountCircle";
import ModalShares from "./ModalShares";
import ModalBalance from "./ModalBalance";

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
	userName: {
		display: "flex",
		flexDirection: "row",
		alignItems: "center",
		width: "200px",
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
	balanceBtn: {
		display: "flex",
		flexDirection: "row",
		alignItems: "center",
		width: "200px",
	},
}));

export default function UserAccount({
	userName,
	checkingAccountAmount,
	consolidated,
}) {
	const classes = useStyles();
	const [openModal, setOpenModal] = useState(false);
	const [openModalBalance, setOpenModalBalance] = useState(false);
	const [balance, setBalance] = useState(false);

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
	return (
		<div className={classes.root}>
			<AppBar position="static">
				<Toolbar>
					<Box m={1} className={classes.userAccount}>
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
						<Box p={1}>
							<Button variant="contained" onClick={handleOpenModal}>
								Comprar Ações
							</Button>
							<ModalShares
								openModal={openModal}
								closeModal={handleCloseModal}
							/>
						</Box>
						<Box p={1}>
							<Button variant="contained" onClick={handleOpenBalanceModal}>
								Adicionar Saldo
							</Button>
							<ModalBalance
								openModal={openModalBalance}
								closeModal={handleCloseBalanceModal}
							/>
						</Box>
					</Box>

					<Box p={1} class={classes.userName}>
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
