window.onload = function () {
    listarEmpleado();
    //filtrarTipoMedicamento();
    //BuscarTipoMedicamento();
    //LimpiarControl();
    ConfigAgregarFiltrar();
}

let objEmpleado;

async function listarEmpleado() {
    objEmpleado = {
        url: "Empleado/listarEmpleados",
        cabeceras: ["ID", "Nombre", "Apellido", "Cargo", "Telefono", "Email", "Password"],
        propiedades: ["id", "nombre", "apellido", "cargo", "telefono", "email", "password"],
        editar: true,
        eliminar: true,
        propiedadId: "id",
            config: true
    }

    pintar(objEmpleado);
}

function buscarEmpleado() {
    let forma = document.getElementById("frmBusqueda");
    let frm = new FormData(forma);
    fetchPost("Empleado/filtrarEmpleado", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generarTabla(res);
    })
}

function limpiarEmpleados() {
    LimpiarDatos("formAgregar");

    listarEmpleado();
}

function editar(id) {
    recuperarGenerico(`Empleado/recuperarEmpleado?id=${id}`, "formAgregar");
    document.getElementById("Options-config").style.display = "none";
}

async function modificar() {
    let frmGuardar = document.getElementById("formAgregar");
    let frm = new FormData(frmGuardar);
    confirmacion(undefined, undefined, function (rpta) {
        fetchPost("Empleado/actualizarEmpleado", "text", frm, function (data) {
            exito();
            listarEmpleado();
            LimpiarDatos('formAgregar');

        })
    })
}

function guardarEmpleado() {
    let frmGuardar = document.getElementById("formAgregar");
    let frm = new FormData(frmGuardar);
    confirmacion(undefined, undefined, function (rpta) {
        fetchPost("Empleado/guardarEmpleado", "text", frm, function (data) {
            if (data == "1") {
                exito();
                listarEmpleado();
                LimpiarDatos("formAgregar");
            }
        })
    })


}

function eliminar(id) {
    //let frmGuardar = document.getElementById("frmGuardarTipoMedicamento");

    //confirmacion(undefined, `Desea eliminar el dato ${nombre}?`, function (rpta) {
    confirmacion(undefined, undefined, function (rpta) {
        fetchGet(`Empleado/eliminarEmpleado?objEmpleado=${id}`, "text", function (res) {
            exito();
            listarEmpleado();
            document.getElementById("Options-config").style.display = "none";
        });
    })
    //})

}

