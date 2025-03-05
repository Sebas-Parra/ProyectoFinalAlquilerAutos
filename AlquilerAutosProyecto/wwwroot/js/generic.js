function get(valor) {
    return document.getElementById(valor).value;
}

function set(idControl, valor) {
    return document.getElementById(idControl).value = valor;
}

function setN(namecontrol, valor) {
    let elementos = document.getElementsByName(namecontrol);
    if (elementos.length > 0) {
        elementos[0].value = valor;
    }
}

function GetN(namecontrol) {
    let elementos = document.getElementsByName(namecontrol);
    return elementos.length > 0 ? elementos[0].value : "";
}

function LimpiarDatos(idFormulario) {
    let formulario = document.getElementById(idFormulario);
    if (formulario) {
        let elementos = formulario.querySelectorAll("input, textarea, select");
        elementos.forEach(elemento => {
            elemento.value = "";
        });
    }
}

function confirmacion(titulo = "Confirmación", texto = "Desea guardar los cambios?", callback) {
    Swal.fire({
        title: titulo,
        text: texto,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si",
        cancelButtonText: "Cancelar",
        customClass: {
            popup: 'custom-popup', // Clase CSS personalizada
            title: 'custom-title', // Clase para el título
            htmlContainer: 'custom-text', // Clase para el texto
            confirmButton: 'custom-confirm-button', // Clase para el botón de confirmar
            cancelButton: 'custom-cancel-button', // Clase para el botón de cancelar
            
        },
        

    }).then((result) => {
        if (result.isConfirmed) {
            callback();
        }
    });
}

function exito() {
    toastr.options = {
        /*"closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"*/
        "closeButton": true,
        "positionClass": "toast-top-full-width",
        "timeOut": "3000",
        "extendedTimeOut": "2000",
        "hideMethod": "slideUp"
    }

    toastr.success("Registro realizado exitosamente");
}


/*function recuperarGenerico(url, idFormulario) {
    let elementosName = document.querySelectorAll("#" + idFormulario + " [name]");
    let nombrename;

    fetchGet(url, "json", function (data) {
        for (let i = 0; i < elementosName.length; i++) {
            nombrename = elementosName[i].name;
            let elemento = elementosName[i];
            let valor = data[nombrename];

            if (elemento.type === "date" && valor) {
                // Formatear la fecha a YYYY-MM-DD si es necesario
                let fecha = new Date(valor);
                let fechaFormateada = fecha.toISOString().split("T")[0]; // Obtiene YYYY-MM-DD
                elemento.value = fechaFormateada;
            } else {
                elemento.value = valor; // Para otros tipos de inputs
            }
        }
    });
}*/
function recuperarGenerico(url, idFormulario) {
    let elementosName = document.querySelectorAll("#" + idFormulario + " [name]");
    let nombrename;

    fetchGet(url, "json", function (data) {
        for (let i = 0; i < elementosName.length; i++) {
            nombrename = elementosName[i].name;
            let elemento = elementosName[i];
            let valor = data[nombrename];

            if (!valor) continue; // Evitar asignar valores vacíos o nulos

            if (elemento.tagName === "SELECT") {
                // Si es un select, asignar el valor si existe en las opciones
                let opcionExiste = Array.from(elemento.options).some(opt => opt.value === valor);
                if (opcionExiste) {
                    elemento.value = valor;
                }
            } else if (elemento.type === "date") {
                // Formatear la fecha a YYYY-MM-DD si es necesario
                let fecha = new Date(valor);
                let fechaFormateada = fecha.toISOString().split("T")[0]; // Obtiene YYYY-MM-DD
                elemento.value = fechaFormateada;
            } else {
                // Para otros tipos de inputs
                elemento.value = valor;
            }
        }
    });
}

async function fetchGet(url, tipoRespuesta, callback) {
    try {
        let raiz = document.getElementById("hdfOculto").value;

        //http://localhost
        let urlCompleta = window.location.protocol + "//" + window.location.host + "/" + raiz + url;

        let res = await fetch(urlCompleta)

        if (tipoRespuesta == "json") {
            res = await res.json();
        }
        else if (tipoRespuesta == "text") {
            res = await res.text();
        }

        //JSON (object)
        console.log("Datos recibidos del backend:", res);

        callback(res)

    } catch (e) {
        console.error("Error occurred during fetchGet:", e);
        alert("Ocurrió un problema: " + e.message);
    }
}

