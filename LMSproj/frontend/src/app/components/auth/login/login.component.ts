
// import { Component } from '@angular/core';

// import { Router } from '@angular/router';

// import { CommonModule } from '@angular/common';

// import { FormsModule } from '@angular/forms';

// import { AuthService } from '../../../services/auth.service';

// @Component({

//   selector: 'app-login',

//   standalone: true,

//   imports: [CommonModule, FormsModule],

//   templateUrl: './login.component.html',

//   styleUrls: ['./login.component.css']

// })

// export class LoginComponent {

//   loginData = {

//     email: '',

//     password: ''

//   };

//   constructor(private router: Router, private authService: AuthService) {}

//   onLogin(): void {

//     this.authService.login(this.loginData).subscribe({

//       next: (response) => {

//         console.log('Login successful:', response);

//         alert('Login successful.');

     

//           this.router.navigate(['/']); // Redirect normal users


//       },

//       error: (error) => {

//         console.error('Login failed:', error);

//         alert('Invalid email or password.');

//       }

//     });

//   }

// } 

import { Component } from '@angular/core';

import { Router } from '@angular/router';

import { CommonModule } from '@angular/common';

import { FormsModule, ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';

import { AuthService } from '../../../services/auth.service';

@Component({

  selector: 'app-login',

  standalone: true,

  imports: [CommonModule, FormsModule, ReactiveFormsModule],

  templateUrl: './login.component.html',

  styleUrls: ['./login.component.css']

})

export class LoginComponent {

  loginForm: FormGroup;

  constructor(private router: Router, private authService: AuthService) {

    this.loginForm = new FormGroup({

      email: new FormControl('', [Validators.required, Validators.email]),

      password: new FormControl('', Validators.required)

    });

  }

  onLogin(): void {

    if (this.loginForm.valid) {

      const loginData = this.loginForm.value;

      this.authService.login(loginData).subscribe({

        next: (response) => {

          console.log('Login successful:', response);

          alert('Login successful.');

          this.router.navigate(['/']); // Redirect to home page after login

        },

        error: (error) => {

          console.error('Login failed:', error);

          alert('Invalid email or password.');

        }

      });

    }

  }

} 