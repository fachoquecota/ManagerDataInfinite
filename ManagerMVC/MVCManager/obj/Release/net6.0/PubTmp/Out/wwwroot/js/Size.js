// Función para llenar la tabla de Tamaños
let deletedItems = [];

async function fillSizeTable(idProducto) {
    const response = await fetch(`/Productos/GetCrudSizeDetalleById?idProducto=${idProducto}`);
    if (response.ok) {
        const data = await response.json();
        const items = data.result;
        const tbody = document.getElementById('sizeTableBody');
        tbody.innerHTML = ''; // Limpiar la tabla

        items.forEach(item => {
            addNewSizeRow(item.idSizeDetalle, item.idSize, item.activo);
        });

        // Llenar el combobox
        const sizeSelects = document.querySelectorAll('.sizeSelect');
        await fillComboBoxForAll(sizeSelects, '/Productos/GetSize_CrudCB');

        // Establecer el valor del select basado en el atributo data-id
        sizeSelects.forEach(select => {
            const idSize = select.getAttribute('data-id');
            if (idSize) {
                select.value = idSize;
            }
        });
    }
}

// Función para agregar una nueva fila a la tabla de Tamaños
function addNewSizeRow(idSizeDetalle, idSize, isActive) {
    const tbody = document.getElementById('sizeTableBody');
    const row = document.createElement('tr');

    // ID SizeDetalle
    const cellIdSizeDetalle = document.createElement('td');
    cellIdSizeDetalle.innerText = idSizeDetalle;
    row.appendChild(cellIdSizeDetalle);

    // ID Size (esto será un combobox)
    const cellIdSize = document.createElement('td');
    const select = document.createElement('select');
    select.className = 'sizeSelect';
    select.setAttribute('data-id', idSize);  // Añadir el idSize como un atributo data-id
    cellIdSize.appendChild(select);
    row.appendChild(cellIdSize);

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
        if (idSizeDetalle && idSizeDetalle !== 'Nuevo!') {
            deletedItems.push(idSizeDetalle);
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
    document.getElementById('addSizeRow').addEventListener('click', async function () {
        addNewSizeRow('Nuevo!', null, true);  // Añadido null para idSize y un estado activo por defecto (true)

        // Llenar el nuevo combobox y mantener los valores seleccionados
        const sizeSelects = document.querySelectorAll('.sizeSelect');
        await fillComboBoxForAll(sizeSelects, '/Productos/GetSize_CrudCB');
    });
});
