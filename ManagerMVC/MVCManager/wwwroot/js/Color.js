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

    //// ID Color (esto será un combobox)
    //const cellIdColor = document.createElement('td');
    //const select = document.createElement('select');
    //select.className = 'colorSelect';
    //select.setAttribute('data-id', idColor);  // Añadir el idColor como un atributo data-id
    //cellIdColor.appendChild(select);
    //row.appendChild(cellIdColor);

    // ID Color (esto será un combobox)
    const cellIdColor = document.createElement('td');
    const select = document.createElement('select');
    select.className = 'colorSelect';
    select.setAttribute('data-id', idColor);  // Añadir el idColor como un atributo data-id

    // Añadir evento para cambiar el color del cuadro
    select.addEventListener('change', function () {
        const selectedOption = this.options[this.selectedIndex];
        const colorCode = selectedOption.text.split(' ')[0];
        colorBox.style.backgroundColor = `#${colorCode}`;
    });

    cellIdColor.appendChild(select);
    row.appendChild(cellIdColor);


    // Descripción
    const cellDescripcion = document.createElement('td');
    const colorBox = document.createElement('div');
    colorBox.style.width = '20px';
    colorBox.style.height = '20px';
    colorBox.style.backgroundColor = `#${descripcion.split(' ')[0]}`; // Asume que la descripción es "FF0000 - Rojo"
    cellDescripcion.appendChild(colorBox);
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

// Función para llenar todos los comboboxes y mantener los valores seleccionados
async function fillComboBoxForAll(selectElements, url) {
    // Guardar los valores seleccionados actuales
    const selectedValues = Array.from(selectElements).map(select => select.value);

    const response = await fetch(url);
    if (response.ok) {
        const data = await response.json();
        const items = data.result;

        selectElements.forEach((select, index) => {
            // Limpiar opciones existentes
            select.innerHTML = '';

            // Agregar nuevas opciones
            items.forEach(item => {
                const option = document.createElement('option');
                option.value = item.id;
                option.text = item.descripcion;
                select.appendChild(option);
            });

            // Restaurar el valor seleccionado
            select.value = selectedValues[index];
        });
    }
}

// Escuchar clics en el botón "Agregar"
document.addEventListener("DOMContentLoaded", function () {
    // Escuchar clics en el botón para agregar una nueva fila
    document.getElementById('addNewRow').addEventListener('click', async function () {
        addNewColorRow('Nuevo!', null, 'FFFFFF', true);  // Añadido null para idColor y un color por defecto (blanco)

        // Llenar el nuevo combobox y mantener los valores seleccionados
        const colorSelects = document.querySelectorAll('.colorSelect');
        await fillComboBoxForAll(colorSelects, '/Productos/GetColor_CrudCB');
    });
});
