window.onload = function () {
    listarClientes();
    ConfigAgregarFiltrar();
}

let objClientes;

async function listarClientes() {
    objClientes = {
        url: "Clientes/listarClientes",
        cabeceras: ["ID", "Nombre", "Apellido", "Telefono", "Email", "Password"],
        propiedades: ["id", "nombre", "apellido", "telefono", "email", "password"],
        editar: true,
        eliminar: true,
        propiedadId: "id",
        config: true
    }

    pintar(objClientes);
}

function buscarCliente() {
    let forma = document.getElementById("frmCliente");
    let frm = new FormData(forma);
    fetchPost("Clientes/filtrarClientes", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generarTabla(res);
    })
}

function limpiarDatos() {
    LimpiarDatos("formAgregar")
    listarClientes();
}

function guardarDatos() {
    let frmGuardar = document.getElementById("formAgregar");
    let frm = new FormData(frmGuardar);
    confirmacion(undefined, undefined, function (rpta) { 
        fetchPost("Clientes/guardarCliente", "text", frm, function (data) {
            if (data == "1") {
                exito();
                listarClientes();
                LimpiarDatos("formAgregar");
            }
        })
    })
}


function editar(id) {
    recuperarGenerico(`Clientes/recuperarDatos?idCliente=${id}`, "formAgregar");
    document.getElementById("Options-config").style.display = "none";
}

function modificar() {
    let frmGuardar = document.getElementById("formAgregar");
    let frm = new FormData(frmGuardar);
    confirmacion(undefined, undefined, function (rpta) { 
        fetchPost("Clientes/actualizarCliente", "text", frm, function (data) {
            exito();
            listarClientes();
            LimpiarDatos('formAgregar');

        })
    })
}
function eliminar(id) {
    confirmacion(undefined, undefined, function (rpta) {
        fetchGet(`Clientes/eliminarCliente?idCliente=${id}`, "text", function (res) {
            exito();
            listarClientes();
            document.getElementById("Options-config").style.display = "none";
        })
    })

}

function AbrirAgregar() {
    let contenedor = document.getElementById("AgregarFiltrar");
    let boton = document.querySelector(`[onclick="AbrirAgregar()"]`);

    // Alternar visibilidad
    if (contenedor.style.display === "block") {
        contenedor.style.display = "none";
        return;
    }

    // Obtener posición del botón
    let rect = boton.getBoundingClientRect();

    // Insertar contenido dinámicamente
    contenedor.innerHTML = OpAgregarFiltrar();

    // Posicionar el menú al lado del botón
    contenedor.style.position = "absolute";
    contenedor.style.top = `${rect.top + window.scrollY - 100}px`;  // Mantiene la posición con desplazamiento
    contenedor.style.left = `${rect.right + 10}px`;  // Aparece justo al lado derecho con margen de 10px
    contenedor.style.display = "block";  // Mostrarlo
}





