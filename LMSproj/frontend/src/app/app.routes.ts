// import { Routes } from '@angular/router';
// import { HomeComponent } from './components/home/home.component';
// import { RegisterComponent } from './components/auth/register/register.component';
// export const routes: Routes = [
//  { path: '', component: HomeComponent },
//  { path: 'register', component: RegisterComponent }, // Register route
// //  { path: 'login', loadComponent: () => import('./components/auth/login/login.component').then(m => m.LoginComponent) },
// //  { path: '**', redirectTo: '' } // Redirect unknown routes to home
// ];

import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { LoginComponent } from './components/auth/login/login.component';
import { MyRequestsComponent } from './components/user/my-requests/my-requests.component';
import { RequestBookComponent } from './components/user/request-book/request-book.component';
import { ManageRequestsComponent } from './components/admin/manage-request/manage-request.component';
import { BookIssuesComponent } from './components/admin/book-issues/book-issues.component';
import { PayFineComponent } from './components/admin/pay-fines/pay-fines.component';
import { ManageBooksComponent } from './components/admin/manage-books/manage-books.component';
import { UserActivityLogComponent } from './components/admin/user-activity-log/user-activity-log.component';
import { IssuedBooksComponent } from './components/user/issued-books/issued-books.component';
import { AboutUsComponent } from './components/about-us/about-us.component';
import { OverdueBooksComponent } from './components/user/overdue-books/overdue-books.component';
// import { AboutComponent } from './components/about/about.component';
export const routes: Routes = [
 { path: '', component: HomeComponent },
 {path:'requests', component: MyRequestsComponent},
 { path: 'register', component: RegisterComponent },
 { path: 'login', component: LoginComponent },
 {path:'request-book',component: RequestBookComponent},
 {path:'manage-requests', component:ManageRequestsComponent},
 {path:'book-issues',component:BookIssuesComponent},
 {path:'pay-fines',component:PayFineComponent},
 {path:'manage-books',component:ManageBooksComponent},
 {path:'user-activity',component:UserActivityLogComponent},
 {path:'issued-books',component:IssuedBooksComponent},
 {path:'about-us',component:AboutUsComponent},
 {path:'overdue-books',component:OverdueBooksComponent},
 {path:'**',redirectTo:''},
//  { path: 'about', component: AboutComponent },
];