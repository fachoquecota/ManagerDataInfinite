document.addEventListener("DOMContentLoaded", function () {
    // Escuchar el evento 'change' en el input de tipo 'file'
    document.getElementById('newImage').addEventListener('change', function () {
        // Obtener el archivo seleccionado
        const file = this.files[0];

        // Verificar si se seleccionó un archivo
        if (file) {
            // Crear un FileReader para leer el archivo
            const reader = new FileReader();

            // Escuchar el evento 'load' del FileReader
            reader.addEventListener('load', function () {
                // Crear un nuevo elemento de imagen
                const img = document.createElement('img');
                img.src = this.result;
                img.alt = 'Imagen previsualizada';
                img.className = 'w-full h-48 object-cover rounded';

                // Crear un contenedor para la imagen
                const imgContainer = document.createElement('div');
                imgContainer.className = 'image-container relative';

                // Crear un botón para eliminar la imagen
                const deleteButton = document.createElement('button');
                deleteButton.innerHTML = 'x';
                deleteButton.className = 'absolute top-0 right-0 bg-blue-500 hover:bg-blue-700 text-white font-bold rounded-full flex items-center justify-center shadow-md';
                deleteButton.style.width = '24px';  // Establecer el ancho en píxeles
                deleteButton.style.height = '24px';  // Establecer el alto en píxeles
                deleteButton.style.lineHeight = '24px';  // Ajustar la alineación vertical del texto
                deleteButton.addEventListener('click', function () {
                    imgContainer.remove();
                });




                // Agregar la imagen y el botón al contenedor
                imgContainer.appendChild(img);
                imgContainer.appendChild(deleteButton);

                // Agregar el contenedor al contenedor de previsualización
                document.getElementById('previewContainer').appendChild(imgContainer);
            });

            // Leer el archivo como URL de datos
            reader.readAsDataURL(file);
        }
    });



});
