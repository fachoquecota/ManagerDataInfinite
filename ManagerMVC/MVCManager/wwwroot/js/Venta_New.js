var productosSeleccionados = [];

$(document).ready(function () {
    $('.btnSeleccionarProducto').click(function () {
        var productoCard = $(this).closest('.producto');
        var idProducto = productoCard.data('idproducto');
        var filaExistente = $('#tablaProductosSeleccionados tbody').find('tr[data-idproducto="' + idProducto + '"]');
        var cantidadMax = productoCard.data('cantidad');

        if (filaExistente.length > 0) {
            var inputCantidad = filaExistente.find('.cantidadProducto');
            var cantidadActual = parseInt(inputCantidad.val());
            var nuevaCantidad = cantidadActual + 1;

            if (nuevaCantidad <= cantidadMax) {
                inputCantidad.val(nuevaCantidad);
                var precio = parseFloat(filaExistente.find('.precioProducto').text());
                filaExistente.find('.totalProducto').text((precio * nuevaCantidad).toFixed(2));
                actualizarMatrizProductos(idProducto, nuevaCantidad, precio, tallaProducto);

            } else {
                alert('No hay suficientes unidades disponibles para aumentar la cantidad.');
            }
        } else {
            var imagenSrc = productoCard.find('img').attr('src');
            var nombreProducto = productoCard.find('h3').text().trim();
            var precioProducto = parseFloat(productoCard.find('.precio-producto').text().trim());
            var tallaProducto = productoCard.data('sizedescription');

            var nuevaFila = `
                    <tr data-idproducto="${idProducto}">
                        <td class="px-2 py-2"><img src="${imagenSrc}" alt="${nombreProducto}" class="w-12 h-12 object-cover rounded-md"></td>

                        <td>${nombreProducto}</td>
                        <td><input type="number" value="1" min="1" max="${cantidadMax}" class="cantidadProducto form-input rounded-md shadow-sm w-full text-center"></td>
                        <td>${tallaProducto}</td>
                        <td class="precioProducto">${precioProducto.toFixed(2)}</td>
                        <td class="totalProducto">${precioProducto.toFixed(2)}</td>
                        <td><button class="eliminarProducto text-red-500 hover:text-red-700">Eliminar</button></td>
                    </tr>`;

            $('#tablaProductosSeleccionados tbody').append(nuevaFila);
            actualizarMatrizProductos(idProducto, 1, precioProducto, tallaProducto);

        }

        actualizarContadorProductos();

    });

    $(document).on('click', '.eliminarProducto', function () {
        var idProducto = $(this).closest('tr').data('idproducto');
        $(this).closest('tr').remove();
        eliminarProductoDeMatriz(idProducto);
        actualizarContadorProductos();
    });

    function actualizarMatrizProductos(idProducto, cantidad, precio, talla) {
        var productoExistente = productosSeleccionados.find(p => p.idProducto === idProducto);
        if (productoExistente) {
            productoExistente.cantidad = cantidad;
            productoExistente.precioUnitario = precio; 
            console.log(productoExistente.precioUnitario);
        } else {
            productosSeleccionados.push({ idProducto, cantidad, precio, talla });
        }
    }


    function eliminarProductoDeMatriz(idProducto) {
        productosSeleccionados = productosSeleccionados.filter(p => p.idProducto !== idProducto);
    }

    function actualizarContadorProductos() {
        var contador = 0;
        $('#tablaProductosSeleccionados tbody .cantidadProducto').each(function () {
            contador += parseInt($(this).val());
        });
        $('#contadorProductos').text(contador);
    }

    // Agregar evento 'change' a los campos de cantidad
    $(document).on('change', '.cantidadProducto', function () {
        var cantidadIngresada = parseInt($(this).val());
        var cantidadMax = parseInt($(this).attr('max'));
        var fila = $(this).closest('tr');
        var idProducto = fila.data('idproducto');
        var precio = parseFloat(fila.find('.precioProducto').text());

        if (cantidadIngresada > cantidadMax) {
            alert('La cantidad no puede ser mayor que el stock disponible.');
            $(this).val(cantidadMax);
            cantidadIngresada = cantidadMax;
        }

        // Actualizar el total en la fila
        fila.find('.totalProducto').text((precio * cantidadIngresada).toFixed(2));

        // Actualizar la cantidad en la matriz productosSeleccionados
        actualizarMatrizProductos(idProducto, cantidadIngresada, precio);
    });


    $('#confirmSaleButton').click(function () {
        openModal();
    });

    function actualizarTablaResumenTallas() {
        var resumenTallas = {};
        productosSeleccionados.forEach(function (producto) {
            if (!resumenTallas[producto.talla]) {
                resumenTallas[producto.talla] = 0;
            }
            resumenTallas[producto.talla] += producto.cantidad;
        });

        var filasResumen = '';
        Object.keys(resumenTallas).forEach(function (talla) {
            filasResumen += `<tr><td>${talla}</td><td>${resumenTallas[talla]}</td></tr>`;
        });
        $('#tablaResumenTallas tbody').html(filasResumen);
    }
    // Función para abrir el modal
    function openModal() {
        // Copiar la tabla de productos seleccionados al modal
        var tablaProductosSeleccionados = $('#tablaProductosSeleccionados').clone();
        tablaProductosSeleccionados.find('th:last-child, td:last-child').remove(); // Eliminar columna de acciones
        $('#tablaCopiaProductos').html(tablaProductosSeleccionados.html());

        // Calcular la suma total y mostrarla
        var sumaTotal = productosSeleccionados.reduce(function (acc, producto) {
            return acc + (producto.precio * producto.cantidad);
        }, 0);
        $('#ventaTotal').val(sumaTotal.toFixed(2));

        // Mostrar el modal
        $('#resumenVentaModal').removeClass('hidden');

        // Actualizar la tabla de resumen por talla
        actualizarTablaResumenTallas();
    }



    // Evento click del botón "Confirmar Venta"
    $('#confirmSaleButton').click(function () {
        openModal();
    });

    // Evento click del botón "Cancelar" para cerrar el modal
    $('#closeModal').click(function () {
        $('#resumenVentaModal').addClass('hidden');
    });

    $('#confirmFinalSale').click(function () {
        // Mostrar el spinner
        $('#resumenVentaModal').addClass('hidden');

        $('#loadingSpinner').removeClass('hidden');

        var ventaTotal = $('#ventaTotal').val();
        var idCliente = $('#clienteCombobox').val();
        var idTipoPago = $('#tipoVentaCombobox').val();
        var idTransporteCombobox = $('#transporteCombobox').val(); 
        console.log("productosSeleccionados ", productosSeleccionados);


        $.ajax({
            url: '/Venta/ConfirmarVenta',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ detallesVenta: productosSeleccionados, totalDefinido: ventaTotal, idCliente: idCliente, idTipoPago: idTipoPago, idEmpresa: idTransporteCombobox }),
            success: function (response) {
                // Ocultar el spinner
                $('#loadingSpinner').addClass('hidden');

                if (response.success) {
                    // Mostrar el modal de éxito
                    $('#successModal').removeClass('hidden');
                } else {
                    // Manejar caso de error
                }
            },
            error: function (error) {
                // Ocultar el spinner
                $('#loadingSpinner').addClass('hidden');
                // Manejar error
            }
        });
    });

    function printTicket(printableContent) {
        var printWindow = window.open('', '_blank');
        printWindow.document.open();
        printWindow.document.write(printableContent);
        printWindow.document.close();
        printWindow.onload = function () {
            printWindow.print();
            printWindow.close();
        };
    }



    $('#continueSelling').click(function () {
        // Mostrar el spinner
        $('#loadingSpinner').removeClass('hidden');

        // Recargar la página
        window.location.reload();
    });

    $(window).on('unload', function () {
        // Mantener el spinner visible durante la recarga
        $('#loadingSpinner').removeClass('hidden');
    });


    $('#printReceipt').click(function () {
        $('#successModal').hide();
        printTicket(/* Aquí necesitas pasar el contenido adecuado para imprimir */);
    });

});