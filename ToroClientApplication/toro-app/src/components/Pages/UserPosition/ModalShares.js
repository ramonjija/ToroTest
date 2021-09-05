import React, { useState, useEffect } from "react";
import {
	makeStyles,
	MenuItem,
	Typography,
	TextField,
	Button,
} from "@material-ui/core";
import { Modal, Select, Fade, Snackbar } from "@material-ui/core";
import MuiAlert from "@material-ui/lab/Alert";
import FormControl from "@material-ui/core/FormControl";
import InputLabel from "@material-ui/core/InputLabel";
import { getShares, buyShare } from "../../../Services/UserPositionServices";
import useToken from "../../../Utils/useToken";
import { useHistory } from "react-router";

const useStyles = makeStyles((theme) => ({
	formControl: {
		display: "flex",
		flexDirection: "row",
		margin: theme.spacing(1),
		minWidth: 500,
		justifyContent: "space-around",
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
	inputPapel: {
		minWidth: "150px",
		paddingLeft: "2%",
	},
	totalAmount: {
		padding: theme.spacing(2),
	},
}));

export default function ModalShares({ openModal, closeModal }) {
	const classes = useStyles();
	const history = useHistory();
	const { token, setToken } = useToken();
	const [paper, setPaper] = useState();
	const [amount, setAmount] = useState(0);
	const [shares, setShares] = useState();
	const [validationMessage, setValidationMessage] = useState(null);
	const [openValidation, setOpenValidation] = useState(false);
	const [severity, setSeverity] = useState();

	useEffect(() => {
		async function getAllShares() {
			try {
				var shares = await getShares(token);
				setShares(shares);
			} catch (error) {
				console.log(error.message);
			}
		}
		getAllShares();
	}, []);

	const handleClose = (event, reason) => {
		if (reason === "clickaway") {
			return;
		}
		setOpenValidation(false);
	};

	const handleChange = (event) => {
		setPaper(event.target.value);
	};

	const handleBuyShare = async (event) => {
		try {
			var result = await buyShare(token, {
				ShareSymbol: paper,
				Amount: amount,
			});
			const { userPositionId } = result;

			if (userPositionId) {
				setValidationMessage("Share bought successfully!");
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

	const handleAmount = (event) => {
		setAmount(event.target.value);
	};

	const calculateTotalMount = () => {
		if (shares !== 0) {
			return shares.map((share, i) => {
				if (share.symbol === paper) {
					return share.currentPrice * amount;
				}
				return null;
			});
		}
		return 0;
	};

	return (
		<Modal
			open={openModal}
			onClose={closeModal}
			aria-labelledby="simple-modal-title"
			aria-describedby="simple-modal-description"
			className={classes.modal}>
			<Fade in={openModal}>
				<div className={classes.paper}>
					<Typography gutterBottom variant="h4">
						Comprar Ações
					</Typography>
					<FormControl className={classes.formControl}>
						<InputLabel id="select-papel-label" className={classes.inputPapel}>
							Papel
						</InputLabel>
						<Select
							className={classes.inputPapel}
							labelId="select-papel-label"
							id="select-papel-label"
							value={paper}
							onChange={handleChange}>
							{shares &&
								shares.map((share, i) => {
									return (
										<MenuItem value={share.symbol}>{share.symbol}</MenuItem>
									);
								})}
						</Select>
						<TextField
							id="qtd-share"
							label="Quantidade"
							type="number"
							InputLabelProps={{
								shrink: true,
							}}
							value={amount}
							onChange={handleAmount}
						/>
						<Button
							id="btn-buy-share"
							variant="contained"
							color="primary"
							onClick={handleBuyShare}>
							Comprar
						</Button>
					</FormControl>
					<Typography variant="subtitle1" className={classes.totalAmount}>
						Valor Total: R$
						{shares && calculateTotalMount()}
					</Typography>
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
