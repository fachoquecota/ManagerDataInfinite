document.addEventListener("DOMContentLoaded", function () {
    let selectedImages = [];
    // Función para cargar la imagen en memoria del servidor
    async function loadImageToMemory(imageBase64, type) {
        console.log(imageBase64);
        const response = await fetch('/Productos/LoadImageToMemory', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ image: imageBase64, type: type })
        });

        if (!response.ok) {
            throw new Error('Error al cargar la imagen en memoria del servidor');
        }

        return await response.json();
    }

    // Función para enviar el imageId al controlador para eliminarlo de la memoria/base de datos
    async function deleteImageFromMemory(imageId) {
        try {
            console.log("Iniciando deleteImageFromMemory");
            const response = await fetch('/Productos/DeleteImageFromMemory', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ imageId: imageId })
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || 'Error al eliminar la imagen de la memoria del servidor');
            }

            return await response.json();
        } catch (error) {
            throw error;
        }
    }



    // Escuchar el evento 'change' en el input de tipo 'file'
    document.getElementById('newImage').addEventListener('change', async function () {
        for (let i = 0; i < this.files.length; i++) {
            const file = this.files[i];
            if (file) {
                const reader = new FileReader();
                reader.addEventListener('load', async function () {
                    const img = document.createElement('img');
                    img.src = this.result;
                    img.alt = 'Imagen previsualizada';
                    img.className = 'w-full h-48 object-cover rounded';

                    const imgContainer = document.createElement('div');
                    imgContainer.className = 'image-container relative';

                    const deleteButton = document.createElement('button');
                    deleteButton.innerHTML = 'x';
                    deleteButton.className = 'delete-button-class absolute top-0 right-0 bg-red-500 hover:bg-red-700 text-white font-bold rounded-full flex items-center justify-center shadow-md z-10';
                    deleteButton.style.width = '24px';
                    deleteButton.style.height = '24px';
                    deleteButton.style.lineHeight = '24px';
                    deleteButton.addEventListener('click', function () {
                        imgContainer.remove();
                    });

                    imgContainer.appendChild(img);
                    imgContainer.appendChild(deleteButton);
                    document.getElementById('previewContainer').appendChild(imgContainer);

                    const fileBase64 = this.result.split(',')[1];
                    const result = await loadImageToMemory(fileBase64, file.type);
                    if (result.success) {
                        console.log("Imagen cargada en memoria del servidor con éxito.");
                    } else {
                        console.error("Hubo un error al cargar la imagen en memoria del servidor.");
                    }
                });
                reader.readAsDataURL(file);
            }
        }
    });

    const openModalButtons = document.querySelectorAll('.open-modal');
    openModalButtons.forEach(button => {
        button.addEventListener('click', function () {
            const idProducto = this.getAttribute('data-id');
            fillImageSection(idProducto);
        });
    });

    document.getElementById('previewContainer').addEventListener('click', function (event) {
        if (event.target && event.target.className.includes('delete-button-class')) {
            const imgContainer = event.target.closest('.image-container');
            const idBoxElement = imgContainer.querySelector('.image-id-box');

            // Verificar si idBoxElement existe antes de intentar acceder a sus propiedades
            if (idBoxElement) {
                const imageId = idBoxElement.textContent;
                console.log(imageId);
                deleteImageFromMemory(imageId)
                    .then(result => {
                        if (result.success) {
                            console.log("Imagen eliminada de la memoria del servidor con éxito.");
                        } else {
                            console.error("Hubo un error al eliminar la imagen de la memoria del servidor.");
                        }
                    })
                    .catch(error => {
                        console.error(error);
                    });

                imgContainer.remove();
            } else {
                console.error("No se encontró el elemento con la clase .image-id-box dentro del contenedor de la imagen.");
            }
        }
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

                const idBox = document.createElement('div');
                idBox.className = 'absolute top-0 left-0 bg-blue-500 text-white w-6 h-6 flex items-center justify-center z-10';
                idBox.innerText = item.idImagenes;
                imgContainer.appendChild(idBox);

                const deleteButton = document.createElement('button');
                deleteButton.innerHTML = 'x';
                deleteButton.className = 'delete-button-class absolute top-0 right-0 bg-red-500 hover:bg-red-700 text-white font-bold rounded-full flex items-center justify-center shadow-md z-10';
                deleteButton.style.width = '24px';
                deleteButton.style.height = '24px';
                deleteButton.addEventListener('click', async function () {
                    const imageId = item.idImagenes;
                    try {
                        console.log(imageId);
                        const result = await deleteImageFromMemory(imageId);
                        if (result.success) {
                            console.log("Imagen eliminada de la memoria del servidor con éxito.");
                        } else {
                            console.error("Hubo un error al eliminar la imagen de la memoria del servidor.");
                        }
                    } catch (error) {
                        console.error("Error al llamar a deleteImageFromMemory:", error);
                    }


                    imgContainer.remove();
                });

                imgContainer.appendChild(deleteButton);

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

});


