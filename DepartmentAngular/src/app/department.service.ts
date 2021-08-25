import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  constructor(private http: HttpClient) { }

  private apiUrl: string = 'http://localhost:18110/api/department';

  getDepartments():Observable<any[]>{
    return this.http.get<any>(this.apiUrl);
  }

  getDepartment(val:any){
    return this.http.get(this.apiUrl+'/'+val);
  }

  postDepartment(val:any){
    return this.http.post(this.apiUrl, val);
  }

  putDepartment(val:any){
    return this.http.put(this.apiUrl, val);
  }

  deleteDepartment(val:any){
    return this.http.delete(this.apiUrl+'/'+val);
  }
}