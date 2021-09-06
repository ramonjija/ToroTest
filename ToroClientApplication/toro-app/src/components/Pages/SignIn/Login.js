import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import Container from "@material-ui/core/Container";
import CssBaseline from "@material-ui/core/CssBaseline";
import Avatar from "@material-ui/core/Avatar";
import LockOutlinedIcon from "@material-ui/icons/LockOutlined";
import Typography from "@material-ui/core/Typography";
import TextField from "@material-ui/core/TextField";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Checkbox from "@material-ui/core/Checkbox";
import Button from "@material-ui/core/Button";
import Grid from "@material-ui/core/Grid";
import { makeStyles } from "@material-ui/core/styles";
import Snackbar from "@material-ui/core/Snackbar";
import MuiAlert from "@material-ui/lab/Alert";
import PropTypes from "prop-types";
import { loginUser } from "../../../Services/LoginServices";
import { cpfMask } from "../../../Utils/index";

const useStyles = makeStyles((theme) => ({
	avatar: {
		margin: theme.spacing(1),
		backgroundColor: "#6561a4",
	},
	container: {
		width: "400px",
		boxShadow: "0 1px 3px rgb(0 0 0 / 12%), 0 1px 2px rgb(0 0 0 / 24%)",
		backgroundColor: "white",
		paddingBottom: "30px",
	},
	form: {
		width: "100%",
		marginTop: theme.spacing(1),
	},
	submit: {
		backgroundColor: "#6131b4",
		color: "#fff",
		margin: theme.spacing(3, 0, 2),
		padding: "10px",
	},
	paper: {
		marginTop: theme.spacing(8),
		marginBotton: theme.spacing(8),
		display: "flex",
		flexDirection: "column",
		alignItems: "center",
		width: "100%",
	},
}));

export default function Login({ setToken }) {
	const classes = useStyles();
	const history = useHistory();
	const [cpf, setCpf] = useState("");
	const [password, setPassword] = useState();
	const [validationMessage, setValidationMessage] = useState();
	const [open, setOpen] = React.useState(false);
	const [severity, setSeverity] = React.useState();

	const handleClose = (evt, reason) => {
		if (reason === "clickaway") {
			return;
		}
		setOpen(false);
	};

	const handleLogin = async () => {
		try {
			var loginAttempt = await loginUser({
				cpf: cpf.replace(/\D/g, ""),
				password,
			});
			const { token } = loginAttempt;

			if (token) {
				setValidationMessage("Login successfully!");
				setSeverity("success");
				setOpen(true);
				setTimeout(() => {
					setToken(token);
					history.push("/userposition");
				}, 1000);
			} else {
				setValidationMessage(loginAttempt[0]);
				setSeverity("error");
				setOpen(true);
			}
		} catch (error) {
			setValidationMessage(error.message);
			setSeverity("error");
			setOpen(true);
		}
	};

	const validateCPF = (value) => {
		setCpf(cpfMask(value));
	};

	return (
		<Container component="main" className={classes.container}>
			<div className={classes.paper}>
				<Avatar className={classes.avatar}>
					<LockOutlinedIcon />
				</Avatar>
				<Typography component="h1" variant="h5">
					Sign In Toro
				</Typography>
				<form
					className={classes.form}
					noValidate
					onSubmit={(e) => {
						e.preventDefault();
					}}>
					<TextField
						margin="small"
						required={true}
						fullWidth
						id="cpf"
						label="CPF"
						type="text"
						name="cpf"
						autoComplete="number"
						autoFocus
						value={cpf}
						inputProps={{ maxLength: 14, minLength: 11 }}
						onChange={(e) => validateCPF(e.target.value)}
					/>
					<TextField
						margin="normal"
						required={true}
						fullWidth
						name="password"
						label="Password"
						type="password"
						id="password"
						autoComplete="current-password"
						onChange={(e) => setPassword(e.target.value)}
					/>

					<Button
						type="submit"
						fullWidth
						variant="contained"
						className={classes.submit}
						onClick={handleLogin}>
						Sign In
					</Button>
					<Grid container>
						<Grid item>
							<a href="/signup">{"Don't have an account? Register here"}</a>
						</Grid>
					</Grid>
				</form>
				{validationMessage && (
					<Snackbar
						open={open}
						autoHideDuration={6000}
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
		</Container>
	);
}

Login.propTypes = {
	setToken: PropTypes.func.isRequired,
};
