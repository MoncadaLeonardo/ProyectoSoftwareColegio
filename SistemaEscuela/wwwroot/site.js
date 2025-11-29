window.SistemaEscuela = {
    submitLogoutForm: function (formId) {
        var form = document.getElementById(formId);
        if (form) {
            console.log("Enviando formulario de logout: " + formId);
            form.submit();
        } else {
            console.error("No se encontró el formulario con ID: " + formId);
        }
    }
};