# 🚗 Proyecto Alquiler de Vehículos
Sistema de gestión de alquiler de autos desarrollado en **ASP.NET Core MVC** con arquitectura por capas.

---

## 📌 Descripción
Este proyecto es un sistema de alquiler de vehículos que permite a los usuarios **gestionar reservas, clientes, empleados y pagos**.  
Se implementa una **arquitectura por capas** para mejorar la **escalabilidad y mantenimiento** del código.

---

## ⚙️ Requisitos Previos
Antes de ejecutar el proyecto, asegúrate de contar con las siguientes herramientas instaladas:

- ✅ **Visual Studio 2022** con **.NET Core**.
- ✅ **SQL Server** y **SQL Server Management Studio (SSMS)**.
- ✅ **Bootstrap** o **Materialize CSS** *(para la interfaz de usuario)*.
- ✅ **JavaScript** *(para mejorar la interactividad del sistema)*.

---

## 🚀 Características Principales
✔️ **Registro y autenticación de usuarios**.  
✔️ **Gestión de vehículos, clientes y empleados**.  
✔️ **Administración de reservas y pagos**.  
✔️ **Interfaz responsiva con Bootstrap o Materialize CSS**.  
✔️ **Base de datos en SQL Server** con relaciones entre entidades.

---

## 🏗️ Creación de la Base de Datos
El sistema usa **SQL Server** como base de datos relacional, incluyendo las siguientes tablas principales:

| Tabla      | Descripción |
|------------|------------------------------------------------|
| `Vehículos` | Información de los autos disponibles para alquiler. |
| `Clientes` | Datos personales de los clientes. |
| `Empleados` | Personal encargado de gestionar el sistema. |
| `Reservas` | Registro de las solicitudes de alquiler de vehículos. |
| `Pagos` | Información de los pagos realizados. |
| `Seguros` | Coberturas de seguros asociadas a las reservas. |

---

## 📂 Arquitectura del Proyecto
El sistema sigue una **arquitectura por capas**, organizada de la siguiente manera:

📌 **Capa de Entidad**: Clases que representan las entidades de la base de datos.  
📌 **Capa de Datos**: Manejo del acceso a la base de datos mediante **procedimientos almacenados**.  
📌 **Capa de Negocio**: Implementación de la **lógica del sistema**.  
📌 **Capa de Presentación**: Contiene los **controladores y vistas** para la interacción con el usuario.

---

## 🎛️ Controladores y Formularios
Cada módulo del sistema cuenta con su propio **controlador** en la capa de presentación:

| Módulo      | Funcionalidad |
|------------|------------------------------------------------|
| **Vehículos** | Registro, actualización y eliminación de vehículos. |
| **Clientes** | Administración de la información de los clientes. |
| **Empleados** | Gestión de usuarios internos del sistema. |
| **Reservas** | Manejo de solicitudes de alquiler. |
| **Pagos** | Registro de transacciones y facturación. |
| **Seguros** | Asociación de coberturas a las reservas. |

---

## 🤝 Cómo Contribuir
Si deseas contribuir al proyecto, sigue estos pasos:

1. **Realiza un fork** del repositorio.  
2. **Crea una nueva rama** para tu funcionalidad:  
   ```bash
   git checkout -b nueva-funcionalidad
3. **Realiza los cambios y confírmalos:**
   ```bash
   git commit -m "Agregada nueva funcionalidad"
4. **Sube los cambios a tu fork:**
   ```bash
   git push origin nueva-funcionalidad
5. **Abre un Pull Request en este repositorio.**

## 📜 Licencia
Este proyecto está bajo la Licencia MIT.
Consulta el archivo LICENSE para más detalles.

---

## **📌 2️⃣ Explicación del Formato**
🔹 **Encabezados:** Usa `#` para títulos (`#` H1, `##` H2, `###` H3).  
🔹 **Negritas:** Usa `**texto**`.  
🔹 **Listas:** Usa `-` o `*`.  
🔹 **Tablas:** Usa `|` y `-` para dividir filas y columnas.  
🔹 **Códigos o comandos:** Usa **triple comilla invertida (` ``` `)** para bloques de código.  
🔹 **Emojis:** Se escriben con `:nombre_del_emoji:` (GitHub los soporta nativamente).  

---

## **📌 3️⃣ Cómo Subirlo a GitHub**
1. **Guarda el archivo** como `README.md`.
2. **Abre Git Bash o la terminal y sube los cambios:**
   ```bash
   git add README.md
   git commit -m "Agregado README con formato Markdown"
   git push origin main
