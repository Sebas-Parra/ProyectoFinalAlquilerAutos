window.onload = function () {
    obtenerCliente();
    if (!currentDate) {
        currentDate = new Date(); // Solo se inicializa una vez
    }
    renderCalendar();
}

document.addEventListener("DOMContentLoaded", function () {
    let hoy = new Date().toISOString().split("T")[0];
    document.getElementById("date").value = hoy;
});


let objReserva = {
    clienteId: 0,
    vehiculoId: 0,
    fechaInicio: "",
    fechaFin: "",
    estado: ""
};

let objPago = {
    reservaId: 0,
    monto: 0,
    metodoPago: "",
    date: ""
};

const idVehiculo = localStorage.getItem("id");
const monto = localStorage.getItem("monto");

async function obtenerCliente() {
    fetchGet('Login/obtenerCliente', "json", function (data) {
        objReserva.clienteId = data;
    })
}

objReserva.vehiculoId = idVehiculo;


//Calendario

const calendarBody = document.getElementById("calendar-body");
const monthYear = document.getElementById("monthYear");
const prevMonth = document.getElementById("prevMonth");
const nextMonth = document.getElementById("nextMonth");

let currentDate = new Date();

let selectedStartDate = null;
let selectedEndDate = null;

function renderCalendar() {
    console.log(`Renderizando calendario: ${currentDate.toISOString()}`);
    calendarBody.innerHTML = "";
    let year = currentDate.getFullYear();
    let month = currentDate.getMonth();

    let firstDay = new Date(year, month, 1).getDay();
    let lastDate = new Date(year, month + 1, 0).getDate();

    monthYear.textContent = currentDate.toLocaleString("es-ES", { month: "long", year: "numeric" });

    for (let i = 0; i < firstDay; i++) {
        let emptyDiv = document.createElement("div");
        calendarBody.appendChild(emptyDiv);
    }

    for (let day = 1; day <= lastDate; day++) {
        let dayDiv = document.createElement("div");
        dayDiv.classList.add("day");
        dayDiv.textContent = day;

        let selectedDate = `${year}-${String(month + 1).padStart(2, '0')}-${String(day).padStart(2, '0')}`;

        // Resaltar fecha seleccionada
        if (selectedStartDate === selectedDate) {
            dayDiv.classList.add("selected-start");
        }
        if (selectedEndDate === selectedDate) {
            dayDiv.classList.add("selected-end");
        }

        dayDiv.addEventListener("click", function () {
            if (!selectedStartDate || (selectedStartDate && selectedEndDate)) {
                selectedStartDate = selectedDate;
                selectedEndDate = null;
                objReserva.fechaInicio = selectedStartDate;
            } else {
                selectedEndDate = selectedDate;
                objReserva.fechaFin = selectedEndDate;
            }
            renderCalendar();
        });

        calendarBody.appendChild(dayDiv);
    }
}


prevMonth.addEventListener("click", (event) => {
    event.preventDefault(); // Evita que otros eventos interfieran
    console.log("Cambiando al mes anterior");
    currentDate.setMonth(currentDate.getMonth() - 1);
    renderCalendar();
});

nextMonth.addEventListener("click", (event) => {
    event.preventDefault(); // Evita que otros eventos interfieran
    console.log("Cambiando al mes siguiente");
    currentDate.setMonth(currentDate.getMonth() + 1);
    renderCalendar();
});


objReserva.estado = "Pendiente"

renderCalendar();

function guardarReservas() {
    let frm = new FormData();
    for (const key in objReserva) {
        if (objReserva.hasOwnProperty(key)) {
            frm.append(key, objReserva[key]);
        }
    }

    fetchPost("Reserva/guardarReserva", "text", frm, function (data) {
        if (data == "1") {
            document.querySelector("#monto").value = monto;
        }
    })
}

function cancelar() {
    window.location.href = "../Vehiculos/mostrarVehiculos";
}

async function eliminar() {
    let frm = new FormData();
    for (const key in objReserva) {
        if (objReserva.hasOwnProperty(key)) {
            frm.append(key, objReserva[key]);
        }
    }

    try {
        fetchPost("Reserva/filtrarReserva", "json", frm, function (res) {
            const id = res[0].id;
            fetchGet(`Reserva/eliminarReserva?objReserva=${id}`, "text", function (data) {
                if (data == "true") {
                    
                }
            });
        });
    } catch (error) {
        console.error("Error al eliminar la reserva:", error);
    }
}

objPago.monto = monto;

function guardarPagos() {
    let frm = new FormData();
    let metodoPago = document.querySelector("#metodoPago").value
    objPago.metodoPago = metodoPago;
    let fecha = document.querySelector("#date").value
    objPago.date = fecha;
    for (const key in objReserva) {
        if (objReserva.hasOwnProperty(key)) {
            frm.append(key, objReserva[key]);
        }
    }
    try {
        fetchPost("Reserva/filtrarReserva", "json", frm, function (res) {
            objPago.reservaId = res[0].id;
        let frmPago = new FormData();
        for (const key in objPago) {
            if (objPago.hasOwnProperty(key)) {
                frmPago.append(key, objPago[key]);
            }
        }
            fetchPost("Pagos/guardarPagos", "text", frmPago, function (data) {
                if (data == "1") {
                    exito("La reserva de su vehiculo se a realizado con exito");
                    //window.location.href = "../Home/Index";
                }
                fetchGet(`Vehiculos/recuperarVehiculo?idVehiculo=${idVehiculo}`, "json", function (data) {
                    let frmVehiculo = new FormData();
                    data.estado = "Reservado";
                    for (const key in data) {
                        if (data.hasOwnProperty(key)) {
                            frmVehiculo.append(key, data[key]);
                        }
                    }
                    fetchPost("Vehiculos/actualizarVehiculo", "text", frmVehiculo, function (data) {
                    })
                })
        })
        });
    } catch (error) {
        console.error("Error al eliminar la reserva:", error);
    }

}

function modificar() {
    
}

