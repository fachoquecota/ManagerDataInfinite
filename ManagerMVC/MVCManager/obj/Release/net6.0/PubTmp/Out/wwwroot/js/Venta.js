function actualizarResumenProductos(productos) {
    let resumen = {};

    productos.forEach(producto => {
        let clave = `${producto.categoria}-${producto.calidad}-${producto.talla}`;
        console.log(clave);
        if (!resumen[clave]) {
            resumen[clave] = { categoria: producto.categoria, calidad: producto.calidad, talla: producto.talla, cantidad: 0 };
        }
        resumen[clave].cantidad += producto.cantidad;
    });

    const tbody = document.querySelector("#resumenProductos tbody");
    tbody.innerHTML = '';

    Object.values(resumen).forEach(item => {
        const fila = `<tr>
                <td>${item.categoria}</td>
                <td>${item.calidad}</td>
                <td>${item.talla}</td>
                <td>${item.cantidad}</td>
            </tr>`;
        tbody.innerHTML += fila;
    });
}


// Función para recalcular el total de un producto
function recalculateProductTotal(row) {
    const quantity = parseFloat(row.querySelector('.quantity-input').value);
    const price = parseFloat(row.querySelector('.price-input').value);
    const totalCell = row.querySelector('.total-cell');
    totalCell.textContent = (quantity * price).toFixed(2);
}

// Función para recalcular la "Venta Total"
function recalculateSaleTotal() {
    console.log("iniciando recalculateSaleTotal");
    const table = document.querySelector("#tablaProductosSeleccionados");
    let totalColumnIndex = -1;

    // Encontrar el índice de la columna "Total"
    table.querySelectorAll('thead th').forEach((th, index) => {
        if (th.textContent.trim() === 'Total') {
            totalColumnIndex = index;
        }
    });

    // Si no encontramos la columna, terminamos la función
    if (totalColumnIndex === -1) return;

    // Calcular el total sumando los valores de la columna "Total" en cada fila
    let total = 0;
    table.querySelectorAll('tbody tr').forEach(row => {
        const totalCell = row.querySelector(`td:nth-child(${totalColumnIndex + 1})`);
        if (totalCell) {
            total += parseFloat(totalCell.textContent.trim() || 0);
        }
    });

    // Establecer el valor del total en el campo correspondiente
    document.getElementById("totalSale").value = total.toFixed(2);
}