async function fetchPost(url, tipoRespuesta, frm, callback) {
    try {
        let raiz = document.getElementById("hdfOculto").value;
        let urlCompleta = window.location.protocol + "//" + window.location.host + "/" + raiz + url;
        let res = await fetch(urlCompleta, {
            method: "POST",
            body: frm
        })

        if (!res.ok) {
            throw new Error(`HTTP error! status: ${res.status}`);
        }

        if (tipoRespuesta == "json") {
            res = await res.json();
        }
        else if (tipoRespuesta == "text") {
            res = await res.text();
        }
        console.log(res);
        callback(res);
    } catch (e) {
        console.log(e);
        alert("Ocurrió un problema en POST");
    }
}

let objConfiguracionGlobal;

//(url:"", nombreColumnas[], nombrePropiedades: [])
function pintar(objConfiguracion) {
    objConfiguracionGlobal = objConfiguracion;

    //Crear el div contenedor de forma dinamica
    if (objConfiguracionGlobal.divContenedorTabla == undefined) {
        objConfiguracionGlobal.divContenedorTabla = "divContenedorTabla"
    }
    if (objConfiguracionGlobal.editar == undefined) {
        objConfiguracionGlobal.editar = "false";
    }
    if (objConfiguracionGlobal.eliminar == undefined) {
        objConfiguracionGlobal.eliminar = "false";
    }
    if (objConfiguracionGlobal.propiedadId == undefined) {
        objConfiguracionGlobal.propiedadId = "";
    }
    if (objConfiguracionGlobal.propiedadImagen == undefined) {
        objConfiguracionGlobal.propiedadImagen = false;
    }
    if (objConfiguracionGlobal.verDetalle == undefined) {
        objConfiguracionGlobal.verDetalle = false;
    }
    if (objConfiguracionGlobal.config == undefined) {
        objConfiguracionGlobal.config = false;
    }

    fetchGet(objConfiguracion.url, "json", function (res) {
        let contenido = "";

        contenido += "<div id='" + objConfiguracionGlobal.divContenedorTabla + "'>"

        contenido += generarTabla(res);

        contenido += "</div>";

        document.getElementById("divtabla").innerHTML = contenido;

        activarPaginacion("myTable")
    })
}

