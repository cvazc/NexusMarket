import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { CatalogService } from './services/catalog';
import { Product } from './models/product';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, ReactiveFormsModule],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App implements OnInit {
  products: Product[] = [];
  title = 'Nexus Market';
  productForm: FormGroup;

  constructor(private catalogService: CatalogService, private fb: FormBuilder) {
    this.productForm = this.fb.group({
      name: ['', [Validators.required]],
      price: [0, [Validators.required, Validators.min(0.01)]],
      stock: [0, [Validators.required, Validators.min(0)]],
      description: [''],
    });
  }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts() {
    this.catalogService.getProducts().subscribe({
      next: (data) => (this.products = data),
      error: (err) => console.error('Error cargando:', err),
    });
  }

  onSubmit() {
    if (this.productForm.valid) {
      const newProduct = this.productForm.value;

      this.catalogService.createProduct(newProduct).subscribe({
        next: (product) => {
          console.log('Creado:', product);
          this.products.push(product);
          this.productForm.reset();
        },
        error: (err) => alert('Error al crear producto: ' + err.message),
      });
    } else {
      alert('Formulario inválido, revisa los campos.');
    }
  }

  deleteProduct(id: number) {
    const confirmDelete = confirm('¿Estás seguro de que quieres eliminar este producto?');

    if (confirmDelete) {
      this.catalogService.deleteProduct(id).subscribe({
        next: () => {
          console.log('Producto eliminado con éxito');
          this.products = this.products.filter((p) => p.id !== id);
        },
        error: (err) => {
          console.error('Error al eliminar:', err);
          alert('No se pudo eliminar el producto.');
        },
      });
    }
  }
}
