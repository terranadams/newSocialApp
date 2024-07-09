import { HttpClient } from '@angular/common/http';
import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  http = inject(HttpClient) // new way of injecting without constructor
  title = 'New Social App';
  users: any;

  ngOnInit(): void {
      this.http.get('https://localhost:5001/api/users').subscribe({
        next: res => this.users = res,
        error: error => console.log(error),
        complete: () => console.log("Request has completed :)"),
      })
  }
}
