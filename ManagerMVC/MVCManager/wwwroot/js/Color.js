// Función para llenar la tabla de Colores
async function fillColorTable(idProducto) {
    const response = await fetch(`/Productos/GetCrudColorDetalleById?idProducto=${idProducto}`);
    if (response.ok) {
        const data = await response.json();
        const items = data.result;
        const tbody = document.getElementById('colorTableBody');
        tbody.innerHTML = ''; // Limpiar la tabla

        items.forEach(item => {
            addNewColorRow(item.idColorDetalle, item.idColor, item.codigo, item.activo);
        });

        // Llenar el combobox
        const colorSelects = document.querySelectorAll('.colorSelect');
        await fillComboBoxForAll(colorSelects, '/Productos/GetColor_CrudCB');

        // Establecer el valor del select basado en el atributo data-id
        colorSelects.forEach(select => {
            const idColor = select.getAttribute('data-id');
            if (idColor) {
                select.value = idColor;
            }
        });
    }
}





// Función para agregar una nueva fila a la tabla de Colores
function addNewColorRow(idColorDetalle, idColor, descripcion, isActive) {
    const tbody = document.getElementById('colorTableBody');
    const row = document.createElement('tr');

    // ID ColorDetalle
    const cellIdColorDetalle = document.createElement('td');
    cellIdColorDetalle.innerText = idColorDetalle;
    row.appendChild(cellIdColorDetalle);

    // ID Color (esto será un combobox)
    const cellIdColor = document.createElement('td');
    const select = document.createElement('select');
    select.className = 'colorSelect';
    select.setAttribute('data-id', idColor);  // Añadir el idColor como un atributo data-id
    cellIdColor.appendChild(select);
    row.appendChild(cellIdColor);

    // Descripción
    const cellDescripcion = document.createElement('td');
    cellDescripcion.innerText = descripcion;
    row.appendChild(cellDescripcion);

    // Activo (interruptor deslizable)
    const cellActivo = document.createElement('td');
    const switchContainer = document.createElement('div');
    switchContainer.className = 'switch-container';

    const switchLabel = document.createElement('label');
    switchLabel.className = 'switch';

    const checkbox = document.createElement('input');
    checkbox.type = 'checkbox';
    checkbox.className = 'switch-input';
    checkbox.checked = isActive;

    const slider = document.createElement('span');
    slider.className = 'slider';

    switchLabel.appendChild(checkbox);
    switchLabel.appendChild(slider);
    switchContainer.appendChild(switchLabel);
    cellActivo.appendChild(switchContainer);
    // Acciones
    const cellActions = document.createElement('td');
    cellActions.className = 'text-center';
    const deleteButton = document.createElement('button');
    deleteButton.innerText = 'Eliminar';
    deleteButton.className = 'bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded';
    deleteButton.addEventListener('click', function () {
        const idColorDetalle = item.idColorDetalle;
        if (idColorDetalle) {
            // Aquí puedes añadir lógica para manejar los elementos eliminados, como almacenarlos en un array
            // deletedItems.push(idColorDetalle);
        }
        row.remove();  // Esto eliminará la fila de la tabla
    });
    cellActions.appendChild(deleteButton);
    row.appendChild(cellActivo);
    row.appendChild(cellActions);


    tbody.appendChild(row);
}

// Función para llenar todos los comboboxes
async function fillComboBoxForAll(selectElements, url) {
    const response = await fetch(url);
    console.log("datos de response combo ", response);
    if (response.ok) {
        const data = await response.json();
        const items = data.result;

        selectElements.forEach(select => {
            // Limpiar opciones existentes
            select.innerHTML = '';

            // Agregar nuevas opciones
            items.forEach(item => {
                const option = document.createElement('option');
                option.value = item.id;
                option.text = item.descripcion;
                select.appendChild(option);
            });
        });
    }
}

// Escuchar clics en el botón "Agregar"
document.addEventListener("DOMContentLoaded", function () {
    document.getElementById('addNewColor').addEventListener('click', function () {
        const newColor = document.getElementById('newColor').value;
        if (newColor) {
            addNewColorRow('Nuevo!', null, newColor, true);  // Añadido null para idColor
        }
    });
});
