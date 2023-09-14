document.addEventListener("DOMContentLoaded", function () {
    // Escuchar el evento 'change' en el input de tipo 'file'
    document.getElementById('newImage').addEventListener('change', function () {
        const file = this.files[0];
        if (file) {
            const reader = new FileReader();
            reader.addEventListener('load', function () {
                const img = document.createElement('img');
                img.src = this.result;
                img.alt = 'Imagen previsualizada';
                img.className = 'w-full h-48 object-cover rounded';

                const imgContainer = document.createElement('div');
                imgContainer.className = 'image-container relative';

                const deleteButton = document.createElement('button');
                deleteButton.innerHTML = 'x';
                deleteButton.className = 'absolute top-0 right-0 bg-blue-500 hover:bg-blue-700 text-white font-bold rounded-full flex items-center justify-center shadow-md';
                deleteButton.style.width = '24px';
                deleteButton.style.height = '24px';
                deleteButton.style.lineHeight = '24px';
                deleteButton.addEventListener('click', function () {
                    imgContainer.remove();
                });

                imgContainer.appendChild(img);
                imgContainer.appendChild(deleteButton);
                document.getElementById('previewContainer').appendChild(imgContainer);
            });
            reader.readAsDataURL(file);
        }
    });

    const openModalButtons = document.querySelectorAll('.open-modal');
    openModalButtons.forEach(button => {
        button.addEventListener('click', function () {
            const idProducto = this.getAttribute('data-id');
            fillImageSection(idProducto);
        });
    });
});

async function fillImageSection(idProducto) {
    const response = await fetch(`/Productos/GetCrudImagenById?idProducto=${idProducto}`);
    if (response.ok) {
        const data = await response.json();
        const items = data.result;
        const container = document.getElementById('previewContainer');
        container.innerHTML = '';

        items.forEach(item => {
            const img = document.createElement('img');
            img.src = `${item.rutaImagen}${item.nombreImagen}`;
            img.alt = 'Imagen del producto';
            img.className = 'w-full h-48 object-cover rounded';

            const imgContainer = document.createElement('div');
            imgContainer.className = 'image-container relative';

            // ID de la imagen en un cuadrado por encima de la imagen
            const idBox = document.createElement('div');
            idBox.className = 'absolute top-0 left-0 bg-blue-500 text-white w-6 h-6 flex items-center justify-center z-10';
            idBox.innerText = item.idImagenes;
            imgContainer.appendChild(idBox);

            // Botón de eliminar imagen
            const deleteButton = document.createElement('button');
            deleteButton.innerHTML = 'x';
            deleteButton.className = 'absolute top-0 right-0 bg-red-500 hover:bg-red-700 text-white font-bold rounded-full flex items-center justify-center shadow-md z-10';
            deleteButton.style.width = '24px';
            deleteButton.style.height = '24px';
            deleteButton.addEventListener('click', function () {
                imgContainer.remove();
            });
            imgContainer.appendChild(deleteButton);

            // Interruptor deslizable para 'activo'
            const switchContainer = document.createElement('div');
            switchContainer.className = 'absolute bottom-0 left-0 p-2';
            const switchLabel = document.createElement('label');
            switchLabel.className = 'switch';
            const checkbox = document.createElement('input');
            checkbox.type = 'checkbox';
            checkbox.className = 'switch-input';
            checkbox.checked = item.activo;
            const slider = document.createElement('span');
            slider.className = 'slider';
            switchLabel.appendChild(checkbox);
            switchLabel.appendChild(slider);
            switchContainer.appendChild(switchLabel);
            imgContainer.appendChild(switchContainer);

            imgContainer.appendChild(img);
            container.appendChild(imgContainer);
        });
    }
}