function generarTabla(res) {

    let contenido = " ";

    //cabeceras: ["id Tipo Medicamento", "Nombre", "Descripcion"],
    let cabeceras = objConfiguracionGlobal.cabeceras;

    //propiedades: ["idMedicamento", "nombre", "descripcion"]
    let nombrePropiedades = objConfiguracionGlobal.propiedades;

    contenido = '<table id="myTable" class="table">';
    contenido += "<thead>";
    contenido += "<tr>";

    for (var i = 0; i < cabeceras.length; i++) {
        contenido += `<td class="table-active">` + cabeceras[i] + "</td>";
    }
    if (objConfiguracionGlobal.propiedadImagen == true) {
        contenido += `<td class="table-active">Imagen</td>`;
    }

    if (objConfiguracionGlobal.editar == true || objConfiguracionGlobal.eliminar == true) {
        contenido += `<td class="table-active">Operaciones</td>`;
    }

    contenido += "</tr>";
    contenido += "</thead>";
    contenido += "<tbody>";

    let nroRegistros = res.length;
    let obj;
    let propiedadActual;

    for (let i = 0; i < nroRegistros; i++) {
        obj = res[i];
        contenido += "<tr>";

        for (var j = 0; j < nombrePropiedades.length; j++) {
            propiedadActual = nombrePropiedades[j];
            contenido += "<td>" + obj[propiedadActual] + "</td>";
        }
        if (objConfiguracionGlobal.propiedadImagen == true) {
            contenido += "<td><img src='" + obj["imagen"] + "' width='100px' height='100px'></td>";
        }

        if (objConfiguracionGlobal.editar == true || objConfiguracionGlobal.eliminar == true || objConfiguracionGlobal.verDetalle == true) {
            let propiedadId = objConfiguracionGlobal.propiedadId;
            contenido += "<td>";
            contenido += " ";
            if (objConfiguracionGlobal.verDetalle == true) {
                contenido += `<i onclick="guardarYRedirigir(${obj[propiedadId]})" class="btn btn-info"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-info-square" viewBox="0 0 16 16">
                  <path d="M14 1a1 1 0 0 1 1 1v12a1 1 0 0 1-1 1H2a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1zM2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2z"/>
                  <path d="m8.93 6.588-2.29.287-.082.38.45.083c.294.07.352.176.288.469l-.738 3.468c-.194.897.105 1.319.808 1.319.545 0 1.178-.252 1.465-.598l.088-.416c-.2.176-.492.246-.686.246-.275 0-.375-.193-.304-.533zM9 4.5a1 1 0 1 1-2 0 1 1 0 0 1 2 0"/>
                </svg></i>`
            }
            contenido += " ";
            if (objConfiguracionGlobal.config == true) {
                contenido += `<i onclick="AbrirConfig(${obj[propiedadId]})" class="btn btn-info"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-gear" viewBox="0 0 16 16">
                <path d="M8 4.754a3.246 3.246 0 1 0 0 6.492 3.246 3.246 0 0 0 0-6.492M5.754 8a2.246 2.246 0 1 1 4.492 0 2.246 2.246 0 0 1-4.492 0"/>
                <path d="M9.796 1.343c-.527-1.79-3.065-1.79-3.592 0l-.094.319a.873.873 0 0 1-1.255.52l-.292-.16c-1.64-.892-3.433.902-2.54 2.541l.159.292a.873.873 0 0 1-.52 1.255l-.319.094c-1.79.527-1.79 3.065 0 3.592l.319.094a.873.873 0 0 1 .52 1.255l-.16.292c-.892 1.64.901 3.434 2.541 2.54l.292-.159a.873.873 0 0 1 1.255.52l.094.319c.527 1.79 3.065 1.79 3.592 0l.094-.319a.873.873 0 0 1 1.255-.52l.292.16c1.64.893 3.434-.902 2.54-2.541l-.159-.292a.873.873 0 0 1 .52-1.255l.319-.094c1.79-.527 1.79-3.065 0-3.592l-.319-.094a.873.873 0 0 1-.52-1.255l.16-.292c.893-1.64-.902-3.433-2.541-2.54l-.292.159a.873.873 0 0 1-1.255-.52zm-2.633.283c.246-.835 1.428-.835 1.674 0l.094.319a1.873 1.873 0 0 0 2.693 1.115l.291-.16c.764-.415 1.6.42 1.184 1.185l-.159.292a1.873 1.873 0 0 0 1.116 2.692l.318.094c.835.246.835 1.428 0 1.674l-.319.094a1.873 1.873 0 0 0-1.115 2.693l.16.291c.415.764-.42 1.6-1.185 1.184l-.291-.159a1.873 1.873 0 0 0-2.693 1.116l-.094.318c-.246.835-1.428.835-1.674 0l-.094-.319a1.873 1.873 0 0 0-2.692-1.115l-.292.16c-.764.415-1.6-.42-1.184-1.185l.159-.291A1.873 1.873 0 0 0 1.945 8.93l-.319-.094c-.835-.246-.835-1.428 0-1.674l.319-.094A1.873 1.873 0 0 0 3.06 4.377l-.16-.292c-.415-.764.42-1.6 1.185-1.184l.292.159a1.873 1.873 0 0 0 2.692-1.115z"/>
                </svg></i>`
            }

            contenido += "</td>";
        }
        contenido += "</tr>";
    }
    contenido += "</tbody>"
    contenido += "</table>"

    return contenido;
}
/*function exito(titulo = "Se Realizo con exito la operacion!!") {
    Swal.fire({
        title: titulo,
        icon: "success",
        draggable: true
    });
}*/

function activarPaginacion(idTabla) {
    // Comprobamos si el ID de la tabla es válido
    if (document.getElementById(idTabla)) {
        $(`#${idTabla}`).DataTable({
            "paging": true,   // Habilitar la paginación
            "searching": true, // Habilitar la búsqueda
            "info": true,      // Muestra información sobre el número de registros
            "lengthChange": true, // Permite cambiar el número de filas por página
            "pageLength": 5,  // Número de filas por página
        });
    } else {
        console.error('No se encontró la tabla con el ID proporcionado');
    }
}

