// Función para llenar un combobox
async function fillComboBox(selectId, apiUrl, selectedValue) {
    const response = await fetch(apiUrl);
    if (response.ok) {
        const data = await response.json();
        const items = data.result;
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
}

document.addEventListener("DOMContentLoaded", function () {
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

    // Escuchar clics en los botones "Editar"
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
                document.getElementById('marca').value = producto.marca;
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

                // Llenar la tabla de tallas
                await fillSizeTable(id);

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
    document.getElementById('saveChanges').addEventListener('click', async function () {
        console.log("test");
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

            if (idSizeDetalle && idSizeDetalle !== 'Nuevo !') {
                toUpdate.push(record);
            } else {
                toCreate.push(record);
            }
        });

        const tableData = {
            toUpdate,
            toCreate
        };

        formData.append('TableData', JSON.stringify(tableData));
        formData.append('DeletedItems', JSON.stringify(deletedItems));

        // Fin de la nueva sección

        const response = await fetch('/Productos/UpdateProducto', {
            method: 'POST',
            body: formData
        });

        if (response.ok) {
            const data = await response.json();
            if (data.success) {
                // Mostrar la notificación con el nombre del producto
                document.getElementById('editModal').classList.add('hidden');
                const productoGuardado = document.getElementById('productoGuardado');
                productoGuardado.innerText = document.getElementById('producto').value;
                const notification = document.getElementById('notification');
                notification.classList.remove('hidden');

                // Cerrar la notificación después de unos segundos (opcional)
                setTimeout(() => {
                    notification.classList.add('hidden');
                }, 5000); // La notificación se ocultará después de 5 segundos
            } else {
                // Manejar errores
                alert('No se pudo actualizar el producto.');
            }
        } else {
            // Manejar errores
            alert('Ocurrió un error al actualizar el producto.');
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

    document.getElementById('addSizeRow').addEventListener('click', async function () {
        const tbody = document.getElementById('sizeTableBody');
        const row = document.createElement('tr');

        // ID SizeDetalle (vacío en este caso)
        const cellIdSizeDetalle = document.createElement('td');
        cellIdSizeDetalle.innerText = 'Nuevo !';
        cellIdSizeDetalle.className = 'text-center';
        row.appendChild(cellIdSizeDetalle);

        // ID Size
        const cellIdSize = document.createElement('td');
        const select = document.createElement('select');
        select.className = 'sizeSelect text-center';
        cellIdSize.appendChild(select);
        row.appendChild(cellIdSize);

        // Activo
        const cellActivo = document.createElement('td');
        const switchContainer = document.createElement('div');
        switchContainer.className = 'w-1/2 px-2 mb-4 switch-container mx-auto'; // Añadir mx-auto para centrar el contenedor

        const switchLabel = document.createElement('label');
        switchLabel.className = 'switch';

        const hiddenInput = document.createElement('input');
        hiddenInput.type = 'hidden';
        hiddenInput.name = 'activo';
        hiddenInput.value = 'false';

        const checkbox = document.createElement('input');
        checkbox.type = 'checkbox';
        checkbox.className = 'switch-input';

        const slider = document.createElement('span');
        slider.className = 'slider';

        switchLabel.appendChild(hiddenInput);
        switchLabel.appendChild(checkbox);
        switchLabel.appendChild(slider);

        switchContainer.appendChild(switchLabel);
        cellActivo.appendChild(switchContainer);
        row.appendChild(cellActivo);

        // Acciones (botón Quitar para registros nuevos)
        const cellActions = document.createElement('td');
        cellActions.className = 'text-center'; // Centrar el botón en la celda

        const removeButton = document.createElement('button');
        removeButton.innerText = 'Eliminar';
        removeButton.className = 'bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded';

        // Evento para eliminar la fila
        removeButton.addEventListener('click', function () {
            row.remove();
        });

        cellActions.appendChild(removeButton);
        row.appendChild(cellActions);

        console.log("A punto de agregar una nueva fila a la tabla");
        console.log("Contenido de la fila:", row.innerHTML);
        tbody.appendChild(row);

        // Llenar el combobox
        const sizeSelects = document.querySelectorAll('.sizeSelect');
        await fillComboBoxForAll(sizeSelects, '/Productos/GetSize_CrudCB');
    });



});
let deletedItems = [];

async function fillSizeTable(idProducto) {
    const response = await fetch(`/Productos/GetCrudSizeDetalleById?idProducto=${idProducto}`);
    if (response.ok) {
        const data = await response.json();
        const items = data.result;
        const tbody = document.getElementById('sizeTableBody');
        tbody.innerHTML = ''; // Limpiar la tabla

        items.forEach(item => {
            const row = document.createElement('tr');

            // ID SizeDetalle
            const cellIdSizeDetalle = document.createElement('td');
            cellIdSizeDetalle.innerText = item.idSizeDetalle;
            cellIdSizeDetalle.className = 'text-center';
            row.appendChild(cellIdSizeDetalle);

            // ID Size
            const cellIdSize = document.createElement('td');
            const select = document.createElement('select');
            select.className = 'sizeSelect text-center';
            select.setAttribute('data-id', item.idSize);
            select.value = item.idSize;
            cellIdSize.appendChild(select);
            row.appendChild(cellIdSize);

            // Activo
            const cellActivo = document.createElement('td');
            const switchContainer = document.createElement('div');
            switchContainer.className = 'w-1/2 px-2 mb-4 switch-container text-center';

            const switchLabel = document.createElement('label');
            switchLabel.className = 'switch';

            const hiddenInput = document.createElement('input');
            hiddenInput.type = 'hidden';
            hiddenInput.name = 'activo';
            hiddenInput.value = 'false';

            const checkbox = document.createElement('input');
            checkbox.type = 'checkbox';
            checkbox.className = 'switch-input';
            checkbox.checked = item.activo;

            const slider = document.createElement('span');
            slider.className = 'slider';

            switchLabel.appendChild(hiddenInput);
            switchLabel.appendChild(checkbox);
            switchLabel.appendChild(slider);

            switchContainer.appendChild(switchLabel);
            cellActivo.appendChild(switchContainer);
            row.appendChild(cellActivo);

            // Acciones
            const cellActions = document.createElement('td');
            cellActions.className = 'text-center'; 
            const deleteButton = document.createElement('button');
            deleteButton.innerText = 'Eliminar';
            deleteButton.className = 'bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded';
            deleteButton.addEventListener('click', function () {
                const idSizeDetalle = item.idSizeDetalle;
                if (idSizeDetalle) {
                    deletedItems.push(idSizeDetalle);
                }
                row.remove();  // Esto eliminará la fila de la tabla
            });
            cellActions.appendChild(deleteButton);
            row.appendChild(cellActions);

            tbody.appendChild(row);
        });

        // Llenar todos los comboboxes
        const sizeSelects = document.querySelectorAll('.sizeSelect');
        await fillComboBoxForAll(sizeSelects, '/Productos/GetSize_CrudCB');
    }
}

async function fillComboBoxForAll(selectElements, apiUrl) {
    const response = await fetch(apiUrl);
    if (response.ok) {
        const data = await response.json();
        const items = data.result;

        selectElements.forEach(select => {
            select.innerHTML = '';
            items.forEach(item => {
                const option = document.createElement('option');
                option.value = item.id;
                option.text = item.descripcion;
                select.appendChild(option);
            });
            // Aquí establecemos el valor seleccionado del select
            const selectedValue = select.getAttribute('data-id');
            if (selectedValue) {
                select.value = selectedValue;
            }
        });
    }
}
