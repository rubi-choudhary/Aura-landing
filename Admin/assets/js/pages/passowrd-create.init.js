Array.from(document.querySelectorAll("form .auth-pass-inputgroup")).forEach(function (s) {
    Array.from(s.querySelectorAll(".password-addon")).forEach(function (t) {
        t.addEventListener("click", function (t) {
            var e = s.querySelector(".password-input");
            "password" === e.type ? e.type = "text" : e.type = "password"
        })
    })
});
var arr = document.getElementsByClassName("password-input");
var password = document.getElementsByClassName("password-input"),
    confirm_password = document.getElementsByClassName("confirm-password-input");
function validatePassword() {
    password.value != confirm_password.value ? confirm_password.setCustomValidity("Passwords Don't Match") : confirm_password.setCustomValidity("")
}
password.onchange = validatePassword;
var myInput = document.getElementsByClassName("password-input"),
    letter = document.getElementById("pass-lower"),
    capital = document.getElementById("pass-upper"),
    number = document.getElementById("pass-number"),
    specialChar = document.getElementById("pass-special"),
    length = document.getElementById("pass-length");
myInput[0].onfocus = function () {
    document.getElementsByClassName("password-contain")[0].style.display = "block"
},
    myInput[0].onblur = function () {
        document.getElementsByClassName("password-contain")[0].style.display = "none"
    },
    myInput[0].onkeyup = function () {
        myInput[0].value.match(/[a-z]/g) ? (letter.classList.remove("invalid"), letter.classList.add("valid")) : (letter.classList.remove("valid"), letter.classList.add("invalid")),
            myInput[0].value.match(/[A-Z]/g) ? (capital.classList.remove("invalid"), capital.classList.add("valid")) : (capital.classList.remove("valid"), capital.classList.add("invalid"));
        myInput[0].value.match(/[0-9]/g) ? (number.classList.remove("invalid"), number.classList.add("valid")) : (number.classList.remove("valid"), number.classList.add("invalid")),
            myInput[0].value.match(/[~!@#$%^&*(),.?:{}|<>]/g) ? (specialChar.classList.remove("invalid"), specialChar.classList.add("valid")) : (specialChar.classList.remove("valid"), specialChar.classList.add("invalid")),
            8 <= myInput[0].value.length ? (
                length.classList.remove("invalid"),
                length.classList.add("valid")
            ) : (
                    length.classList.remove("valid"),
                    length.classList.add("invalid")
                )
    };