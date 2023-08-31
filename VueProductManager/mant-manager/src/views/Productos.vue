<template>
    <div>
      <h1>Productos</h1>
      <button @click="showCreateForm = true">Crear nuevo producto</button>
  
      <!-- Formulario para crear productos -->
      <div v-if="showCreateForm">
        <h2>Crear Producto</h2>
        <input v-model="newProduct.nombre" placeholder="Nombre" />
        <input v-model="newProduct.precio" placeholder="Precio" />
        <button @click="createProduct">Crear</button>
        <button @click="showCreateForm = false">Cancelar</button>
      </div>
  
      <!-- Lista de productos -->
      <ul>
        <li v-for="producto in productos" :key="producto.id">
          {{ producto.nombre }} - {{ producto.precio }}
          <button @click="editProduct(producto)">Editar</button>
          <button @click="deleteProduct(producto.id)">Eliminar</button>
        </li>
      </ul>
  
      <!-- Formulario para editar productos -->
      <div v-if="showEditForm">
        <h2>Editar Producto</h2>
        <input v-model="selectedProduct.nombre" />
        <input v-model="selectedProduct.precio" />
        <button @click="updateProduct">Actualizar</button>
        <button @click="showEditForm = false">Cancelar</button>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        productos: [
          { id: 1, nombre: "Producto 1", precio: 100 },
          { id: 2, nombre: "Producto 2", precio: 200 },
        ],
        newProduct: { nombre: "", precio: 0 },
        selectedProduct: null,
        showCreateForm: false,
        showEditForm: false,
      };
    },
    methods: {
      createProduct() {
        const newId = this.productos.length ? Math.max(this.productos.map(p => p.id)) + 1 : 1;
        this.productos.push({ id: newId, ...this.newProduct });
        this.newProduct = { nombre: "", precio: 0 };
        this.showCreateForm = false;
      },
      editProduct(producto) {
        this.selectedProduct = { ...producto };
        this.showEditForm = true;
      },
      updateProduct() {
        const index = this.productos.findIndex(p => p.id === this.selectedProduct.id);
        if (index !== -1) {
          this.productos[index] = this.selectedProduct;
        }
        this.showEditForm = false;
      },
      deleteProduct(id) {
        this.productos = this.productos.filter(p => p.id !== id);
      },
    },
  };
  </script>
  