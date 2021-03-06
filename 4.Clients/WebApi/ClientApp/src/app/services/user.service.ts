import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { Router } from "@angular/router";
import { AppConfig } from "../app-config/app.config";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { catchError, tap } from 'rxjs/operators';
import { User } from "src/entities/user";

@Injectable()
export class UserService extends BaseService {

  constructor(router: Router, config: AppConfig, http: HttpClient) {
    super(router, config, http);
    this.apiUrl += 'User';
  }

  public getRoleByUserName(userName: string): Observable<string> {
    const url = `${this.apiUrl}/GetRoleByUserName/${userName}`;
    return this.http.get<string>(url, {
      headers: this.headersWithAuth
    })
      .pipe(
        catchError(this.handleErrors)
      );
  }

  getRoles() {
    let currentUser: User = JSON.parse(localStorage.getItem('currentUser'));
    if (currentUser.Role == "") {
      this.getRoleByUserName(currentUser.Email).subscribe(res => {
        currentUser.Role = res['role'];
        localStorage.setItem('currentUser', JSON.stringify(currentUser));
        location.reload();
      }, err => {
        console.log('error');
      });
    }
  }
}