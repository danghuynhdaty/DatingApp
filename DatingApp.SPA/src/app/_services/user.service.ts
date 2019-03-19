import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';



@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = environment.apiUrl + 'users/';

  constructor(private httpClient: HttpClient) { }

  getUsers(): Observable<User[]> {
    return this.httpClient.get<User[]>(this.baseUrl + 'getusers');
  }

  getUser(id: number): Observable<User> {
    return this.httpClient.get<User>(this.baseUrl + id);
  }

  updateUser(user: User) {
    return this.httpClient.put(this.baseUrl + 'updateuser', user);
  }

  setMainPhoto(userId: number, id: number) {
    return this.httpClient.post(this.baseUrl + userId + '/photos/' + id + '/setmainphoto', {});
  }

  deletePhoto(userId: number, id: number) {
    return this.httpClient.delete(this.baseUrl + userId + '/photos/' + id + '/delete');
  }
}
