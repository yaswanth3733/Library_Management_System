import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
@Component({
 selector: 'app-overdue-books',
 standalone: true,
 imports: [CommonModule],
 templateUrl: './overdue-books.component.html',
 styleUrls: ['./overdue-books.component.css']
})
export class OverdueBooksComponent implements OnInit {
 userId: number | null = null;
 overdueBooks: any[] = []; // Store all overdue books
 private apiUrl = 'https://localhost:44373/api/BookIssue/user';
 constructor(private http: HttpClient) {}
 ngOnInit(): void {
   this.userId = Number(localStorage.getItem('userId')); // Get userId from localStorage
   if (this.userId) {
     this.fetchOverdueBooks();
   } else {
     console.error("User ID not found.");
   }
 }
 fetchOverdueBooks(): void {
   const token = localStorage.getItem('token');
   if (!token) {
     console.error("No authentication token found!");
     return;
   }
   const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
   this.http.get<any[]>(`${this.apiUrl}/${this.userId}`, { headers }).subscribe({
     next: (data) => {
       this.overdueBooks = data.filter(book => {
         const today = new Date();
         const dueDate = new Date(book.dueDate);
         return !book.returnDate && dueDate < today;
       });
     },
     error: (error) => console.error("Error fetching overdue books:", error)
   });
 }
}