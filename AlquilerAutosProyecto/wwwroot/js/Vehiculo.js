window.onload = function () {
    listarVehiculos();
    ConfigAgregarFiltrar();
}

let objVehiculo;

async function listarVehiculos() {
    objVehiculo = {
        url: "Vehiculos/listarVehiculos",
        cabeceras: ["ID", "Marca", "Modelo", "Año", "Precio", "Estado", "Link Video"],
        propiedades: ["id", "marca", "modelo", "anio", "precio", "estado", "video"],
        editar: true,
        eliminar: true,
        propiedadId: "id",
        propiedadImagen: true,
        config: true
    }

    pintar(objVehiculo);
}
function buscarVehiculo() {
    let forma = document.getElementById("frmVehiculo");
    let frm = new FormData(forma);
    fetchPost("Vehiculos/filtrarVehiculos", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generarTabla(res);
    })
}

function limpiarDatos() {
    LimpiarDatos("formAgregar")
    listarVehiculos();
}
function guardarDatos() {
    let frmGuardar = document.getElementById("formAgregar");
    let frm = new FormData(frmGuardar);
    confirmacion(undefined, undefined, function (rpta) {
        fetchPost("Vehiculos/guardarVehiculo", "text", frm, function (data) {
            if (data == "1") {
                exito();
                listarVehiculos();
                LimpiarDatos("formAgregar");
            }
        })
    })
}

function editar(id) {
    recuperarGenerico(`Vehiculos/recuperarVehiculo?idVehiculo=${id}`, "formAgregar");
    document.getElementById("Options-config").style.display = "none";
}

function modificar() {
    let frmGuardar = document.getElementById("formAgregar");
    let frm = new FormData(frmGuardar);
    confirmacion(undefined, undefined, function (rpta) {
        fetchPost("Vehiculos/actualizarVehiculo", "text", frm, function (data) {
            exito();
            listarVehiculos();
            LimpiarDatos('formAgregar');

        })
    })
}
function eliminar(id) {
    confirmacion(undefined, undefined, function (rpta) {
        fetchGet(`Vehiculos/eliminarVehiculo?idVehiculo=${id}`, "text", function (res) {
            exito();
            listarVehiculos();
            document.getElementById("Options-config").style.display = "none";
        })
    })
}

function guardarYRedirigir(id) {
    localStorage.setItem("id", id);
    window.location.href = "/Vehiculos/mostrarVehiculos"; 
}