// import { Component } from '@angular/core';
// import { Router } from '@angular/router';
// import { CommonModule } from '@angular/common';
// import { FormsModule } from '@angular/forms';
// import { AuthService } from '../../../services/auth.service';
// @Component({
//  selector: 'app-register',
//  standalone: true,
//  imports:[CommonModule,FormsModule],
//  templateUrl: './register.component.html',
//  styleUrls: ['./register.component.css']
// })
// export class RegisterComponent {
//  registerData = {
//    fullName: '',
//    email: '',
//    password: '',
//    role: 'User' // Default role
//  };
//  constructor(private router: Router, private authService: AuthService) {} // ✅ Inject AuthService
//  OnRegister(): void {
//    this.authService.register(this.registerData).subscribe({
//      next: (response) => {
//        console.log('Registration successful:', response);
//        alert('Registration successful. Please log in.');
//        this.router.navigate(['/login']); // ✅ Redirect to login page after success
//      },
//      error: (error) => {
//        console.error('Registration failed:', error);
//        alert('Registration failed. Please try again.');
//      },
//    });
//  }
// }

import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';
@Component({
 selector: 'app-register',
 standalone: true,
 imports: [CommonModule, ReactiveFormsModule],
 templateUrl: './register.component.html',
 styleUrls: ['./register.component.css']
})
export class RegisterComponent {
 // Define a reactive form with validations
 registerForm: FormGroup;
 constructor(private router: Router, private authService: AuthService) {
   this.registerForm = new FormGroup({
     fullName: new FormControl('', Validators.required),
     email: new FormControl('', [Validators.required, Validators.email]),
     password: new FormControl('', [Validators.required, Validators.minLength(6)]),
     role: new FormControl('User', Validators.required)
   });
 }
 onRegister(): void {
   // If form is invalid, mark all controls as touched to trigger error messages
   if (this.registerForm.invalid) {
     this.registerForm.markAllAsTouched();
     return;
   }
   // Get the form values
   const registerData = this.registerForm.value;
   // Call the register API via AuthService
   this.authService.register(registerData).subscribe({
     next: (response) => {
       console.log('Registration successful:', response);
       alert('Registration successful. Please log in.');
       this.router.navigate(['/login']); // Redirect to login page
     },
     error: (error) => {
       console.error('Registration failed:', error);
       alert('Registration failed. Please try again.');
     }
   });
 }
}