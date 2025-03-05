document.addEventListener("DOMContentLoaded", function () {
    mostrarAutos(); // Cargar los primeros autos
});

// Variables para la paginación
let currentPage = 1;
const autosPorPagina = 4;
let vehiculosGlobal = []; // Se almacenarán todos los autos aquí

async function mostrarAutos() {
    fetchGet("Vehiculos/listarVehiculos", "json", function (vehiculos) {
        vehiculosGlobal = vehiculos.filter(vehiculo => vehiculo.estado === "Disponible"); // Guardar la lista completa de autos
        renderizarAutos(); // Mostrar los autos de la primera página
        actualizarPaginacion();
    });
}

function renderizarAutos() {
    let autosContainer = document.getElementById("Autos");
    autosContainer.innerHTML = ""; // Limpiar antes de agregar nuevos autos

    let startIndex = (currentPage - 1) * autosPorPagina;
    let endIndex = startIndex + autosPorPagina;
    let autosAMostrar = vehiculosGlobal.slice(startIndex, endIndex);

    autosAMostrar.forEach(vehiculo => {
        if (vehiculo.estado === "Disponible") {
            let autoCard = document.createElement("div");
            autoCard.classList.add("car-card");
            autoCard.innerHTML = `
                <div class="bg">
                    <h3 class="Title-list">${vehiculo.marca} ${vehiculo.modelo}</h3>
                    <p class="PrecioAuto"><strong>$${vehiculo.precio}</strong> / Per Day</p>
                    <button class="btn" onclick="guardarYRedirigir(${vehiculo.id}, ${vehiculo.precio})">Drive Now</button>
                    <div class="car-image-container">
                        <img src="${vehiculo.imagen || 'https://source.unsplash.com/250x150/?car'}" 
                            alt="${vehiculo.marca} ${vehiculo.modelo}">
                    </div>
                </div>
                <div class="blob"></div>
            `;
            autosContainer.appendChild(autoCard);
        }
    });

    actualizarPaginacion();
}

function actualizarPaginacion() {
    let totalPaginas = Math.ceil(vehiculosGlobal.length / autosPorPagina);
    let paginationDots = document.getElementById("pagination-dots");

    paginationDots.innerHTML = ""; // Limpiar los puntos previos

    for (let i = 1; i <= totalPaginas; i++) {
        let dot = document.createElement("div");
        dot.classList.add("page-dot");
        if (i === currentPage) dot.classList.add("active");

        dot.addEventListener("click", function () {
            currentPage = i;
            renderizarAutos();
        });

        paginationDots.appendChild(dot);
    }
}

// Mostrar todos los autos cuando se presiona "See All"
document.getElementById("see-all").addEventListener("click", function () {
    let autosContainer = document.getElementById("Autos");
    autosContainer.innerHTML = ""; // Limpiar contenido

    vehiculosGlobal.forEach(vehiculo => {
        if (vehiculo.estado === "Disponible") {
            let autoCard = document.createElement("div");
            autoCard.classList.add("car-card");
            autoCard.innerHTML = `
                <div class="bg">
                    <h3>${vehiculo.marca} ${vehiculo.modelo}</h3>
                    <p><strong>$${vehiculo.precio}</strong> / Per Day</p>
                    <button class="btn" onclick="guardarYRedirigir(${vehiculo.id})">Drive Now</button>
                    <div class="car-image-container">
                        <img src="${vehiculo.imagen || 'https://source.unsplash.com/250x150/?car'}" 
                            alt="${vehiculo.marca} ${vehiculo.modelo}">
                    </div>
                </div>
                <div class="blob"></div>
            `;
            autosContainer.appendChild(autoCard);
        }
    });

    // Ocultar paginación y mostrar botón de regreso
    document.querySelector(".pagination").style.display = "none";
    document.getElementById("see-all").style.display = "none";
    document.getElementById("back-to-pages").style.display = "inline-block";
});

// Volver a la paginación cuando se presiona "Back to Pages"
document.getElementById("back-to-pages").addEventListener("click", function () {
    currentPage = 1; // Volver a la primera página
    renderizarAutos(); // Volver a mostrar los autos paginados

    // Mostrar paginación y ocultar el botón de regreso
    document.querySelector(".pagination").style.display = "flex";
    document.getElementById("see-all").style.display = "inline-block";
    document.getElementById("back-to-pages").style.display = "none";
});

function guardarYRedirigir(id, monto) {
    localStorage.setItem("id", id);
    localStorage.setItem("monto", monto);
    window.location.href = "/Vehiculos/mostrarVehiculos";
}
