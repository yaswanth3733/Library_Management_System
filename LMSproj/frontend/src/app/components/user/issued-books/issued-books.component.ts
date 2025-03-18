// import { Component, OnInit } from '@angular/core';
// import { HttpClient, HttpHeaders } from '@angular/common/http';
// import { CommonModule } from '@angular/common';
// @Component({
//  selector: 'app-issued-books',
//  standalone: true,
//  imports: [CommonModule],
//  templateUrl: './issued-books.component.html',
//  styleUrls: ['./issued-books.component.css']
// })
// export class IssuedBooksComponent implements OnInit {
//  userId: number | null = null;
//  issuedBooks: any[] = [];
//  private apiUrl = 'https://localhost:44373/api/BookIssue/user';
//  constructor(private http: HttpClient) {}
//  ngOnInit(): void {
//    this.userId = Number(localStorage.getItem('userId')); // Assuming userId is stored in localStorage
//    if (this.userId) {
//      this.fetchIssuedBooks();
//    } else {
//      console.error("User ID not found.");
//    }
//  }
//  fetchIssuedBooks(): void {
//    const token = localStorage.getItem('token');
//    if (!token) {
//      console.error("No authentication token found!");
//      return;
//    }
//    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
//    this.http.get<any[]>(`${this.apiUrl}/${this.userId}`, { headers }).subscribe({
//      next: (data) => this.issuedBooks = data,
//      error: (error) => console.error("Error fetching issued books:", error)
//    });
//  }
// }

import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
@Component({
 selector: 'app-issued-books',
 standalone: true,
 imports: [CommonModule],
 templateUrl: './issued-books.component.html',
 styleUrls: ['./issued-books.component.css']
})
export class IssuedBooksComponent implements OnInit {
 userId: number | null = null;
 issuedBooks: any[] = []; // Store all issued books
 filteredBooks: any[] = []; // Store filtered books
 notReturned: boolean = false; // Toggle filter
 private apiUrl = 'https://localhost:44373/api/BookIssue/user';
 constructor(private http: HttpClient) {}
 ngOnInit(): void {
   this.userId = Number(localStorage.getItem('userId')); // Get userId from localStorage
   if (this.userId) {
     this.fetchIssuedBooks();
   } else {
     console.error("User ID not found.");
   }
 }
 fetchIssuedBooks(): void {
   const token = localStorage.getItem('token');
   if (!token) {
     console.error("No authentication token found!");
     return;
   }
   const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
   this.http.get<any[]>(`${this.apiUrl}/${this.userId}`, { headers }).subscribe({
     next: (data) => {
       this.issuedBooks = data;
       this.applyFilter(); // Apply filter after fetching data
     },
     error: (error) => console.error("Error fetching issued books:", error)
   });
 }
 applyFilter(): void {
   if (this.notReturned) {
     this.filteredBooks = this.issuedBooks.filter(book => !book.returnDate);
   } else {
     this.filteredBooks = [...this.issuedBooks]; // Show all books
   }
 }
 toggleNotReturned(): void {
   this.notReturned = !this.notReturned;
   this.applyFilter(); // Reapply filter when toggled
 }
}