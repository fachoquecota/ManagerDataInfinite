﻿var productosSeleccionados = [];
var datosVentaGlobal;
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

                var tallaProducto = productoCard.data('sizedescription');
                var calidadProducto = productoCard.data('calidaddescripcion');
                actualizarMatrizProductos(idProducto, nuevaCantidad, precio, tallaProducto, calidadProducto);

            } else {
                alert('No hay suficientes unidades disponibles para aumentar la cantidad.');
            }
        } else {
            var imagenSrc = productoCard.find('img').attr('src');
            var nombreProducto = productoCard.find('h3').text().trim();
            var precioProducto = parseFloat(productoCard.find('.precio-producto').text().trim());
            var tallaProducto = productoCard.data('sizedescription');
            var calidadProducto = productoCard.data('calidaddescripcion'); // Obtener calidad del producto

            var nuevaFila = `
            <tr data-idproducto="${idProducto}">
                <td class="px-2 py-2"><img src="${imagenSrc}" alt="${nombreProducto}" class="w-12 h-12 object-cover rounded-md"></td>
                <td>${nombreProducto}</td>
                <td>${calidadProducto}</td> <!-- Añadir calidad aquí -->
                <td><input type="number" value="1" min="1" max="${cantidadMax}" class="cantidadProducto form-input rounded-md shadow-sm w-full text-center"></td>
                <td>${tallaProducto}</td>
                <td class="precioProducto">${precioProducto.toFixed(2)}</td>
                <td class="totalProducto">${precioProducto.toFixed(2)}</td>
                <td><button class="eliminarProducto text-red-500 hover:text-red-700">Eliminar</button></td>
            </tr>`;

            $('#tablaProductosSeleccionados tbody').append(nuevaFila);
            actualizarMatrizProductos(idProducto, 1, precioProducto, tallaProducto, calidadProducto);

        }

        actualizarContadorProductos();

    });

    $(document).on('click', '.eliminarProducto', function () {
        var idProducto = $(this).closest('tr').data('idproducto');
        $(this).closest('tr').remove();
        eliminarProductoDeMatriz(idProducto);
        actualizarContadorProductos();
    });

    function actualizarMatrizProductos(idProducto, cantidad, precio, talla, calidad) {
        var productoExistente = productosSeleccionados.find(p => p.idProducto === idProducto);
        if (productoExistente) {
            productoExistente.cantidad = cantidad;
            productoExistente.talla = talla;
            productoExistente.calidad = calidad;
            productoExistente.precioUnitario = precio;
        } else {
            productosSeleccionados.push({ idProducto, cantidad, precio, talla, calidad });
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
        $('#cantidadTotal').val(contador); // Actualizar el campo de cantidad total
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
        var talla = fila.data('sizedescription');
        var calidad = fila.data('calidaddescripcion');
        // Actualizar la cantidad en la matriz productosSeleccionados
        actualizarMatrizProductos(idProducto, cantidadIngresada, precio);
    });


    $('#confirmSaleButton').click(function () {
        openModal();
    });
    function actualizarTablaResumenTallas() {
        var resumenCalidad = {};
        productosSeleccionados.forEach(function (producto) {
            var clave = producto.calidad; // Clave única para cada calidad
            if (!resumenCalidad[clave]) {
                resumenCalidad[clave] = { cantidad: 0, calidad: producto.calidad, producto: producto.nombreProducto, talla: producto.talla };
            }
            resumenCalidad[clave].cantidad += producto.cantidad;
        });

        var filasResumen = '';
        Object.keys(resumenCalidad).forEach(function (clave) {
            var item = resumenCalidad[clave];
            filasResumen += `<tr>
                            <td class="productoColumn">${item.producto}</td>
                            <td>${item.calidad}</td>
                            <td>${item.cantidad}</td>
                            <td class="tallaColumn">${item.talla}</td>
                        </tr>`;
        });

        $('#tablaResumenTallas tbody').html(filasResumen);
    }





    // Función para abrir el modal
    function openModal() {
        // Clonar el cuerpo (tbody) de la tabla de productos seleccionados
        var tablaProductosSeleccionados = $('#tablaProductosSeleccionados tbody').clone();

        // Actualizar las cantidades y eliminar la última columna (acciones) de cada fila clonada
        tablaProductosSeleccionados.find('tr').each(function () {
            var idProducto = $(this).data('idproducto');
            var productoEnMatriz = productosSeleccionados.find(p => p.idProducto === idProducto);

            if (productoEnMatriz) {
                // Actualizar la cantidad
                $(this).find('td').eq(3).text(productoEnMatriz.cantidad); // Asume que la cantidad está en la cuarta celda (índice 3)
            }

            // Eliminar la última celda (acciones)
            $(this).find('td:last-child').remove();
        });

        // Preparar la cabecera de la tabla para el modal
        var cabeceraTabla = `
        <thead class="bg-gray-100">
            <tr>
                <th class="px-2 py-2">Imagen</th>
                <th class="px-2 py-2">Producto</th>
                <th class="px-2 py-2">Calidad</th>
                <th class="px-2 py-2">Cantidad</th>
                <th class="px-2 py-2">Talla</th>
                <th class="px-2 py-2">Precio</th>
                <th class="px-2 py-2">Total</th>
            </tr>
        </thead>`;

        // Insertar la cabecera y el cuerpo clonado en la tabla del modal
        $('#tablaCopiaProductos').html(cabeceraTabla).append(tablaProductosSeleccionados);

        // Calcular y mostrar la suma total
        var sumaTotal = productosSeleccionados.reduce(function (acc, producto) {
            return acc + (producto.precio * producto.cantidad);
        }, 0);
        $('#ventaTotal').val(sumaTotal.toFixed(2));

        // Mostrar el modal
        $('#resumenVentaModal').removeClass('hidden');

        // Actualizar la tabla de resumen por talla, si es necesario
        actualizarTablaResumenTallas();
    }


    $('#mostrarProductoCheckbox').change(function () {
        if (this.checked) {
            $('.productoColumn').show();
        } else {
            $('.productoColumn').hide();
        }
    });

    $('#mostrarTallaCheckbox').change(function () {
        if (this.checked) {
            $('.tallaColumn').show();
        } else {
            $('.tallaColumn').hide();
        }
    });






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

        // Obtener los textos seleccionados de los comboboxes
        var departamentoSeleccionado = $('#departamentoCombobox').find('option:selected').text();
        var provinciaSeleccionada = $('#provinciaCombobox').find('option:selected').text();
        var distritoSeleccionado = $('#distritoCombobox').find('option:selected').text();

        var transporteCombobox = $('#transporteCombobox').val();


        var ventaTotal = $('#ventaTotal').val();
        var idCliente = $('#clienteCombobox').val();
        var idTipoPago = $('#tipoVentaCombobox').val();

        // Preparar el objeto de datos para enviar
        var datosEnvio = {
            detallesVenta: productosSeleccionados,
            totalDefinido: ventaTotal,
            idCliente: idCliente,
            idTipoPago: idTipoPago,
            idTransporteCombobox: transporteCombobox,
            ubicacion: {
                departamento: departamentoSeleccionado,
                provincia: provinciaSeleccionada,
                distrito: distritoSeleccionado
            }
        };

        datosVentaGlobal = {
            cliente: $('#clienteCombobox option:selected').text(), // Asumiendo que el texto es el nombre del cliente
            transporte: $('#transporteCombobox option:selected').text(), // Texto del transporte seleccionado
            destino: departamentoSeleccionado + ', ' + provinciaSeleccionada + ', ' + distritoSeleccionado,
            productos: productosSeleccionados, // Asumiendo que esto contiene la lista de productos
            cantidadTotal: calcularCantidadTotal(productosSeleccionados), // Función para calcular la cantidad total
            montoTotal: ventaTotal
        };


        // Realizar la petición AJAX
        $.ajax({
            url: '/Venta/ConfirmarVenta',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(datosEnvio),
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
    function calcularCantidadTotal(productos) {
        return productos.reduce((total, producto) => total + producto.cantidad, 0);
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




 

    $(document).ready(function () {
        $('#printReceipt').click(function () {
            if (datosVentaGlobal) {
                prepararYMostrarTicket(datosVentaGlobal);
            } else {
                alert("No hay datos de venta disponibles para imprimir.");
            }
        });
    });

    function prepararYMostrarTicket(datosVenta) {
        // Rellenar los datos en la plantilla del ticket
        $('#ticketCliente').text(datosVenta.cliente);
        $('#ticketTransporte').text(datosVenta.transporte);
        $('#ticketDestino').text(datosVenta.destino);
        $('#ticketProductos').empty();
        datosVenta.productos.forEach(producto => {
            $('#ticketProductos').append(`<li>${producto.nombre} - ${producto.cantidad}</li>`);
        });
        $('#ticketCantidadTotal').text(datosVenta.cantidadTotal);
        $('#ticketMontoTotal').text(datosVenta.montoTotal);

        // Mostrar la plantilla en una nueva ventana para imprimir
        var printContent = $('#ticketTemplate').html();
        var win = window.open('', '', 'height=700,width=700');
        win.document.write('<html><head><title>Ticket</title></head><body>');
        win.document.write(printContent);

        // Si realmente necesitas incluir un script, asegúrate de cerrar correctamente la etiqueta
        // win.document.write('<script src="/_framework/aspnetcore-browser-refresh.js"></script>');

        win.document.write('</body></html>');
        win.document.close(); // Necesario para IE >= 10 y navegadores modernos
        win.focus(); // Necesario para IE >= 10

        // Opcional: agregar un pequeño retraso antes de imprimir
        setTimeout(function () {
            win.print();
            win.close();
        }, 250);
    }

    //$('#printReceipt').click(function () {
    //    $('#successModal').hide();
    //    printTicket(/* Aquí necesitas pasar el contenido adecuado para imprimir */);
    //});

});