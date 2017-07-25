var forms = document.querySelectorAll("form");
var _loop_1 = function (i) {
    var form = forms[i];
    form.addEventListener("submit", function (e) {
        if (form.checkValidity()) {
            var controls = form.querySelectorAll("button,input,textarea,select");
            for (var j = 0; j < controls.length; j++) {
                var control = controls[i];
                if (control instanceof HTMLInputElement ||
                    control instanceof HTMLTextAreaElement) {
                    control.readOnly = true;
                }
                else if (control instanceof HTMLButtonElement) {
                    control.disabled = true;
                }
            }
        }
    });
};
for (var i = 0; i < forms.length; i++) {
    _loop_1(i);
}
