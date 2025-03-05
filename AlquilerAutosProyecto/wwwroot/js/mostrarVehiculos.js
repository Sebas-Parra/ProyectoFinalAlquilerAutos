window.onload = function () {
    obtenerVehiculo();
}

async function obtenerVehiculo() {
    const id = localStorage.getItem("id");
    fetchGet(`Vehiculos/recuperarVehiculo?idVehiculo=${id}`, "json", function (data) {
        let titulo = document.getElementById("titulo-principal");
        let image = document.querySelector(".car-image");
        let descripcion = document.querySelector(".description-section");
        let video = document.querySelector(".model-year");
        titulo.innerHTML = `${data.marca} <span>${data.modelo}</span>`;
        video.innerHTML = `<p>${data.marca} ${data.modelo} <span>${data.anio}</span></p>
                <a href="${data.video}" target="_blank" class="watch-video">Ver video</a>`
        image.src = `${data.imagen}`;
        descripcion.innerHTML = `<h2>Características</h2>
                                <ul>
                                    <li><strong>Marca:</strong> ${data.marca}</li>
                                    <li><strong>Modelo:</strong> ${data.modelo}</li>
                                    <li><strong>Año:</strong> ${data.anio}</li>
                                    <li><strong>Precio:</strong> ${data.precio}</li>
                                    <li><strong>Estado:</strong> ${data.estado}</li>
                                </ul>
                                <button onclick="reservar()" class="test-drive-btn">RESERVAR AUTOMOVIL</button>
                <div class="warranty">
                    Garantía <span>5</span> años<br>
                    en todos los componentes
                </div>`;

    })
}
const reservar = () => {
    fetchGet(`Login/validateLogin`, "json", function (data) {
        console.log("hola" + data);
        if (data) {
            window.location.href = "/Reserva/calendario";
        } else {
            window.location.href = "/Login/Index";
        }
    })
}