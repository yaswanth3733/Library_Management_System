import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
@Injectable({
 providedIn: 'root' // Provided globally
})
export class AuthService {
 private baseUrl = 'https://localhost:44373/api/User'; // Your backend API
 constructor(private http: HttpClient) {}
 // ✅ User Registration
 register(userData: any): Observable<any> {
   return this.http.post(`${this.baseUrl}/register`, userData);
 }
 // ✅ User Login
 login(loginData: any): Observable<any> {
   return this.http.post(`${this.baseUrl}/login`, loginData).pipe(
     tap((response: any) => {
       console.log("Login Response:", response); // Debugging
       if (response.token && response.role && response.userId && response.userName) {
         localStorage.setItem('token', response.token);
         localStorage.setItem('role', response.role);
         localStorage.setItem('userId', response.userId.toString()); // Store userId as a string
         localStorage.setItem('userName',response.userName);
         console.log("Role Stored:", response.role); // Debugging
       } else {
         console.error("Role or User ID not found!"); // Debugging
       }
     })
   );
 }
 // ✅ Check if user is logged in
 isLoggedIn(): boolean {
   return !!localStorage.getItem('token');
 }
 // ✅ Get the role of the logged-in user
 getUserRole(): string | null {
   return localStorage.getItem('role');
 }

 getUserName(): string|null{
  return localStorage.getItem('userName');
 }
 // ✅ Get the user ID of the logged-in user
 getUserId(): number | null {
   const userId = localStorage.getItem('userId');
   return userId ? parseInt(userId, 10) : null;
 }
 // ✅ Logout (clear local storage)
 logout(): void {
   localStorage.clear();
 }
}