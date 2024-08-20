import { Component, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  usersFromHomeComponent = input.required<any>() // parent to child
  cancelRegister = output<boolean>(); // child to parent, executed when cancel() is called
  model: any = {};

  register() {
    console.log(this.model);
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
