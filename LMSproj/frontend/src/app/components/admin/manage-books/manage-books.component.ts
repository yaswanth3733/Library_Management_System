
import { Component, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';

import { FormsModule } from '@angular/forms';

import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({

  selector: 'app-manage-books',

  standalone: true,

  imports: [CommonModule, FormsModule],

  templateUrl: './manage-books.component.html',

  styleUrls: ['./manage-books.component.css']

})

export class ManageBooksComponent implements OnInit {

  books: any[] = [];

  searchTitle: string = '';

  searchCategory: string = '';

  newBook: any = { title: '', author: '', isbn: '', totalCopies: 0, category: '' };

  selectedBook: any = null;

  private apiUrl = 'https://localhost:44373/api/Book';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {

    this.fetchBooks();

  }

  getAuthHeaders(): HttpHeaders {

    const token = localStorage.getItem('token');

    return new HttpHeaders({ 'Authorization': `Bearer ${token}`, 'Content-Type': 'application/json' });

  }

  fetchBooks(): void {

    const headers = this.getAuthHeaders();

    this.http.get<any[]>(`${this.apiUrl}/all`, { headers }).subscribe({

      next: (data) => (this.books = data),

      error: (error) => console.error('Error fetching books:', error)

    });

  }

  searchBooks(): void {

    const headers = this.getAuthHeaders();

    this.http.get<any[]>(`${this.apiUrl}/search?title=${this.searchTitle}&category=${this.searchCategory}`, { headers }).subscribe({

      next: (data) => (this.books = data),

      error: (error) => console.error('Error searching books:', error)

    });

  }

  addBook(): void {

    const headers = this.getAuthHeaders();

    this.http.post(`${this.apiUrl}/add`, this.newBook, { headers }).subscribe({

      next: () => {

        alert('Book added successfully!');

        this.fetchBooks();

        this.newBook = { title: '', author: '', isbn: '', totalCopies: 0, category: '' };

      },

      error: (error) => console.error('Error adding book:', error)

    });

  }

  editBook(book: any): void {

    this.selectedBook = { ...book };

  }

  updateBook(): void {

    const headers = this.getAuthHeaders();

    this.http.put(`${this.apiUrl}/update/${this.selectedBook.bookId}`, this.selectedBook, { headers }).subscribe({

      next: () => {

        alert('Book updated successfully!');

        this.fetchBooks();

        this.selectedBook = null;

      },

      error: (error) => console.error('Error updating book:', error)

    });

  }

  confirmDelete(bookId: number): void {

    if (confirm('Are you sure you want to delete this book?')) {

      this.deleteBook(bookId);

    }

  }

  deleteBook(bookId: number): void {

    const headers = this.getAuthHeaders();

    this.http.delete(`${this.apiUrl}/delete/${bookId}`, { headers }).subscribe({

      next: () => {

        alert('Book deleted successfully!');

        this.fetchBooks();

      },

      error: (error) => console.error('Error deleting book:', error)
    });
  }
} 