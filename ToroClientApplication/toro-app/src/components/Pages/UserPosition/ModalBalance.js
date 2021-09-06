import React, { useState } from "react";
import { makeStyles, Typography, TextField, Button } from "@material-ui/core";
import { Modal, Fade, Snackbar, InputAdornment } from "@material-ui/core";
import MuiAlert from "@material-ui/lab/Alert";
import FormControl from "@material-ui/core/FormControl";
import { addBalance } from "../../../Services/UserPositionServices";
import useToken from "../../../Utils/useToken";
import { numberValidation } from "../../../Utils";

import { useHistory } from "react-router";

const useStyles = makeStyles((theme) => ({
	formControl: {
		display: "flex",
		flexDirection: "row",
		margin: theme.spacing(1),
		minWidth: 350,
		justifyContent: "space-between",
	},
	selectEmpty: {
		marginTop: theme.spacing(2),
	},
	modal: {
		display: "flex",
		alignItems: "center",
		justifyContent: "center",
	},
	paper: {
		backgroundColor: theme.palette.background.paper,
		border: "2px none #000",
		boxShadow: theme.shadows[5],
		padding: theme.spacing(2, 4, 3),
	},
	button: {
		backgroundColor: "#6131b4",
		color: "#fff",
	},
	inputPapel: {
		minWidth: "150px",
		paddingLeft: "2%",
	},
	totalAmount: {
		padding: theme.spacing(2),
	},
}));

export default function ModalBalance({ openModal, closeModal }) {
	const classes = useStyles();
	const history = useHistory();
	const { token } = useToken();
	const [balance, setBalance] = useState();
	const [validationMessage, setValidationMessage] = useState(null);
	const [openValidation, setOpenValidation] = useState(false);
	const [severity, setSeverity] = useState();

	const handleClose = (event, reason) => {
		if (reason === "clickaway") {
			return;
		}
		setOpenValidation(false);
	};

	const handleAddBalance = async (event) => {
		try {
			const formattedBalance = numberValidation(balance);

			const result = await addBalance(token, {
				Balance: balance,
			});
			const { userPositionId } = result;

			if (userPositionId) {
				setValidationMessage("Balance added successfully!");
				setSeverity("success");
				setTimeout(() => {
					closeModal();
					history.go(0);
				}, 1000);
			} else {
				setValidationMessage(result[0]);
				setSeverity("error");
			}
			setOpenValidation(true);
		} catch (error) {
			setValidationMessage(error.message);
			setSeverity("error");
			setOpenValidation(true);
		}
	};

	const handleBalance = (event) => {
		const balanceInput = event.target.value;
		const pattern = /^[0-9^&*)]*[.]{0,1}[0-9^&*)]{0,2}$/;

		if (pattern.test(balanceInput)) {
			setBalance(balanceInput);
		}
	};

	return (
		<Modal open={openModal} onClose={closeModal} className={classes.modal}>
			<Fade in={openModal}>
				<div className={classes.paper}>
					<Typography gutterBottom variant="h4">
						Add Balance
					</Typography>
					<FormControl className={classes.formControl}>
						<TextField
							id="qtd-share"
							type="text"
							InputProps={{
								startAdornment: (
									<InputAdornment position="start">R$</InputAdornment>
								),
							}}
							value={balance}
							onChange={handleBalance}
						/>
						<Button
							id="btn-buy-share"
							variant="contained"
							className={classes.button}
							onClick={handleAddBalance}>
							Add
						</Button>
					</FormControl>
					{validationMessage && (
						<Snackbar
							open={openValidation}
							autoHideDuration={2000}
							onClose={handleClose}>
							<MuiAlert severity={severity} elevation={6} variant="filled">
								{validationMessage}
							</MuiAlert>
						</Snackbar>
					)}
				</div>
			</Fade>
		</Modal>
	);
}
