import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { CatalogService } from './services/catalog';
import { Product } from './models/product';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App implements OnInit {
  products: Product[] = [];
  title = 'Nexus Market';

  constructor(private catalogService: CatalogService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts() {
    this.catalogService.getProducts().subscribe({
      next: (data) => {
        this.products = data;
        console.log('Loaded products', data);
      },
      error: (err) => {
        console.log('Error al cargar productos', err);
      },
    });
  }
}
