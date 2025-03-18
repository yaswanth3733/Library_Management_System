
// import { Injectable } from '@angular/core';
// import { HttpClient, HttpHeaders } from '@angular/common/http';
// import { Observable, throwError } from 'rxjs';
// @Injectable({
//  providedIn: 'root'
// })
// export class UserService {
//  private baseUrl = 'https://localhost:44373/api'; // ✅ Ensure API base URL is correct
//  constructor(private http: HttpClient) {}
//  // ✅ Get Authentication Headers (Helper Function)
//  private getAuthHeaders(): HttpHeaders {
//    const token = localStorage.getItem('token');
//    if (!token) {
//      console.error("No authentication token found. User must log in.");
//      throw throwError(() => new Error("No authentication token found."));
//    }
//    return new HttpHeaders({
//      'Authorization': `Bearer ${token}`,
//      'Content-Type': 'application/json'
//    });
//  }
//  // ✅ Get Book Requests for Logged-in User
//  getUserRequests(userId: number): Observable<any> {
//    return this.http.get(`${this.baseUrl}/BookRequest/user/${userId}`, { headers: this.getAuthHeaders() });
//  }
//  // ✅ Get all available books
//  getAvailableBooks(): Observable<any> {
//    return this.http.get(`${this.baseUrl}/Book/all`, { headers: this.getAuthHeaders() });
//  }
//  // ✅ Request a book
//  requestBook(requestData: any): Observable<any> {
//    return this.http.post(`${this.baseUrl}/BookRequest/request`, requestData, { headers: this.getAuthHeaders() });
//  }
//  // ✅ Search books by title and category
//  searchBooks(title: string | null, category: string | null): Observable<any> {
//    let params: any = {};
//    if (title) params.title = title;
//    if (category) params.category = category;
//    return this.http.get(`${this.baseUrl}/Book/search`, {
//      headers: this.getAuthHeaders(),
//      params: params
//    });
//  }

// }

import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';

@Injectable({

  providedIn: 'root'

})

export class UserService {

  private baseUrl = 'https://localhost:44373/api'; // ✅ Ensure API base URL is correct

  constructor(private http: HttpClient) {}

  // ✅ Get Authentication Headers (Helper Function)

  private getAuthHeaders(): HttpHeaders {

    const token = localStorage.getItem('token');

    if (!token) {

      console.error("No authentication token found. User must log in.");

      throw throwError(() => new Error("No authentication token found."));

    }

    return new HttpHeaders({

      'Authorization': `Bearer ${token}`,

      'Content-Type': 'application/json'

    });

  }

  // ✅ Get Book Requests for Logged-in User

  getUserRequests(userId: number): Observable<any> {

    return this.http.get(`${this.baseUrl}/BookRequest/user/${userId}`, { headers: this.getAuthHeaders() });

  }

  // ✅ Get all available books

  getAvailableBooks(): Observable<any> {

    return this.http.get(`${this.baseUrl}/Book/all`, { headers: this.getAuthHeaders() });

  }

  // ✅ Request a book

  requestBook(requestData: any): Observable<any> {

    return this.http.post(`${this.baseUrl}/BookRequest/request`, requestData, { headers: this.getAuthHeaders() });

  }

  // ✅ Search books by title and category

  searchBooks(title: string | null, category: string | null): Observable<any> {

    let params: any = {};

    if (title) params.title = title;

    if (category) params.category = category;

    return this.http.get(`${this.baseUrl}/Book/search`, {

      headers: this.getAuthHeaders(),

      params: params

    });

  }

  // ✅ Delete a book request

  deleteRequest(requestId: number): Observable<any> {

    return this.http.delete(`${this.baseUrl}/BookRequest/delete/${requestId}`, { headers: this.getAuthHeaders() });

  }

} 