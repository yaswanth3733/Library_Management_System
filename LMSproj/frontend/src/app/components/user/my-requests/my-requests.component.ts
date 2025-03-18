
// import { Component, OnInit } from '@angular/core';
// import { HttpClient, HttpHeaders } from '@angular/common/http';
// import { CommonModule } from '@angular/common';
// import { AuthService } from '../../../services/auth.service';
// // ✅ Define BookRequest interface for better type safety
// interface BookRequest {
//  requestId: number;
//  title: string;
//  status: string;
// }
// @Component({
//  selector: 'app-my-requests',
//  standalone: true,
//  imports: [CommonModule],
//  templateUrl: './my-requests.component.html',
//  styleUrls: ['./my-requests.component.css']
// })
// export class MyRequestsComponent implements OnInit {
//  userRequests: BookRequest[] = [];
//  userId: number | null = null;
//  isLoading: boolean = true;
//  errorMessage: string = '';
//  private apiUrl = 'https://localhost:44373/api/BookRequest';
//  constructor(private authService: AuthService, private http: HttpClient) {}
//  ngOnInit(): void {
//    this.userId = this.authService.getUserId();
//    if (this.userId !== null) {
//      this.fetchUserRequests();
//    } else {
//      this.isLoading = false;
//      this.errorMessage = "User ID not found. Please log in again.";
//      console.error(this.errorMessage);
//    }
//  }
//  fetchUserRequests(): void {
//    if (this.userId === null) {
//      this.errorMessage = "User ID is missing. Cannot fetch requests.";
//      this.isLoading = false;
//      console.error(this.errorMessage);
//      return;
//    }
//    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
//    this.http.get<BookRequest[]>(`${this.apiUrl}/user/${this.userId}`, { headers }).subscribe({
//      next: (requests) => {
//        console.log("API Response:", requests);
//        this.userRequests = requests.map((request) => ({
//          ...request,
//          title: request.title ?? "Unknown"
//        }));
//        this.isLoading = false;
//      },
//      error: (error) => {
//        this.isLoading = false;
//        this.errorMessage = "Error fetching requests. Please try again later.";
//        console.error("Error fetching requests:", error);
//      }
//    });
//  }
//  // ✅ Delete a pending book request
//  deleteRequest(requestId: number): void {
//    if (!confirm('Are you sure you want to delete this request?')) {
//      return;
//    }  
//    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
//    this.http.delete(`${this.apiUrl}/delete/${requestId}`, { headers }).subscribe({
//      next: () => {
//        alert('Request deleted successfully!');
//       //  this.fetchUserRequests(); // Refresh the list
//       this.userRequests = this.userRequests.filter(request => request.requestId !== requestId);
//      },
//      error: (error) => {
//        console.error('Error deleting request:', error);
//        alert(error.error || 'Failed to delete request.');
//      }
//    });
//  }
// }

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../services/auth.service';
import { UserService } from '../../../services/user.service';
// ✅ Define BookRequest interface for better type safety
interface BookRequest {
 requestId: number;
 title: string;
 status: string;
}
@Component({
 selector: 'app-my-requests',
 standalone: true,
 imports: [CommonModule],
 templateUrl: './my-requests.component.html',
 styleUrls: ['./my-requests.component.css']
})
export class MyRequestsComponent implements OnInit {
 userRequests: BookRequest[] = [];
 userId: number | null = null;
 isLoading: boolean = true;
 errorMessage: string = '';
 constructor(private authService: AuthService, private userService: UserService) {}
 ngOnInit(): void {
   this.userId = this.authService.getUserId();
   if (this.userId !== null) {
     this.fetchUserRequests();
   } else {
     this.isLoading = false;
     this.errorMessage = "User ID not found. Please log in again.";
     console.error(this.errorMessage);
   }
 }
 fetchUserRequests(): void {
   if (this.userId === null) {
     this.errorMessage = "User ID is missing. Cannot fetch requests.";
     this.isLoading = false;
     console.error(this.errorMessage);
     return;
   }
   this.userService.getUserRequests(this.userId).subscribe({
     next: (requests:BookRequest[]) => {
       console.log("API Response:", requests);
       this.userRequests = requests.map((request) => ({
         ...request,
         title: request.title ?? "Unknown"
       }));
       this.isLoading = false;
     },
     error: (error) => {
       this.isLoading = false;
       this.errorMessage = "Error fetching requests. Please try again later.";
       console.error("Error fetching requests:", error);
     }
   });
 }
 // ✅ Delete a pending book request
 deleteRequest(requestId: number): void {
   if (!confirm('Are you sure you want to delete this request?')) {
     return;
   }
   this.userService.deleteRequest(requestId).subscribe({
     next: () => {
       alert('Request deleted successfully!');
       this.userRequests = this.userRequests.filter(request => request.requestId !== requestId);
     },
     error: (error) => {
       console.error('Error deleting request:', error);
       alert(error.error || 'Failed to delete request.');
     }
   });
 }
}