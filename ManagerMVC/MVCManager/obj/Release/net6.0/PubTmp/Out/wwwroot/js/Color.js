// Función para llenar la tabla de Colores
const originalColorState = {};


async function fillColorTable(idProducto) {
    idProductoActual = idProducto; // <- Asignación aquí

    const response = await fetch(`/Productos/GetCrudColorDetalleById?idProducto=${idProducto}`);
    if (response.ok) {
        const data = await response.json();
        const items = data.result;
        const tbody = document.getElementById('colorTableBody');
        tbody.innerHTML = ''; // Limpiar la tabla

        items.forEach(item => {
            addNewColorRow(item.idColorDetalle, item.idColor, item.codigo, item.activo);
        });
        items.forEach(item => {
            // Almacenar el estado original de cada color
            originalColorState[item.idColorDetalle] = {
                idColor: item.idColor,
                isActive: item.activo
            };
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
let colorsToDelete = [];
let colorsToUpdate = [];
let colorsToAdd = [];
let idProductoActual;


// Función para obtener el estado actualizado de los colores
function getUpdatedColors() {
    // Define las listas para guardar colores agregados, actualizados y eliminados
    let colorsToAdd = [];
    let colorsToUpdate = [];
    let colorsToDelete = []; // Asegúrate de que estés manejando también esta lista en otro lugar

    let colorRows = document.querySelectorAll('#colorTableBody tr');

    colorRows.forEach(row => {
        let idColorDetalle = row.querySelector('td:nth-child(1)').innerText.trim();
        let idColor = row.querySelector('td:nth-child(2) select').value;
        let checkboxElement = row.querySelector('td:nth-child(4) input[type="checkbox"]');
        let isActive = checkboxElement && checkboxElement.checked;

        // Log para depuración
        console.log('ID:', idColorDetalle, 'Checkbox Value:', isActive);

        // Validación: Si no se ha seleccionado un color, muestra una alerta y detiene la ejecución
        if (!idColor) {
            alert('Debe seleccionar un color antes de guardar.');
            throw new Error('Color no seleccionado.');
        }

        // Si el color es nuevo, agrégalo a la lista de colores a añadir
        if (idColorDetalle === 'Nuevo!') {
            colorsToAdd.push({
                idColorDetalle: '0', // Puede ser 0 o '0', dependiendo de cómo manejes los ID en tu backend
                idProducto: idProductoActual, // Asegúrate de que esta variable tiene el valor correcto
                idColor: idColor,
                codigo: "", // Si no lo estás usando, puedes ponerlo en blanco o eliminarlo
                isActive: isActive
            });
        } else {
            // Si no es un color nuevo, agrégalo a la lista de colores a actualizar
            colorsToUpdate.push({ idColorDetalle, idColor, isActive });
        }
    });

    // Retorna un objeto con las listas de colores
    return {
        colorsToUpdate,
        colorsToAdd,
        colorsToDelete
    };
}


// Hacer que la función getUpdatedColors esté disponible globalmente
window.getUpdatedColors = getUpdatedColors;


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
    console.log('Checkbox initial value for ID:', idColorDetalle, 'is:', checkbox.checked);


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
        if (idColorDetalle && idColorDetalle !== 'Nuevo!') {
            colorsToDelete.push(idColorDetalle);
        }
        row.remove();
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
