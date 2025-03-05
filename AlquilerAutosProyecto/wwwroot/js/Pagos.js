window.onload = function () {
    listarPagos();
    //filtrarTipoMedicamento();
    //BuscarTipoMedicamento();
    //LimpiarControl();
    ConfigAgregarFiltrar();
}

let objPago;

async function listarPagos() {
    objPago = {
        url: "Pagos/listarPagos",
        cabeceras: ["ID", "Reserva ID", "Monto", "Metodo de Pago", "Fecha de Pago"],
        propiedades: ["id", "reservaId", "monto", "metodoPago", "date"],
        editar: true,
        eliminar: true,
        propiedadId: "id",
        config: true
    }

    pintar(objPago);
}

function buscarPago() {
    let forma = document.getElementById("frmBusqueda");
    let frm = new FormData(forma);
    fetchPost("Pagos/filtrarPagos", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generarTabla(res);
    })
}

function limpiarDatos() {
    LimpiarDatos("formAgregar");
    listarPagos();
}

function guardarPagos() {
    let frmGuardar = document.getElementById("formAgregar");
    let frm = new FormData(frmGuardar);
    confirmacion(undefined, undefined, function (rpta) {
        fetchPost("Pagos/guardarPagos", "text", frm, function (data) {
            if (data == "1") {
                exito();
                listarPagos();
                LimpiarDatos("formAgregar");
            }
        })
    })
}

function editar(id) {
    recuperarGenerico(`Pagos/recuperarPago?id=${id}`, "formAgregar");
    document.getElementById("Options-config").style.display = "none";
}

async function modificar() {
    let frmGuardar = document.getElementById("formAgregar");
    let frm = new FormData(frmGuardar);
    confirmacion(undefined, undefined, function (rpta) {
        fetchPost("Pagos/actualizarPago", "text", frm, function (data) {
            exito();
            listarPagos();
            LimpiarDatos('formAgregar');

        })
    })
}

function eliminar(id) {
    //let frmGuardar = document.getElementById("frmGuardarTipoMedicamento");

    //confirmacion(undefined, `Desea eliminar el dato ${nombre}?`, function (rpta) {
    confirmacion(undefined, undefined, function (rpta) {
        fetchGet(`Pagos/eliminarPago?objPago=${id}`, "text", function (res) {
            exito();
            listarPagos();
            document.getElementById("Options-config").style.display = "none";
        });
    })

    //})

}