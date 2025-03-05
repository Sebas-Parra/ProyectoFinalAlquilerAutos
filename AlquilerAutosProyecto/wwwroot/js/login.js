/**
 * Variables
 */
const signupButton = document.getElementById('signup-button'),
    loginButton = document.getElementById('login-button'),
    userForms = document.getElementById('user_options-forms')

/**
 * Add event listener to the "Sign Up" button
 */
signupButton.addEventListener('click', () => {
    userForms.classList.remove('bounceRight')
    userForms.classList.add('bounceLeft')
}, false)

/**
 * Add event listener to the "Login" button
 */
loginButton.addEventListener('click', () => {
    userForms.classList.remove('bounceLeft')
    userForms.classList.add('bounceRight')
}, false)


const signIn = () => {
    let sign = document.querySelector(".sign-in");
    let signOut = document.querySelector(".sign-out");
    sign.classList.add = "hidden";
    signOut.classList.remove = "hidden";
}

const guardarDatos = (event) => {
    event.preventDefault();
    let frmGuardar = document.getElementById("signOut");
    let frm = new FormData(frmGuardar);

    fetchPost("Clientes/guardarCliente", "text", frm, function (data) {
        if (data == "1") {
            userForms.classList.remove('bounceLeft')
            userForms.classList.add('bounceRight')
            exito("Usuario Registrado con exito!");
            LimpiarDatos("signOut");
        }
    })
    return false;
}

let objCliente = {};
function recuperarContrasena() {
    let email = document.getElementById("emailRecuperar").value;

    if (!email) {
        alert("Por favor, ingresa un correo electrónico.");
        return;
    }

    try {
        fetchGet(`Login/RecuperarEmail?email=${email}`, "text", function (data) {
            if (data) {
                objCliente = JSON.parse(data);
                document.getElementById("contrasena").innerHTML = `
                    <label for="password" class="form-label">Nueva contraseña:</label>
                    <input type="password" class="form-control" id="nuevaContrasena" name="nuevaContrasena" required>
                `;

                document.getElementById("recuperarContrasena").remove();
                document.getElementById("btnContrasena").innerHTML = `
                    <button onclick="actualizarContrasena()" id="btn-actualizar" data-bs-dismiss="modal" class="btn">Actualizar contraseña</button>
                `;
            } else {
                LimpiarDatos("formAgregar");
                error("No se ha encontrado el correo");
            }
        });

    } catch (err) {
        console.error("Error al recuperar la contraseña:", err);
        error("Ocurrió un problema al procesar la solicitud.");
    }
}

function actualizarContrasena() {
    let contrasena = document.querySelector("#nuevaContrasena").value;
    objCliente.password = contrasena;
    let frm = new FormData();
    for (const key in objCliente) {
        if (objCliente.hasOwnProperty(key)) {
            frm.append(key, objCliente[key]);
        }
    }
    fetchPost("Clientes/actualizarCliente", "text", frm, function (data) {
        closeRecuperar();
        exito("Contraseña actualizada con exito!");
    });
}

function closeRecuperar() {
    LimpiarDatos("formAgregar");
    document.getElementById("contrasena").innerHTML = "";
    document.getElementById("btnContrasena").innerHTML = ``;
    if (document.getElementById("btn-actualizar")) {
        document.getElementById("btn-actualizar").remove();
    }
    document.getElementById("btnContrasena").innerHTML = `
        <button onclick="recuperarContrasena()" id="recuperarContrasena" class="btn">Recuperar contraseña</button>
    `;
}
