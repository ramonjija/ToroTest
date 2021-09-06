export const cpfMask = (value) => {
	return value
		.replace(/\D/g, "")
		.replace(/(\d{3})(\d)/, "$1.$2")
		.replace(/(\d{3})(\d)/, "$1.$2")
		.replace(/(\d{3})(\d{1,2})/, "$1-$2")
		.replace(/(-\d{2})\d+?$/, "$1");
};

export const validateCurrency = (value) => {
	const pattern = /^[0-9^&*)]*[.]{0,1}[0-9^&*)]{0,2}$/;
	return pattern.test(value);
};

export const formatCents = (value) => {
	return (Math.round(value * 100) / 100).toFixed(2);
};
