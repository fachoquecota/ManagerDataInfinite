document.addEventListener("DOMContentLoaded", function () {
    document.getElementById('addNewColor').addEventListener('click', function () {
        const tbody = document.getElementById('colorTableBody');
        const row = document.createElement('tr');

        // ID Color Detalle
        const cellIdColorDetalle = document.createElement('td');
        cellIdColorDetalle.innerText = 'Nuevo !';
        row.appendChild(cellIdColorDetalle);

        // ID Color (Oculto)
        const cellIdColor = document.createElement('td');
        cellIdColor.innerText = '1'; // Este valor será reemplazado por el valor real
        cellIdColor.className = 'hidden';
        row.appendChild(cellIdColor);

        // Código y muestra de color
        const cellCodigo = document.createElement('td');
        const select = document.createElement('select');
        const option = document.createElement('option');
        option.value = 'FF0000';
        option.text = 'FF0000';
        select.appendChild(option);
        cellCodigo.appendChild(select);

        const colorBox = document.createElement('div');
        colorBox.style.backgroundColor = '#FF0000';
        colorBox.className = 'w-4 h-4 inline-block ml-2';
        cellCodigo.appendChild(colorBox);

        row.appendChild(cellCodigo);

        // Descripción
        const cellDescripcion = document.createElement('td');
        const input = document.createElement('input');
        input.type = 'text';
        cellDescripcion.appendChild(input);
        row.appendChild(cellDescripcion);

        // Activo
        const cellActivo = document.createElement('td');
        const checkbox = document.createElement('input');
        checkbox.type = 'checkbox';
        checkbox.checked = true;
        cellActivo.appendChild(checkbox);
        row.appendChild(cellActivo);

        tbody.appendChild(row);
    });
});
