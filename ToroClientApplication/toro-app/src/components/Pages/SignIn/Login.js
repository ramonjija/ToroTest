import React, { useState } from "react";
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
import Link from "@material-ui/core/Link";
import { makeStyles } from "@material-ui/core/styles";
import Snackbar from "@material-ui/core/Snackbar";
import MuiAlert from "@material-ui/lab/Alert";
import PropTypes from "prop-types";
import { loginUser } from "../../../Services/LoginServices";

const useStyles = makeStyles((theme) => ({
	paper: {
		marginTop: theme.spacing(8),
		display: "flex",
		flexDirection: "column",
		alignItems: "center",
	},
	avatar: {
		margin: theme.spacing(1),
		backgroundColor: theme.palette.secondary.main,
	},
	form: {
		width: "100%", // Fix IE 11 issue.
		marginTop: theme.spacing(1),
	},
	submit: {
		margin: theme.spacing(3, 0, 2),
	},
}));

export default function Login({ setToken }) {
	const classes = useStyles();

	const [cpf, setCpf] = useState();
	const [password, setPassword] = useState();
	const [validationMessage, setValidationMessage] = useState();

	const [open, setOpen] = React.useState(false);

	const handleClose = (event, reason) => {
		if (reason === "clickaway") {
			return;
		}

		setOpen(false);
	};

	const handleLogin = async () => {
		debugger;
		var loginAttempt = await loginUser({ cpf, password });
		const { token } = loginAttempt;
		if (token) setToken(token);
		else {
			console.log(loginAttempt);
			setValidationMessage(loginAttempt[0]);
			setOpen(true);
		}
	};

	return (
		<Container component="main" maxWidth="xs">
			<CssBaseline />
			<div className={classes.paper}>
				<Avatar className={classes.avatar}>
					<LockOutlinedIcon />
				</Avatar>
				<Typography component="h1" variant="h5">
					Sign In
				</Typography>
				<form
					className={classes.form}
					noValidate
					onSubmit={(e) => {
						e.preventDefault();
					}}>
					<TextField
						variant="outlined"
						margin="normal"
						required="true"
						fullWidth
						id="cpf"
						label="CPF"
						name="cpf"
						autoComplete="number"
						autoFocus
						onChange={(e) => setCpf(e.target.value)}
					/>
					<TextField
						variant="outlined"
						margin="normal"
						required="true"
						fullWidth
						name="password"
						label="Password"
						type="password"
						id="password"
						autoComplete="current-password"
						onChange={(e) => setPassword(e.target.value)}
					/>
					<FormControlLabel
						control={<Checkbox value="remember" color="primary" />}
						label="Remember me"
					/>
					<Button
						type="submit"
						fullWidth
						variant="contained"
						color="primary"
						className={classes.submit}
						onClick={handleLogin}>
						Sign In
					</Button>
					<Grid container>
						<Grid item>
							<Link href="./Pages/SignUp/" variant="body2">
								{"Não possui uma conta? Cadastre-se aqui"}
							</Link>
						</Grid>
					</Grid>
				</form>
				{validationMessage && (
					<Snackbar open={open} autoHideDuration={6000} onClose={handleClose}>
						<MuiAlert severity="error" elevation={6} variant="filled">
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
