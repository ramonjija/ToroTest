import React, { useState } from "react";
import { makeStyles, Typography, TextField, Button } from "@material-ui/core";
import { Modal, Fade, Snackbar, InputAdornment } from "@material-ui/core";
import MuiAlert from "@material-ui/lab/Alert";
import FormControl from "@material-ui/core/FormControl";
import { addBalance } from "../../../Services/UserPositionServices";
import useToken from "../../../Utils/useToken";
import { validateCurrency } from "../../../Utils";

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
	const [balance, setBalance] = useState(null);
	const [validationMessage, setValidationMessage] = useState(null);
	const [openValidation, setOpenValidation] = useState(false);
	const [severity, setSeverity] = useState();
	const [buttonDisabled, setButtonDisabled] = useState(true);

	const handleClose = (event, reason) => {
		if (reason === "clickaway") {
			return;
		}
		setOpenValidation(false);
	};

	const handleAddBalance = async (event) => {
		try {
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

	const handleButtonDisplay = (input) => {
		if (input.length > 0) {
			setButtonDisabled(false);
		} else {
			setButtonDisabled(true);
		}
	};

	const handleBalance = (event) => {
		const balanceInput = event.target.value;
		if (validateCurrency(balanceInput)) {
			setBalance(balanceInput);
			handleButtonDisplay(balanceInput);
		}
	};

	return (
		<Modal open={openModal} onClose={closeModal} className={classes.modal}>
			<Fade in={openModal}>
				<div className={classes.paper}>
					<Typography gutterBottom variant="h4">
						Add to Balance
					</Typography>
					<FormControl className={classes.formControl}>
						<TextField
							id="qtd-share"
							type="text"
							InputProps={{
								startAdornment: (
									<InputAdornment position="start">R$</InputAdornment>
								),
								maxLength: 18,
								minLength: 1,
							}}
							value={balance}
							onChange={handleBalance}
						/>
						<Button
							id="btn-buy-share"
							variant="contained"
							disabled={buttonDisabled}
							className={classes.button}
							onClick={handleAddBalance}>
							Add
						</Button>
					</FormControl>
					{validationMessage && (
						<Snackbar
							open={openValidation}
							autoHideDuration={2000}
							onClose={handleClose}
							anchorOrigin={{
								vertical: "top",
								horizontal: "center",
							}}>
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
