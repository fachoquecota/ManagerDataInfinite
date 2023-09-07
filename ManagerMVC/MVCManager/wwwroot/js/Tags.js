document.addEventListener("DOMContentLoaded", function () {
    // Escuchar clics en el botón "Agregar"
    document.getElementById('addNewTag').addEventListener('click', function () {
        const newTag = document.getElementById('newTag').value;
        if (newTag) {
            addNewTagRow(newTag);
        }
    });
});

// Función para agregar una nueva fila a la tabla de Tags
function addNewTagRow(newTag) {
    const tbody = document.getElementById('tagTableBody');
    const row = document.createElement('tr');

    // ID Tag (será "Nuevo!" para nuevos registros)
    const cellIdTag = document.createElement('td');
    cellIdTag.innerText = 'Nuevo!';
    cellIdTag.className = 'text-center';
    row.appendChild(cellIdTag);

    // Descripción
    const cellDescription = document.createElement('td');
    cellDescription.innerText = newTag;
    cellDescription.className = 'text-center';
    row.appendChild(cellDescription);

    // Activo (usando el mismo estilo de checkbox que en Size)
    const cellActivo = document.createElement('td');
    const switchContainer = document.createElement('div');
    switchContainer.className = 'w-1/2 px-2 mb-4 switch-container text-center';

    const switchLabel = document.createElement('label');
    switchLabel.className = 'switch';

    const checkbox = document.createElement('input');
    checkbox.type = 'checkbox';
    checkbox.className = 'switch-input';

    const slider = document.createElement('span');
    slider.className = 'slider';

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
        row.remove(); // Esto eliminará la fila de la tabla
    });
    cellActions.appendChild(deleteButton);
    row.appendChild(cellActions);

    tbody.appendChild(row);
}