function ConfigAgregarFiltrar() {
    let contenedor = document.getElementById("AgregarGenerico");

    let contenido = "";

    /*<div tabindex="0" class="plusButton">
  <svg class="plusIcon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 30 30">
    <g mask="url(#mask0_21_345)">
      <path d="M13.75 23.75V16.25H6.25V13.75H13.75V6.25H16.25V13.75H23.75V16.25H16.25V23.75H13.75Z"></path>
    </g>
  </svg>




</div>*/

    contenido += `<div tabindex="0" class="plusButton" onclick="LimpiarDatos('formAgregar')"   data-bs-toggle="modal" data-bs-target="#exampleModal">`
    contenido += `<svg class="plusIcon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 30 30">`
    contenido += `<g mask="url(#mask0_21_345)">`
    contenido += `<path d="M13.75 23.75V16.25H6.25V13.75H13.75V6.25H16.25V13.75H23.75V16.25H16.25V23.75H13.75Z"></path>`
    contenido += `</g>`
    contenido += `</svg>`
    contenido += `</div>`

    contenedor.innerHTML = contenido;

}

/*function OpAgregarFiltrar() {

    let contenido = "";
    contenido += `<div class="radio-input-wrapper">
  <label class="labelOp">
    <input value="value-2" name="value-radio" id="value-2" class="radio-input" type="radio">
    <div class="radio-design"></div>
    <div class="label-text">Agregar</div>
  </label>
  <label class="labelOp">
    <input value="value-3" name="value-radio" id="value-3" class="radio-input" type="radio">
    <div class="radio-design"></div>
    <div class="label-text">Earth</div>
  </label>
  <label class="labelOp">
    <input value="value-4" name="value-radio" id="value-4" class="radio-input" type="radio">
    <div class="radio-design"></div>
    <div class="label-text">Water</div>
  </label>
</div>`

    return contenido;
}*/

function OpcionesConfig(id) {

    let contenido = "";
    let contenedor = document.getElementById("Options-config");

    if (objConfiguracionGlobal.editar == true || objConfiguracionGlobal.eliminar == true) {
        contenido = `<div class="card">
  <ul class="list">
    <li class="element" onclick="editar(${id})" data-bs-toggle="modal" data-bs-target="#exampleModal">
      <svg
        class="lucide lucide-pencil"
        stroke-linejoin="round"
        stroke-linecap="round"
        stroke-width="2"
        stroke="#7e8590"
        fill="none"
        viewBox="0 0 24 24"
        height="25"
        width="25"
        xmlns="http://www.w3.org/2000/svg"
      >
        <path
          d="M21.174 6.812a1 1 0 0 0-3.986-3.987L3.842 16.174a2 2 0 0 0-.5.83l-1.321 4.352a.5.5 0 0 0 .623.622l4.353-1.32a2 2 0 0 0 .83-.497z"
        ></path>
        <path d="m15 5 4 4"></path>
      </svg>
      <p class="label">Edit</p>
    </li>
  </ul>
  <div class="separator"></div>
  <ul class="list">
    <li class="element delete" onclick="eliminar(${id})"">
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="24"
        height="24"
        viewBox="0 0 24 24"
        fill="none"
        stroke="#7e8590"
        stroke-width="2"
        stroke-linecap="round"
        stroke-linejoin="round"
        class="lucide lucide-trash-2"
      >
        <path d="M3 6h18"></path>
        <path d="M19 6v14c0 1-1 2-2 2H7c-1 0-2-1-2-2V6"></path>
        <path d="M8 6V4c0-1 1-2 2-2h4c1 0 2 1 2 2v2"></path>
        <line x1="10" x2="10" y1="11" y2="17"></line>
        <line x1="14" x2="14" y1="11" y2="17"></line>
      </svg>
      <p class="label">Delete</p>
    </li>
  </ul>
  
    </div>`
    }

    return contenido;
}

function AbrirConfig(id) {
    let contenedor = document.getElementById("Options-config");
    let boton = document.querySelector(`[onclick="AbrirConfig(${id})"]`);

    if (!contenedor || !boton) {
        console.error("❌ No se encontró el contenedor o el botón");
        return;
    }

    // Alternar visibilidad
    if (contenedor.style.display === "block") {
        contenedor.style.display = "none";
        return;
    }

    // Obtener posición del botón
    let rect = boton.getBoundingClientRect();

    // Insertar contenido dinámicamente
    contenedor.innerHTML = OpcionesConfig(id);

    // Posicionar el menú al lado del botón
    contenedor.style.position = "absolute";
    contenedor.style.top = `${rect.top + window.scrollY}px`;  // Mantiene la posición con desplazamiento
    contenedor.style.left = `${rect.left - contenedor.offsetWidth - 220}px`;  // Aparece justo al lado derecho con margen de 10px
    contenedor.style.display = "block";  // Mostrarlo
}

function error(titulo = "Oops...") {
    Swal.fire({
        icon: "error",
        title: titulo,
    });
}
