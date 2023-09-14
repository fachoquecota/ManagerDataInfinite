document.addEventListener("DOMContentLoaded", function () {
    const addNewTagButton = document.getElementById('addNewTag');
    if (addNewTagButton) {  // Comprobar si el elemento existe
        addNewTagButton.addEventListener('click', function () {
            const newTag = document.getElementById('newTag').value;
            if (newTag) {
                addNewTagRow('Nuevo!', newTag, true);
            }
        });
    } else {
        console.error("El elemento con ID 'addNewTag' no se encontró en el DOM.");
    }
});


async function fillTagTable(idProducto) {
    try {
        const response = await fetch(`/Productos/GetTagsById?idProducto=${idProducto}`);
        if (response.ok) {
            const data = await response.json();
            if (data.result) {
                const tags = data.result;
                const tbody = document.getElementById('tagTableBody');
                tbody.innerHTML = ''; // Limpiar la tabla
                if (Array.isArray(tags)) {
                    tags.forEach(tag => {
                        const row = document.createElement('tr');

                        // ID Tag
                        const cellIdTag = document.createElement('td');
                        cellIdTag.innerText = tag.idTags;
                        cellIdTag.className = 'text-center';
                        row.appendChild(cellIdTag);

                        // Descripción
                        const cellDescription = document.createElement('td');
                        cellDescription.innerText = tag.descripcion;
                        cellDescription.className = 'text-center';
                        row.appendChild(cellDescription);

                        // Activo
                        const cellActivo = document.createElement('td');
                        const switchContainer = document.createElement('div');
                        switchContainer.className = 'w-1/2 px-2 mb-4 switch-container text-center';

                        const switchLabel = document.createElement('label');
                        switchLabel.className = 'switch';

                        const checkbox = document.createElement('input');
                        checkbox.type = 'checkbox';
                        checkbox.className = 'switch-input';
                        checkbox.checked = tag.activo;

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
                    });
                } else {
                    console.error("tags no es un array");
                }
            } else {
                console.error("La propiedad 'success' es false");
            }
        } else {
            console.error("La respuesta no es OK", response);
        }
    } catch (error) {
        console.log("Se produjo un error", error);
    }
}


// Función para agregar una nueva fila a la tabla de Tags
function addNewTagRow(idTag, description, isActive) {
    const tbody = document.getElementById('tagTableBody');
    const row = document.createElement('tr');

    // ID Tag
    const cellIdTag = document.createElement('td');
    cellIdTag.innerText = idTag;
    cellIdTag.className = 'text-center';
    row.appendChild(cellIdTag);

    // Descripción
    const cellDescription = document.createElement('td');
    cellDescription.innerText = description;
    cellDescription.className = 'text-center';
    row.appendChild(cellDescription);

    // Activo
    const cellActivo = document.createElement('td');
    const switchContainer = document.createElement('div');
    switchContainer.className = 'w-1/2 px-2 mb-4 switch-container text-center';

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
