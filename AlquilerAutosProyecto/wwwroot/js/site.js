// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    const themeToggle = document.getElementById("onoff");
    const body = document.body;
    const carImage = document.querySelector(".car-image");

    const urlDarkTheme = "https://i.pinimg.com/474x/5d/77/49/5d77493696b17938a0f217f33a9868a6.jpg";
    const urlLightTheme = "https://i.pinimg.com/736x/3f/75/cd/3f75cdb65aab10b6c0fb6831492594f2.jpg";

    //  Verificar si estamos en Home/Index
    if (typeof currentView !== "undefined" && currentView === "Home/Index") {
        if (carImage) { // Evitar errores si carImage no existe
            if (localStorage.getItem("theme") === "dark") {
                carImage.style.backgroundImage = `url('${urlDarkTheme}')`;
            } else {
                carImage.style.backgroundImage = `url('${urlLightTheme}')`;
            }
        }
    }

    // ✅ Cargar la preferencia del usuario desde localStorage
    if (localStorage.getItem("theme") === "dark") {
        themeToggle.checked = true;
        body.classList.add("dark-theme");
    } else {
        themeToggle.checked = false;
        body.classList.remove("dark-theme");
    }

    //  Cambiar el tema cuando el usuario cambia el interruptor
    themeToggle.addEventListener("change", function () {
        if (themeToggle.checked) {
            body.classList.add("dark-theme");
            body.classList.remove("light-theme"); // Remueve light-theme si está activado
            localStorage.setItem("theme", "dark");
        } else {
            body.classList.remove("dark-theme");
            body.classList.add("light-theme"); // Agregar explícitamente la clase light-theme
            localStorage.setItem("theme", "light");
        }

        //  Solo cambiar la imagen en Home/Index
        if (typeof currentView !== "undefined" && currentView === "Home/Index" && carImage) {
            carImage.style.backgroundImage = themeToggle.checked
                ? `url('${urlDarkTheme}')`
                : `url('${urlLightTheme}')`;
        }
    });
});
