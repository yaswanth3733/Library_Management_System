import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
@Component({
 selector: 'app-manage-requests',
 imports:[FormsModule,CommonModule],
 standalone: true,
 templateUrl: './manage-request.component.html',
 styleUrls: ['./manage-request.component.css']
})
export class ManageRequestsComponent implements OnInit {
 bookRequests: any[] = [];
 isLoading: boolean = true;
 errorMessage: string = '';
 private baseUrl = 'https://localhost:44373/api/BookRequest';
 constructor(private http: HttpClient) {}
 ngOnInit(): void {
   this.fetchBookRequests();
 }
 fetchBookRequests(): void {
   const headers = this.getAuthHeaders();
   this.http.get<any[]>(`${this.baseUrl}/all`, { headers }).subscribe({
     next: (requests) => {
       this.bookRequests = requests;
       this.isLoading = false;
     },
     error: (error) => {
       console.error("Error fetching book requests:", error);
       this.errorMessage = "Failed to load book requests.";
       this.isLoading = false;
     }
   });
 }
 updateRequestStatus(requestId: number, status: string): void {
   const headers = this.getAuthHeaders();
   const requestBody = { status };
   this.http.put(`${this.baseUrl}/update/${requestId}`, requestBody, { headers }).subscribe({
     next: () => {
       alert(`Request ${status} successfully!`);
       this.fetchBookRequests();
     },
     error: (error) => {
       console.error(`Error updating request:`, error);
       alert(`Failed to update request status.`);
     }
   });
 }
 private getAuthHeaders() {
   const token = localStorage.getItem('token');
   return new HttpHeaders({ 'Authorization': `Bearer ${token}`, 'Content-Type': 'application/json' });
 }
}