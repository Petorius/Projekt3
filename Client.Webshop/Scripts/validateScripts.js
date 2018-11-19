function validateInput() {
    var error = false;
    var errorText = "";
    var errorEmail = false;
    var errorCity = false;
    var errorStreet = false;
    var errorFirstName = false;
    var errorLastName = false;
    var errorNumber = false;
    var errorZip = false;
    var numberOfErrors = 0;
    var numberOfErrorsProcessed = 0;

    //Zip
    var inputZip = "";
    try {
        inputZip = document.deliveryAddress.zip.value;
    }
    catch (err) { return true; }
    if (inputZip.length < 1) {
        error = true;
        errorZip = true;
        errorText = "Du bedes indtaste dit postnummer!";
        numberOfErrors++;
    }

    //Phone
    var inputPhone = "";
    try {
        inputPhone = document.deliveryAddress.number.value;
    }
    catch (err) { return true; }
    if (inputPhone.length < 1) {
        error = true;
        errorNumber = true;
        errorText = "Du bedes indtaste dit nummer!";
        numberOfErrors++;
    }

    // contact email 
    var inputEmail = "";
    try {
        inputEmail = document.deliveryAddress.email.value;
    }
    catch (err) { return true; }
    if (inputEmail.length < 1) {
        error = true;
        errorEmail = true;
        errorText = "Du bedes indtaste din email!";
        numberOfErrors++;
    }

    // zip City
    var inputCity = "";
    try {
        inputCity = document.deliveryAddress.city.value;
    }
    catch (err) { return true; }
    if (inputCity.length < 1) {
        error = true;
        errorCity = true;
        errorText = "Du bedes indtaste din by!";
        numberOfErrors++;
    }

    // street
    var inputStreet = "";
    try {
        inputStreet = document.deliveryAddress.street.value;
    }
    catch (err) { return true; }
    if (inputStreet.length < 1) {
        error = true;
        errorStreet = true;
        errorText = "Du bedes indtaste din adresse!";
        numberOfErrors++;
    }

    //firstname
    var inputFirstName = "";
    try {
        inputFirstName = document.deliveryAddress.firstName.value;
    }
    catch (err) { return true; }
    if (inputFirstName.length < 1) {
        error = true;
        errorFirstName = true;
        errorText = "Du bedes indtaste dit fornavn!";
        numberOfErrors++;
    }

    //lastName
    var inputLastName = "";
    try {
        inputLastName = document.deliveryAddress.lastName.value;
    }
    catch (err) { return true; }
    if (inputLastName.length < 1) {
        error = true;
        errorLastName = true;
        errorText = "Du bedes indtaste dit efternavn!";
        numberOfErrors++;
    }

    //
    if (error) {
        if (numberOfErrors > 1) {
            errorText = "Du bedes indtaste:";
            if (errorFirstName) {
                errorText += " Fornavn";
                numberOfErrorsProcessed++;
            }
            if (errorLastName) {
                if (numberOfErrorsProcessed < 1) {
                    errorText += " Efternavn";
                } else {
                    errorText += ", Efternavn";
                }
                numberOfErrorsProcessed++;
            }
            if (errorStreet) {
                if (numberOfErrorsProcessed < 1) {
                    errorText += " Adresse";
                } else {
                    errorText += ", Adresse";
                }
                numberOfErrorsProcessed++;
            }
            if (errorZip) {
                if (numberOfErrorsProcessed < 1) {
                    errorText += " Postnummer";
                } else {
                    errorText += ", Postnummer";
                }
                numberOfErrorsProcessed++;
            }
            if (errorCity) {
                if (numberOfErrorsProcessed < 1) {
                    errorText += " By";
                } else {
                    errorText += ", By";
                }
                numberOfErrorsProcessed++;
            }
            if (errorNumber) {
                if (numberOfErrorsProcessed < 1) {
                    errorText += " Nummer";
                } else {
                    errorText += ", Nummer";
                }
                numberOfErrorsProcessed++;
            }
            if (errorEmail) {
                errorText += ", Email";
            }
        }
        alert(errorText);
        return false;
    }
    return true;
}