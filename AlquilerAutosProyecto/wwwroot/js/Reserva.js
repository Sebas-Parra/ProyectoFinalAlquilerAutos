window.onload = function () {
    listarReservas();
    ConfigAgregarFiltrar();
}

let objReserva;

async function listarReservas() {
    objReserva = {
        url: "Reserva/listarReservas",
        cabeceras: ["ID", "ID Cliente", "ID Vehiculo", "Fecha de inicio", "Fecha de fin", "Estado"],
        propiedades: ["id", "clienteId", "vehiculoId", "fechaInicio", "fechaFin", "estado"],
        editar: true,
        eliminar: true,
        propiedadId: "id",
        config: true
    }

    pintar(objReserva);
}


function buscarReserva() {
    let forma = document.getElementById("frmBusqueda");
    let frm = new FormData(forma);
    fetchPost("Reserva/filtrarReserva", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generarTabla(res);
    })
}

function limpiarReserva() {
    LimpiarDatos("formAgregar");

    listarReservas();
}

function guardarReservas() {
    let frmGuardar = document.getElementById("formAgregar");
    let frm = new FormData(frmGuardar);
    confirmacion(undefined, undefined, function (rpta) {
        fetchPost("Reserva/guardarReserva", "text", frm, function (data) {
            if (data == "1") {
                exito();
                listarReservas();
                LimpiarDatos("formAgregar");
            }
        })
    })
}

function editar(id) {
    recuperarGenerico(`Reserva/recuperarReserva?id=${id}`, "formAgregar");
    document.getElementById("Options-config").style.display = "none";
}

async function modificar() {
    let frmGuardar = document.getElementById("formAgregar");
    let frm = new FormData(frmGuardar);
    confirmacion(undefined, undefined, function (rpta) {
        fetchPost("Reserva/actualizarReserva", "text", frm, function (data) {
            exito();
            listarReservas();
            LimpiarDatos('formAgregar');

        })
    })
}

function eliminar(id) {
    //let frmGuardar = document.getElementById("frmGuardarTipoMedicamento");

    //confirmacion(undefined, `Desea eliminar el dato ${nombre}?`, function (rpta) {
    confirmacion(undefined, undefined, function (rpta) {
        fetchGet(`Reserva/eliminarReserva?objReserva=${id}`, "text", function (res) {
            exito();
            listarReservas();
            document.getElementById("Options-config").style.display = "none";
        });
    })
    //})

}

