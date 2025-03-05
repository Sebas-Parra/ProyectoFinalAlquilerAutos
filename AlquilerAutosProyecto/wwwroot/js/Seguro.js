window.onload = function () {
    listarSeguros();
    ConfigAgregarFiltrar();
}

let objSeguros;

async function listarSeguros() {
    objSeguros = {
        url: "Seguro/listarSeguros",
        cabeceras: ["ID", "ID Reserva", "Tipo Seguro", "Costo"],
        propiedades: ["id", "reservaId", "tipoSeguro", "costo"],
        editar: true,
        eliminar: true,
        propiedadId: "id",
        config: true
    }

    pintar(objSeguros);
}

function buscarSeguro() {
    let forma = document.getElementById("frmSeguro");
    let frm = new FormData(forma);
    fetchPost("Seguro/filtrarSeguros", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generarTabla(res);
    })
}

function limpiarDatos() {
    LimpiarDatos("formAgregar")
    listarSeguros();
}

function guardarDatos() {
    let frmGuardar = document.getElementById("formAgregar");
    let frm = new FormData(frmGuardar);
    confirmacion(undefined, undefined, function (rpta) {
        fetchPost("Seguro/guardarSeguro", "text", frm, function (data) {
            if (data == "1") {
                exito();
                listarSeguros();
                LimpiarDatos("formAgregar");
            }
        })
    })
}

function editar(id) {
    recuperarGenerico(`Seguro/recuperarDatos?idSeguro=${id}`, "formAgregar");
    document.getElementById("Options-config").style.display = "none";
}

function modificar() {
    let frmGuardar = document.getElementById("formAgregar");
    let frm = new FormData(frmGuardar);
    confirmacion(undefined, undefined, function (rpta) {
        fetchPost("Seguro/actualizarSeguro", "text", frm, function (data) {
            exito();
            listarSeguros();
            LimpiarDatos('formAgregar');

        })
    })
}
function eliminar(id) {
    confirmacion(undefined, undefined, function (rpta) {
        fetchGet(`Seguro/eliminarSeguro?idSeguro=${id}`, "text", function (res) {
            exito();
            listarSeguros();
            document.getElementById("Options-config").style.display = "none";
        })
    })
}
