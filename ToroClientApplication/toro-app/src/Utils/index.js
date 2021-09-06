export const cpfMask = (value) => {
	return value
		.replace(/\D/g, "")
		.replace(/(\d{3})(\d)/, "$1.$2")
		.replace(/(\d{3})(\d)/, "$1.$2")
		.replace(/(\d{3})(\d{1,2})/, "$1-$2")
		.replace(/(-\d{2})\d+?$/, "$1");
};

export const numberValidation = (value) => {
	const number = value.replace(/(?!0\.00)\d{1,3}(,\d{3})*(\.\d\d)?$/, "");
	return number;
};

export const formatCents = (value) => {
	return (Math.round(value * 100) / 100).toFixed(2);
};
