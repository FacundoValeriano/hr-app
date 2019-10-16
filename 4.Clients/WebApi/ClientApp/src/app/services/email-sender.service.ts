import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppConfig } from '../app-config/app.config';
import { BaseService } from './base.service';
import { Router } from '@angular/router';
import { Observable, forkJoin } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { DaysOff } from 'src/entities/days-off';

@Injectable()
export class EmailSenderService extends BaseService {
  private baseUrl: string = 'http://localhost:61059/';  
  public headers: HttpHeaders;

  constructor(router: Router, config: AppConfig, http: HttpClient) {
    super(router, config, http);
    this.headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  }

  public sendMail(daysOff: DaysOff): Observable<any> {
    return this.http.post(this.baseUrl
         + 'api/EmailSender' + '/DaysOff', daysOff, {
      headers: this.headersWithAuth, observe: 'response'
    })
      .pipe(
        tap(data => {}),
        catchError(this.handleErrors)
      );
  }


}