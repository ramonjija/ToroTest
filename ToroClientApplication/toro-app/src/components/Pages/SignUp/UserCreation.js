import React, { useState } from "react";
import { useHistory } from "react-router";
import Container from "@material-ui/core/Container";
import CssBaseline from "@material-ui/core/CssBaseline";
import Avatar from "@material-ui/core/Avatar";
import LockOutlinedIcon from "@material-ui/icons/LockOutlined";
import Grid from "@material-ui/core/Grid";
import Typography from "@material-ui/core/Typography";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import { makeStyles } from "@material-ui/core/styles";
import Snackbar from "@material-ui/core/Snackbar";
import MuiAlert from "@material-ui/lab/Alert";
import { createUser } from "../../../Services/LoginServices";
import { cpfMask } from "../../../Utils/index";

const useStyles = makeStyles((theme) => ({
	paper: {
		marginTop: theme.spacing(8),
		display: "flex",
		flexDirection: "column",
		alignItems: "center",
	},
	avatar: {
		margin: theme.spacing(1),
		backgroundColor: "#6561a4",
	},
	container: {
		width: "30%",
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
}));

export default function UserCreation() {
	const classes = useStyles();
	const history = useHistory();

	const [cpf, setCpf] = useState();
	const [password, setPassword] = useState();
	const [name, setUserName] = useState();

	const [validationMessage, setValidationMessage] = useState();
	const [open, setOpen] = React.useState(false);
	const [severity, setSeverity] = React.useState();

	const handleClose = (event, reason) => {
		if (reason === "clickaway") {
			return;
		}
		setOpen(false);
	};

	const handleCreateUser = async () => {
		try {
			var userCreateAttempt = await createUser({
				name,
				cpf,
				password,
			});

			const { userId } = userCreateAttempt;
			if (userId) {
				setValidationMessage("User Created successfully! Please Sign In");
				setSeverity("success");
				setOpen(true);
				setTimeout(() => {
					history.push("/SignIn");
				}, 1000);
			} else {
				setValidationMessage(userCreateAttempt[0]);
				setSeverity("error");
				setOpen(true);
			}
		} catch (error) {
			setValidationMessage(error.message);
			setSeverity("error");
			setOpen(true);
		}
	};

	return (
		<Container component="main" className={classes.container}>
			<CssBaseline />
			<div className={classes.paper}>
				<Avatar className={classes.avatar}>
					<LockOutlinedIcon />
				</Avatar>
				<Typography component="h1" variant="h5">
					Register
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
						name="cpf"
						type="text"
						autoComplete="number"
						autoFocus
						onChange={(e) => setCpf(cpfMask(e.target.value))}
					/>
					<TextField
						margin="normal"
						required={true}
						fullWidth
						name="userName"
						label="Name"
						type="text"
						id="username"
						autoComplete="text"
						onChange={(e) => setUserName(e.target.value)}
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
						onClick={handleCreateUser}>
						Register
					</Button>
					<Grid container>
						<Grid item>
							<a href="/">{"Already have an account? Sign in here"}</a>
						</Grid>
					</Grid>
				</form>
			</div>
			{validationMessage && (
				<Snackbar
					open={open}
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
		</Container>
	);
}
