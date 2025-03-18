

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Component({
 selector: 'app-home',
 standalone: true,
 imports: [CommonModule],
 templateUrl: './home.component.html',
 styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
 isLoggedIn = false;
 userRole: string | null = null;
 userName: string | null = null;
 userId: number | null = null;
 private apiUrl = 'https://localhost:44373/api/User';
 constructor(private router: Router, private authService: AuthService, private http: HttpClient) {}
 ngOnInit(): void {
   this.updateLoginStatus();
 }
 updateLoginStatus(): void {
   this.isLoggedIn = this.authService.isLoggedIn();
   this.userRole = this.authService.getUserRole();
   this.userName = this.authService.getUserName();
   this.userId = Number(localStorage.getItem('userId'));
 }
 navigateTo(route: string): void {
   this.router.navigate([route]).then(() => {
     this.updateLoginStatus();
   });
 }
 logout(): void {
   this.authService.logout();
   this.updateLoginStatus();
   this.router.navigate(['/']);
 }
 confirmDelete(): void {
   if (confirm("Are you sure you want to delete your account? This action cannot be undone!")) {
     this.deleteAccount();
   }
 }

deleteAccount(): void {

  if (!this.userId) {

    console.error("User ID not found.");

    return;

  }

  const token = localStorage.getItem("token");

  if (!token) {

    console.error("No authentication token found!");

    return;

  }

  const headers = new HttpHeaders().set("Authorization", `Bearer ${token}`);

  this.http.delete(`https://localhost:44373/api/User/delete/${this.userId}`, { headers, responseType: 'text' }).subscribe({

    next: (response) => {

      console.log("Delete response:", response);

      alert("User account deleted successfully!");

      this.logout(); // ✅ Log out after successful deletion

    },

    error: (error) => {

      console.error("Error deleting account:", error);

      // ✅ Extract and display the actual error message from the backend

      const errorMessage = error.error || "Failed to delete account. Please try again.";

      alert(errorMessage);

    }

  });

} 
}
