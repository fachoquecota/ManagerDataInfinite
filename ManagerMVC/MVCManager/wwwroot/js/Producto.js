﻿// Función para llenar un combobox
async function fillComboBox(selectId, dataOrApiUrl, selectedValue) {
    let items;

    if (typeof dataOrApiUrl === 'string') {
        const response = await fetch(dataOrApiUrl);
        if (!response.ok) {
            console.error('Error al cargar datos desde la API');
            return;
        }
        const data = await response.json();
        items = data.result;

        // Guardar los datos de ModeloProducto para su uso posterior
        if (selectId === 'idModeloProducto') {
            modelosDescripcion = items;
        }
    } else {
        items = dataOrApiUrl;
    }

    const select = document.getElementById(selectId);
    select.innerHTML = '';
    items.forEach(item => {
        const option = document.createElement('option');
        option.value = item.id;
        option.text = item.descripcion;
        select.appendChild(option);
    });
    select.value = selectedValue;
}


let relacionCategoriaModelo = [];
let modelosDescripcion = [];

async function cargarRelacionCategoriaModelo() {
    const response = await fetch('/Productos/GetModelosDetalle'); // Asegúrate de que la ruta sea correcta
    if (response.ok) {
        const data = await response.json();
        relacionCategoriaModelo = data.result;
    } else {
        console.error('Error al cargar los datos desde el controlador');
    }
}

function validateFields() {
    // Validar Producto
    if (document.getElementById('producto').value.trim() === '') {
        return false;
    }

    //// Validar Marca
    //if (document.getElementById('marca').value.trim() === '') {
    //    return false;
    //}

    // Validar Precio
    if (document.getElementById('precio').value.trim() === '') {
        return false;
    }

    // Validar Cantidad
    if (document.getElementById('cantidad').value.trim() === '') {
        return false;
    }

    // Validar ID Proveedor
    if (document.getElementById('idProveedor').value.trim() === '' || document.getElementById('idProveedor').value.trim() === '0') {
        return false;
    }

    // Validar ID Genero
    if (document.getElementById('idGenero').value.trim() === '' || document.getElementById('idGenero').value.trim() === '0') {
        return false;
    }

    // Validar ID Categoria
    if (document.getElementById('idCategoria').value.trim() === '' || document.getElementById('idCategoria').value.trim() === '0') {
        return false;
    }

    // Validar Imagen
    const nuevaImagen = document.getElementById('nuevaImagen');
    const imagenActual = document.getElementById('imagenActual').src;

    // Verifica si no hay una nueva imagen seleccionada y si la imagen actual es la imagen por defecto ("Croquis de domicilio.png")
    if (nuevaImagen.files.length === 0 && imagenActual.includes('Croquis de domicilio.png')) {
        return false;
    }


    return true;
}

