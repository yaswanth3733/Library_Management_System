

// import { Component, OnInit } from '@angular/core';
// import { UserService } from '../../../services/user.service';
// import { AuthService } from '../../../services/auth.service';
// import { CommonModule } from '@angular/common';
// import { FormsModule } from '@angular/forms';
// @Component({
//  selector: 'app-request-book',
//  standalone: true,
//  imports: [CommonModule,FormsModule],
//  templateUrl: './request-book.component.html',
//  styleUrls: ['./request-book.component.css']
// })
// export class RequestBookComponent implements OnInit {
//  availableBooks: any[] = [];
//  userId: number | null = null;
//  isLoading: boolean = true;
//  errorMessage: string = '';
//  searchTitle: string = '';
//  searchCategory: string = '';
//  constructor(private userService: UserService, private authService: AuthService) {}
//  ngOnInit(): void {
//    this.userId = this.authService.getUserId();
//    if (this.userId !== null) {
//      this.fetchAvailableBooks();
//    } else {
//      this.isLoading = false;
//      this.errorMessage = "User ID not found. Please log in again.";
//      console.error(this.errorMessage);
//    }
//  }
//  // ✅ Fetch available books
//  fetchAvailableBooks(): void {
//    this.userService.getAvailableBooks().subscribe({
//      next: (books) => {
//        this.availableBooks = books;
//        this.isLoading = false;
//      },
//      error: (error) => {
//        this.isLoading = false;
//        this.errorMessage = "Error fetching books. Please try again later.";
//        console.error("Error fetching books:", error);
//      }
//    });
//  }
//  // ✅ Search books
//  searchBooks(): void {
//    this.isLoading = true;
//    this.userService.searchBooks(this.searchTitle, this.searchCategory).subscribe({
//      next: (books) => {
//        this.availableBooks = books;
//        this.isLoading = false;
//      },
//      error: (error) => {
//        this.isLoading = false;
//        this.errorMessage = "No books found with the given search criteria.";
//        console.error("Error searching books:", error);
//      }
//    });
//  }
//  // ✅ Request a book
//  requestBook(bookId: number): void {
//    if (this.userId === null) {
//      alert("You must be logged in to request a book.");
//      return;
//    }
//    const requestData = { userId: this.userId, bookId: bookId };
//    this.userService.requestBook(requestData).subscribe({
//      next: () => {
//        alert("Book request submitted successfully!");
//      },
//      error: (error) => {
//        console.error("Error requesting book:", error);
//        alert("Failed to request book. Please try again.");
//      }
//    });
//  }
// }

import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../services/user.service';
import { AuthService } from '../../../services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
@Component({
 selector: 'app-request-book',
 standalone: true,
 imports: [CommonModule, FormsModule],
 templateUrl: './request-book.component.html',
 styleUrls: ['./request-book.component.css']
})
export class RequestBookComponent implements OnInit {
 availableBooks: any[] = [];
 userId: number | null = null;
 isLoading: boolean = true;
 errorMessage: string = '';
 searchTitle: string = '';
 searchCategory: string = '';
 constructor(private userService: UserService, private authService: AuthService) {}
 ngOnInit(): void {
   this.userId = this.authService.getUserId();
   if (this.userId !== null) {
     this.fetchAvailableBooks();
   } else {
     this.isLoading = false;
     this.errorMessage = "User ID not found. Please log in again.";
     console.error(this.errorMessage);
   }
 }
 // ✅ Fetch available books
 fetchAvailableBooks(): void {
   this.userService.getAvailableBooks().subscribe({
     next: (books) => {
       this.availableBooks = books;
       this.isLoading = false;
     },
     error: (error) => {
       this.isLoading = false;
       this.errorMessage = "Error fetching books. Please try again later.";
       console.error("Error fetching books:", error);
     }
   });
 }
 // ✅ Search books
 searchBooks(): void {
   this.isLoading = true;
   this.userService.searchBooks(this.searchTitle, this.searchCategory).subscribe({
     next: (books) => {
       this.availableBooks = books;
       this.isLoading = false;
     },
     error: (error) => {
       this.isLoading = false;
       this.errorMessage = "No books found with the given search criteria.";
       console.error("Error searching books:", error);
     }
   });
 }
 // ✅ Request a book
 requestBook(bookId: number): void {
   if (this.userId === null) {
     alert("You must be logged in to request a book.");
     return;
   }
   const requestData = { userId: this.userId, bookId: bookId };
   this.userService.requestBook(requestData).subscribe({
     next: () => {
       alert("Book request submitted successfully!");
     },
     error: (error) => {
       console.error("Error requesting book:", error);
       alert("Failed to request book. Please try again.");
     }
   });
 }
}