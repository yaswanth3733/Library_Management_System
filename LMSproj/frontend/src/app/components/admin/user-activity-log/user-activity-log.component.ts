

// import { Component, OnInit } from '@angular/core';
// import { HttpClient, HttpHeaders } from '@angular/common/http';
// import { CommonModule } from '@angular/common';
// import { FormsModule } from '@angular/forms';
// @Component({
//  selector: 'app-user-activity-log',
//  standalone: true,
//  imports: [CommonModule, FormsModule],
//  templateUrl: './user-activity-log.component.html',
//  styleUrls: ['./user-activity-log.component.css']
// })
// export class UserActivityLogComponent implements OnInit {
//  logs: any[] = [];
//  filteredLogs: any[] = [];
//  searchUsername: string = '';
//  private apiUrl = 'https://localhost:44373/api/UserActivityLog';
//  constructor(private http: HttpClient) {}
//  ngOnInit(): void {
//    this.fetchAllLogs();
//  }
//  getAuthHeaders(): HttpHeaders {
//    const token = localStorage.getItem('token');
//    return new HttpHeaders({ 'Authorization': `Bearer ${token}` });
//  }
//  fetchAllLogs(): void {
//    const headers = this.getAuthHeaders();
//    this.http.get<any[]>(`${this.apiUrl}/all`, { headers }).subscribe({
//      next: (data) => {
//        this.logs = data;
//        this.filteredLogs = data; // Initialize with full logs
//      },
//      error: (error) => console.error('Error fetching activity logs:', error)
//    });
//  }
//  searchByUsername(): void {
//    if (!this.searchUsername.trim()) {
//      this.filteredLogs = this.logs; // Reset to all logs if search is empty
//      return;
//    }
//    const headers = this.getAuthHeaders();
//    this.http.get<any[]>(`${this.apiUrl}/username/${this.searchUsername}`, { headers }).subscribe({
//      next: (data) => (this.filteredLogs = data),
//      error: (error) => console.error('Error fetching logs by username:', error)
//    });
//  }
//  fetchBorrowedBooks(): void {
//    const headers = this.getAuthHeaders();
//    if (this.searchUsername.trim()) {
//      // Fetch borrowed books of the specific user
//      this.http.get<any[]>(`${this.apiUrl}/borrowed/${this.searchUsername}`, { headers }).subscribe({
//        next: (data) => {
//          this.logs = data;
//          this.filteredLogs = data;
//        },
//        error: (error) => console.error('Error fetching borrowed books for user:', error)
//      });
//    } else {
//      // Fetch all borrowed books
//      this.http.get<any[]>(`${this.apiUrl}/borrowed`, { headers }).subscribe({
//        next: (data) => {
//          this.logs = data;
//          this.filteredLogs = data;
//        },
//        error: (error) => console.error('Error fetching borrowed books:', error)
//      });
//    }
//  }
//  fetchReturnedBooks(): void {
//    const headers = this.getAuthHeaders();
//    if (this.searchUsername.trim()) {
//      // Fetch returned books of the specific user
//      this.http.get<any[]>(`${this.apiUrl}/returned/${this.searchUsername}`, { headers }).subscribe({
//        next: (data) => {
//          this.logs = data;
//          this.filteredLogs = data;
//        },
//        error: (error) => console.error('Error fetching returned books for user:', error)
//      });
//    } else {
//      // Fetch all returned books
//      this.http.get<any[]>(`${this.apiUrl}/returned`, { headers }).subscribe({
//        next: (data) => {
//          this.logs = data;
//          this.filteredLogs = data;
//        },
//        error: (error) => console.error('Error fetching returned books:', error)
//      });
//    }
//  }
// }


import { Component, OnInit } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import { CommonModule } from '@angular/common';

import { FormsModule } from '@angular/forms';

@Component({

  selector: 'app-user-activity-log',

  standalone: true,

  imports: [CommonModule, FormsModule],

  templateUrl: './user-activity-log.component.html',

  styleUrls: ['./user-activity-log.component.css']

})

export class UserActivityLogComponent implements OnInit {

  logs: any[] = [];           // Stores all logs

  filteredLogs: any[] = [];   // Stores the filtered logs (borrowed, returned, overdue, etc.)

  searchUsername: string = '';

  private apiUrl = 'https://localhost:44373/api/UserActivityLog';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {

    this.fetchAllLogs();

  }

  getAuthHeaders(): HttpHeaders {

    const token = localStorage.getItem('token');

    return new HttpHeaders({ 'Authorization': `Bearer ${token}` });

  }

  fetchAllLogs(): void {

    const headers = this.getAuthHeaders();

    this.http.get<any[]>(`${this.apiUrl}/all`, { headers }).subscribe({

      next: (data) => {

        this.logs = data;

        this.filteredLogs = [...this.logs]; // Copy to maintain original data

      },

      error: (error) => console.error('Error fetching activity logs:', error)

    });

  }

  searchByUsername(): void {

    if (!this.searchUsername.trim()) {

      this.filteredLogs = [...this.logs]; // Reset to all logs if search is empty

      return;

    }

    const headers = this.getAuthHeaders();

    this.http.get<any[]>(`${this.apiUrl}/username/${this.searchUsername}`, { headers }).subscribe({

      next: (data) => {

        this.filteredLogs = [...data];

      },

      error: (error) => console.error('Error fetching logs by username:', error)

    });

  }

  fetchBorrowedBooks(): void {

    const headers = this.getAuthHeaders();

    if (this.searchUsername.trim()) {

      // Fetch borrowed books of a specific user

      this.http.get<any[]>(`${this.apiUrl}/borrowed/${this.searchUsername}`, { headers }).subscribe({

        next: (data) => {

          this.filteredLogs = [...data]; 

        },

        error: (error) => console.error('Error fetching borrowed books for user:', error)

      });

    } else {

      // Fetch all borrowed books

      this.http.get<any[]>(`${this.apiUrl}/borrowed`, { headers }).subscribe({

        next: (data) => {

          this.filteredLogs = [...data]; 

        },

        error: (error) => console.error('Error fetching borrowed books:', error)

      });

    }

  }

  fetchReturnedBooks(): void {

    const headers = this.getAuthHeaders();

    if (this.searchUsername.trim()) {

      // Fetch returned books of a specific user

      this.http.get<any[]>(`${this.apiUrl}/returned/${this.searchUsername}`, { headers }).subscribe({

        next: (data) => {

          this.filteredLogs = [...data];

        },

        error: (error) => console.error('Error fetching returned books for user:', error)

      });

    } else {

      // Fetch all returned books

      this.http.get<any[]>(`${this.apiUrl}/returned`, { headers }).subscribe({

        next: (data) => {

          this.filteredLogs = [...data]; 

        },

        error: (error) => console.error('Error fetching returned books:', error)

      });

    }

  }

  fetchOverdueBooks(): void {

    const today = new Date().toISOString().split('T')[0]; // Get today's date in YYYY-MM-DD format

    this.filteredLogs = this.logs.filter(log => log.dueDate && log.dueDate < today);

  }

} 