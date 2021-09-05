import React, { useState, useEffect } from "react";
import {
	makeStyles,
	MenuItem,
	Typography,
	TextField,
	Button,
} from "@material-ui/core";
import { Modal, Select, Fade } from "@material-ui/core";
import FormControl from "@material-ui/core/FormControl";
import InputLabel from "@material-ui/core/InputLabel";
import { getShares } from "../../../Services/UserPositionServices";
import useToken from "../../../Utils/useToken";

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
}));

export default function ModalShares({ open, onClose }) {
	const classes = useStyles();
	const { token, setToken } = useToken();
	const [paper, setPaper] = useState();
	const [amount, setAmount] = useState();
	const [shares, setShares] = useState();

	useEffect(() => {
		async function getAllShares() {
			var shares = await getShares(token);
			setShares(shares);
		}
		getAllShares();
	}, []);

	const handleChange = (event) => {
		console.log(event.target.value);
		setPaper(event.target.value);
	};

	const handleBuyShare = (event) => {
		console.log({ paper, amount });
	};

	const handleAmount = (event) => {
		setAmount(event.target.value);
	};

	return (
		<Modal
			open={open}
			onClose={onClose}
			aria-labelledby="simple-modal-title"
			aria-describedby="simple-modal-description"
			className={classes.modal}>
			<Fade in={open}>
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
							id="standard-number"
							label="Quantidade"
							type="number"
							InputLabelProps={{
								shrink: true,
							}}
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
				</div>
			</Fade>
		</Modal>
	);
}