document.addEventListener("DOMContentLoaded", () => {

    /* Función para agregar productos a "Detalles de venta" */
    function agregarProductoADetalles(producto) {
        const tablaProductosSeleccionados = document.getElementById("tablaProductosSeleccionados");
        const idProducto = producto.getAttribute("data-idproducto");
        const existingRow = tablaProductosSeleccionados.querySelector(`[data-idproducto="${idProducto}"]`);

        // Obtener la descripción de la categoría desde el atributo del producto
        const categoriaDescripcion = producto.getAttribute("data-categoriaDescripcion");

        if (existingRow) {
            const quantityInput = existingRow.querySelector(".quantity-input");
            const maxQuantity = parseInt(producto.getAttribute("data-cantidad"));
            if (parseInt(quantityInput.value) < maxQuantity) {
                quantityInput.value = parseInt(quantityInput.value) + 1;
                recalculateProductTotal(existingRow);
                recalculateSaleTotal();
            } else {
                alert("No puedes agregar más unidades de este producto. Has alcanzado la cantidad máxima permitida.");
            }
        } else {
            const nombreProducto = producto.querySelector("h3").textContent;
            const precioProductoTexto = producto.querySelector(".precio-producto").textContent;
            const cantidadMaxima = parseInt(producto.getAttribute("data-cantidad"));

            const imgSrc = producto.querySelector("img").src;

            const newRow = document.createElement("tr");
            newRow.setAttribute("data-idproducto", idProducto);
            newRow.innerHTML = `
            <td class="px-2 py-2"><img src="${imgSrc}" alt="${nombreProducto}" class="w-12 h-12 object-cover rounded-md"></td>
            <td class="px-2 py-2">${nombreProducto}</td>

            

            <td class="px-2 py-2">
                <input type="number" value="1" min="1" max="${cantidadMaxima}" data-max="${cantidadMaxima}" class="w-16 border rounded p-1 quantity-input">
            </td>
            <td class="px-2 py-2">
                <input type="number" value="${parseFloat(precioProductoTexto).toFixed(2)}" class="w-20 border rounded p-1 price-input precio-producto" readonly>
            </td>
            <td class="px-2 py-2 total-cell">${parseFloat(precioProductoTexto).toFixed(2)}</td>
            <td class="px-2 py-2"><button class="text-red-500 hover:text-red-700">Eliminar</button></td>
        `;

            newRow.querySelector('.quantity-input').addEventListener('input', function (e) {
                let max = parseInt(this.getAttribute('data-max'));
                if (parseInt(this.value) > max) {
                    alert("La cantidad ingresada supera la cantidad disponible.");
                    this.value = max;
                } else if (this.value < 1) {
                    alert("La cantidad ingresada es menor que la cantidad mínima permitida.");
                    this.value = 1;
                }
                recalculateProductTotal(newRow);
                recalculateSaleTotal();
            });

            tablaProductosSeleccionados.querySelector("tbody").appendChild(newRow);
        }
    }

    /* Evento para el botón de agregar producto */
    document.getElementById("productosGrid").addEventListener("click", (e) => {
        if (e.target.tagName === "BUTTON") {
            const producto = e.target.closest(".producto");
            agregarProductoADetalles(producto);
        }
    });
    /* Funciones */
    function filtrarProductosPorModelo(idModelo) {
        const productos = document.querySelectorAll(".producto");
        productos.forEach(producto => {
            if (producto.getAttribute("data-idmodelo") == idModelo) {
                producto.style.display = "block";
            } else {
                producto.style.display = "none";
            }
        });
    }

    function construirResumenCategorias(productosSeleccionados) {
        let resumenCategorias = {};

        productosSeleccionados.forEach(producto => {
            const categoria = producto.categoria;
            const cantidad = producto.cantidad;

            console.log("construirResumenCategorias",categoria);

            if (!resumenCategorias[categoria]) {
                resumenCategorias[categoria] = 0;
            }
            resumenCategorias[categoria] += cantidad;
        });

        // Generar tabla de resumen
        const tbodyResumen = document.querySelector("#resumenProductos tbody");
        tbodyResumen.innerHTML = '';

        for (const categoria in resumenCategorias) {
            const fila = `<tr>
            <td>${categoria}</td>
            <td>${resumenCategorias[categoria]}</td>
        </tr>`;
            tbodyResumen.innerHTML += fila;
        }
    }


    /* Evento para el botón de confirmación de venta */
    document.getElementById("confirmSaleButton").addEventListener("click", function (event) {
        event.preventDefault();

        let productosSeleccionados = [];
        const rows = document.querySelectorAll("#tablaProductosSeleccionados tbody tr");

        rows.forEach(row => {
            // Obtener la descripción de la categoría directamente de la fila
            let descripcionCategoria = row.cells[2].textContent;
            let calidad = row.getAttribute("data-calidad");
            let talla = row.getAttribute("data-talla");
            let cantidad = parseInt(row.querySelector(".quantity-input").value, 10);

            console.log("confirmSaleButton",descripcionCategoria);
            productosSeleccionados.push({
                categoria: descripcionCategoria,
                calidad: calidad,
                talla: talla,
                cantidad: cantidad
            });
        });

        construirResumenCategorias(productosSeleccionados);


        // Antes de clonar la tabla, calcula el total de venta.
        recalculateSaleTotal();
        const totalVenta = parseFloat(document.getElementById("totalSale").value);

        // Copia el contenido de la tabla de productos seleccionados
        const selectedProductsTable = document.getElementById("tablaProductosSeleccionados").cloneNode(true);

        // Elimina la columna de "Acción" de la tabla copiada
        selectedProductsTable.querySelectorAll("thead tr th:last-child, tbody tr td:last-child").forEach(cell => cell.remove());

        // Desactiva los campos de entrada de la tabla copiada
        selectedProductsTable.querySelectorAll("input").forEach(input => input.disabled = true);

        // Pega el contenido en el modal
        const modalTableContent = document.getElementById("modalTableContent");
        modalTableContent.innerHTML = '';
        modalTableContent.appendChild(selectedProductsTable);

        // Usa el total calculado para establecer el valor en el modal.
        document.getElementById("totalSale").value = totalVenta.toFixed(2);

        // Muestra el modal
        document.getElementById("confirmSaleModal").classList.remove("hidden");

        // Evento para cerrar el modal al hacer clic en "Cancelar"
        document.getElementById("closeModal").addEventListener("click", function () {
            document.getElementById("confirmSaleModal").classList.add("hidden");
        });




        // Evento para "Confirmar"
        document.getElementById("confirmFinalSale").addEventListener("click", function (event) {
            event.preventDefault();

            const rows = document.getElementById("tablaProductosSeleccionados").querySelectorAll("tbody tr");

            const dataToSend = {
                detallesVenta: [],
                totalDefinido: parseFloat(document.getElementById("totalSale").value),
                idCliente: parseFloat(document.getElementById("clienteCombobox").value),
                idTipoPago: parseFloat(document.getElementById("tipoVenta").value)

            };
            console.log(dataToSend);
            rows.forEach((row, index) => {
                const idProducto = parseInt(row.getAttribute("data-idproducto"), 10);
                const cantidad = parseInt(row.querySelector(".quantity-input").value, 10);
                const precio = parseFloat(row.querySelector(".price-input").value);
                //const total = parseFloat(row.querySelector(".total-cell").textContent);

                dataToSend.detallesVenta.push({
                    idProducto: idProducto,
                    cantidad: cantidad,
                    precioUnitario: precio
                });
            });

            fetch('/Venta/GuardarVenta', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(dataToSend)
            })
                .then(response => {
                    if (response.ok) {
                        alert("Venta realizada exitosamente");
                        location.reload();
                    } else {
                        response.text().then(text => {
                            alert("Hubo un error al agregar la calidad: " + text);
                        });
                    }
                });
        });
    });

    /* Llenar el dropdown de tipos de venta al cargar la página */
    fetch("/Venta/GetTiposVenta")
        .then(response => response.json())
        .then(data => {
            const tipoVentaSelect = document.getElementById("tipoVenta");
            data.forEach(item => {
                const option = document.createElement("option");
                option.value = item.id;
                option.textContent = item.descripcion;
                tipoVentaSelect.appendChild(option);
            });
        });

    // Manejador de eventos para el botón "Eliminar"
    tablaProductosSeleccionados.addEventListener("click", function (e) {
        if (e.target && e.target.matches("button.text-red-500")) {
            const filaProducto = e.target.closest("tr");
            filaProducto.remove();  // Elimina la fila

            // Si tienes un contador, disminuye el contador en 1
            contadorProductos.textContent = count;
        }
    });
});