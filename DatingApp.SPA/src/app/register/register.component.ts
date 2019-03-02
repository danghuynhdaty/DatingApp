import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  private baseUrl = 'http://localhost:5000/api/auth/register'

  @Output() cancleRegister = new EventEmitter();

  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      console.log('Registeration successfully');
    }, err => {
      console.log(err);
    });
  }

  cancel() {
    this.cancleRegister.emit(false);
  }
}
