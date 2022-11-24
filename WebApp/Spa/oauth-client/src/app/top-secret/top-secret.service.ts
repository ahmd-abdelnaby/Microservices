import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { BaseService } from "../shared/base.service";
import { ConfigService } from '../shared/config.service';

@Injectable({
  providedIn: 'root'
})

export class TopSecretService extends BaseService {

  constructor(private http: HttpClient, private configService: ConfigService) {    
    super();      
  }

  fetchTopSecretData(token: string) {   
    
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': token
      })
    };

    return this.http.get('https://localhost:7158/valuesapi', httpOptions).pipe(catchError(this.handleError));
  }

  getOrders(token: string) {   
    
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': token
      })
    };

    return this.http.get('https://localhost:7158/orderapi', httpOptions).pipe(catchError(this.handleError));
  }
}