document.addEventListener("DOMContentLoaded", function () {

    cargarRelacionCategoriaModelo();

    document.getElementById('idCategoria').addEventListener('change', function () {
        const idCategoriaSeleccionada = this.value;

        const modelosFiltrados = relacionCategoriaModelo.filter(item =>
            item.idCategoria == idCategoriaSeleccionada).map(item => {
                const modeloEncontrado = modelosDescripcion.find(m => m.id === item.idModeloProducto);
                return modeloEncontrado ? modeloEncontrado : { id: item.idModeloProducto, descripcion: 'Descripción no encontrada' };
            });

        fillComboBox('idModeloProducto', modelosFiltrados, '');
    });


    // Manejo de pestañas
    const tabButtons = document.querySelectorAll('.tab-button');
    const tabContents = document.querySelectorAll('.tab-content');

    tabButtons.forEach(button => {
        button.addEventListener('click', function () {
            const target = this.getAttribute('data-target');

            // Desactivar todos los botones y ocultar todo el contenido
            tabButtons.forEach(btn => btn.classList.remove('active'));
            tabContents.forEach(content => content.classList.remove('active'));

            // Activar el botón clickeado y mostrar su contenido
            this.classList.add('active');
            document.getElementById(target).classList.add('active');
        });
    });

    document.querySelectorAll('.open-modal').forEach(button => {
        button.addEventListener('click', async function (e) {
            const id = e.target.getAttribute('data-id');
            // Mostrar el spinner de carga
            const loadingSpinner = document.getElementById('loadingSpinner');
            loadingSpinner.classList.remove('hidden');

            // Actualizar el título del modal con el ID del producto
            document.getElementById('modalTitleId').innerText = id;

            // Hacer una llamada AJAX para obtener los datos del producto
            const response = await fetch(`/Productos/GetProductoById?idProducto=${id}`);

            if (response.ok) {
                const data = await response.json();
                const producto = data.result[0];

                // Llenar los campos del formulario en el modal
                localStorage.setItem('currentIdProducto', producto.idProducto);

                document.getElementById('producto').value = producto.producto;
                //document.getElementById('marca').value = producto.marca;
                document.getElementById('costo').value = producto.costo;
                document.getElementById('precio').value = producto.precio;
                document.getElementById('cantidad').value = producto.cantidad;

                document.getElementById('activo').checked = producto.activo;

                // Actualizar la imagen actual
                const imagenActual = document.getElementById('imagenActual');
                imagenActual.src = producto.imagenCarpeta + producto.imagenNombre;
                console.log(imagenActual.src);

                // Llenar los combobox
                await fillComboBox('idProveedor', '/Productos/GetProveedores', producto.idProveedor);
                await fillComboBox('idGenero', '/Productos/GetGeneros', producto.idGenero);
                await fillComboBox('idCategoria', '/Productos/GetCategorias', producto.idCategoria);
                await fillComboBox('idModeloProducto', '/Productos/GetModelos', producto.idModeloProducto);
                await fillComboBox('idCalidad', '/Productos/GetCalidades', producto.idCalidad);
                await fillComboBox('idMarca', '/Productos/GetMarcas', producto.idMarca);



                // Llenar la tabla de tallas
                await fillSizeTable(id);
                await fillTagTable(id);
                await fillColorTable(id);


                // Mostrar el modal
                document.getElementById('editModal').classList.remove('hidden');
                loadingSpinner.classList.add('hidden');
            }
        });
    });

    // Cerrar el modal
    document.getElementById('closeModal').addEventListener('click', function () {
        document.getElementById('editModal').classList.add('hidden');
    });

    // Guardar cambios


    console.log("Estoy a punto de adjuntar el evento");
    document.getElementById('saveChanges').addEventListener('click', async function () {
        console.log("Botón de Guardar Cambios presionado");
        event.preventDefault(); // Evitar la ejecución predeterminada del botón

        // Si los campos no son válidos, muestra un mensaje y termina la ejecución
        if (!validateFields()) {
            alert('Por favor, completa todos los campos requeridos antes de guardar.');
            return;
        }
        let endpoint;
        if ($("#mode").val() === "add") {
            endpoint = '/Productos/CreateProducto';  // Endpoint para crear producto
        } else {
            endpoint = '/Productos/UpdateProducto';  // Endpoint para actualizar producto
        }

        //const idProducto = parseInt(document.getElementById('modalTitleId').innerText, 10);
        const idProducto = localStorage.getItem('currentIdProducto');
        console.log(idProducto);
        const formData = new FormData(document.getElementById('editForm'));
        formData.append('IdProducto', idProducto);
        formData.set('activo', document.getElementById('activo').checked ? 'true' : 'false');

        // Nueva sección para recopilar datos de la tabla
        const toUpdate = [];
        const toCreate = [];
        const rows = document.querySelectorAll('#sizeTableBody tr');

        rows.forEach(row => {
            const idSizeDetalle = row.querySelector('td:nth-child(1)').innerText;
            const idSize = row.querySelector('td:nth-child(2) select').value;
            const activo = row.querySelector('td:nth-child(3) input[type="checkbox"]').checked;

            const record = {
                idSizeDetalle,
                idSize,
                activo
            };

            if (idSizeDetalle && idSizeDetalle !== 'Nuevo!') {
                toUpdate.push(record);
            } else {
                toCreate.push(record);
            }
        });

        const sizeUpdateorDelete = {
            toUpdate,
            toCreate,
            deletedItems
        };

        formData.append('SizeUpdateorDelete', JSON.stringify(sizeUpdateorDelete));

        console.log("localstorege desde producto.js ", localStorage.getItem('selectedImages'));
        const selectedImages = JSON.parse(localStorage.getItem('selectedImages') || '[]');

        console.log("selectedImages ",selectedImages);

        try {
            selectedImages.forEach((imageObject, index) => {
                console.log("Base64 String: ", imageObject.base64);  // Para depuración
                const byteCharacters = atob(imageObject.base64);
                const byteNumbers = new Array(byteCharacters.length);
                for (let i = 0; i < byteCharacters.length; i++) {
                    byteNumbers[i] = byteCharacters.charCodeAt(i);
                }
                const byteArray = new Uint8Array(byteNumbers);
                const blob = new Blob([byteArray], { type: imageObject.type });
                formData.append(`newImages[${index}]`, blob, `image${index}.${imageObject.type.split('/')[1]}`);
            });
        } catch (e) {
            console.error("Error al decodificar la cadena Base64: ", e);
        }

        console.log("formData ", formData);
        const colorData = getUpdatedColors();
        formData.append('ColorUpdateInfoJson', JSON.stringify(colorData));

        const tagData = getUpdatedTags();

        formData.append('TagUpdateInfo', JSON.stringify(tagData));


        console.log(formData);

        console.log("realizando post");

        
        const response = await fetch(endpoint, {
            method: 'POST',
            body: formData
        });

        if (response.ok) {
            const data = await response.json();
            if (data.success) {
                localStorage.setItem('showNotification', 'true');
                window.location.reload();
            }
            else {
                // Manejar errores
                alert('No se pudo actualizar el producto.');
            }
        } else {
            // Manejar errores
            alert('Ocurrió un error al actualizar el producto.');
        }
    });

    document.addEventListener("DOMContentLoaded", function () {
        const showNotification = localStorage.getItem('showNotification');
        if (showNotification === 'true') {
            const productoGuardado = document.getElementById('productoGuardado');
            productoGuardado.innerText = document.getElementById('producto').value;
            const notification = document.getElementById('notification');
            notification.classList.remove('hidden');

            setTimeout(() => {
                notification.classList.add('hidden');
            }, 5000); // La notificación se ocultará después de 5 segundos

            // Limpiar el indicador en localStorage para no mostrar la notificación en futuras recargas
            localStorage.removeItem('showNotification');
        }
    });


    document.getElementById('nuevaImagen').addEventListener('change', function () {
        const file = this.files[0];
        if (file) {
            const reader = new FileReader();
            reader.addEventListener('load', function () {
                document.getElementById('imagenActual').setAttribute('src', this.result);
            });
            reader.readAsDataURL(file);
        }
    });

    // Escuchar clics en los botones "Eliminar"
    document.querySelectorAll('.delete-product').forEach(button => {
        button.addEventListener('click', function (e) {
            const id = e.target.getAttribute('data-id');
            // Mostrar el modal de confirmación de eliminación
            document.getElementById('deleteModal').classList.remove('hidden');
            // Almacenar el ID del producto en el modal para su uso posterior
            document.getElementById('deleteModal').setAttribute('data-id', id);
        });
    });

    // Escuchar clics en el botón "Confirmar" del modal de eliminación
    document.getElementById('confirmDelete').addEventListener('click', async function () {
        const idProducto = document.getElementById('deleteModal').getAttribute('data-id');
        const response = await fetch(`/Productos/DeleteProducto?idProducto=${idProducto}`, {
            method: 'DELETE'
        });

        if (response.ok) {
            const data = await response.json();
            if (data.success) {
                // Cerrar el modal de eliminación
                document.getElementById('deleteModal').classList.add('hidden');
                // Aquí puedes recargar la página o eliminar la fila de la tabla, etc.
                location.reload();
            } else {
                alert('No se pudo eliminar el producto.');
            }
        } else {
            alert('Ocurrió un error al eliminar el producto.');
        }
    });

    // Escuchar clics en el botón "Cancelar" del modal de eliminación
    document.getElementById('closeModalConfirm').addEventListener('click', function () {
        document.getElementById('deleteModal').classList.add('hidden');
    });
});