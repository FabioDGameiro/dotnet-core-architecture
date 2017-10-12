import { AuthService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';

@Component({
  selector: 'app-my-tasks',
  templateUrl: './my-tasks.component.html',
  styleUrls: ['./my-tasks.component.css']
})
export class MyTasksComponent implements OnInit {
  response: string;

  constructor(private http: Http, private authService: AuthService) {}

  ngOnInit() {
    const header = new Headers({ Authorization: this.authService.getAuthorizationHeaderValue() });
    const options = new RequestOptions({ headers: header });

    this.http.get('http://localhost:3000/tasks', options)
      .subscribe(response => (this.response = response.text()));
  }
}
