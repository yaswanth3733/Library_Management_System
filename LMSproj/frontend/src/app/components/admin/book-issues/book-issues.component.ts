
// import { Component, OnInit } from '@angular/core';

// import { HttpClient, HttpHeaders } from '@angular/common/http';

// import { FormsModule } from '@angular/forms';

// import { CommonModule } from '@angular/common';

// @Component({

//   selector: 'app-book-issues',

//   imports: [FormsModule, CommonModule],

//   templateUrl: './book-issues.component.html',

//   styleUrls: ['./book-issues.component.css']

// })

// export class BookIssuesComponent implements OnInit {

//   bookIssues: any[] = [];

//   returnDate: { [key: number]: string } = {}; 

//   private apiUrl = 'https://localhost:44373/api/BookIssue';

//   constructor(private http: HttpClient) {}

//   ngOnInit(): void {

//     this.fetchBookIssues();

//   }


//   fetchBookIssues() {

//     const token = localStorage.getItem('token'); 

//     if (!token) {

//       console.error('No authentication token found!');

//       return;

//     }

//     const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

//     this.http.get<any[]>(`${this.apiUrl}/all`, { headers }).subscribe({

//       next: (data) => {

//         this.bookIssues = data.map(issue => ({

//           ...issue,

//           title: issue.title || 'Unknown Book', 

//           username: issue.username || 'Unknown User' 

//         }));

//       },

//       error: (error) => {

//         console.error('Error fetching book issues:', error);

//         alert('Failed to load issued books. Please check your login status.');

//       }

//     });

//   }


//   returnBook(issueId: number) {

//     const returnDate = this.returnDate[issueId];

//     if (!returnDate) {

//       alert('Please select a return date.');

//       return;

//     }

//     const token = localStorage.getItem('token');

//     if (!token) {

//       console.error('No authentication token found!');

//       return;

//     }

//     const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

//     const returnDto = { IssueId: issueId, ReturnDate: returnDate };

//     this.http.put(`${this.apiUrl}/return`, returnDto, { headers }).subscribe({

//       next: () => {

//         alert('Book returned successfully!');

//         this.fetchBookIssues(); 

//       },

//       error: (error) => {

//         console.error('Error returning book:', error);

//         alert('Failed to return book. Please try again.');

//       }

//     });

//   }

// } 

import { Component, OnInit } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import { FormsModule } from '@angular/forms';

import { CommonModule } from '@angular/common';

@Component({

  selector: 'app-book-issues',

  imports: [FormsModule, CommonModule],

  templateUrl: './book-issues.component.html',

  styleUrls: ['./book-issues.component.css']

})

export class BookIssuesComponent implements OnInit {

  bookIssues: any[] = [];

  filteredBookIssues: any[] = []; // Store filtered results

  returnDate: { [key: number]: string } = {}; // Store return dates for each issue

  searchText: string = ''; // Search text for filtering

  private apiUrl = 'https://localhost:44373/api/BookIssue';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {

    this.fetchBookIssues();

  }

  // Fetch all issued books

  fetchBookIssues() {

    const token = localStorage.getItem('token');

    if (!token) {

      console.error('No authentication token found!');

      return;

    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    this.http.get<any[]>(`${this.apiUrl}/all`, { headers }).subscribe({

      next: (data) => {

        this.bookIssues = data.map(issue => ({

          ...issue,

          title: issue.title || 'Unknown Book',

          username: issue.username || 'Unknown User'

        }));

        this.filteredBookIssues = [...this.bookIssues]; // Initialize filtered list

      },

      error: (error) => {

        console.error('Error fetching book issues:', error);

        alert('Failed to load issued books. Please check your login status.');

      }

    });

  }

  // Apply search filter based on username

  applyFilter() {

    this.filteredBookIssues = this.bookIssues.filter(issue =>

      issue.userName.toLowerCase().includes(this.searchText.toLowerCase())

    );

  }

  // Return a book

  returnBook(issueId: number) {

    const returnDate = this.returnDate[issueId];

    if (!returnDate) {

      alert('Please select a return date.');

      return;

    }

    const token = localStorage.getItem('token');

    if (!token) {

      console.error('No authentication token found!');

      return;

    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    const returnDto = { IssueId: issueId, ReturnDate: returnDate };

    this.http.put(`${this.apiUrl}/return`, returnDto, { headers }).subscribe({

      next: () => {

        alert('Book returned successfully!');

        this.fetchBookIssues(); // Refresh the list

      },

      error: (error) => {

        console.error('Error returning book:', error);

        alert('Failed to return book. Please try again.');

      }

    });

  }

} 