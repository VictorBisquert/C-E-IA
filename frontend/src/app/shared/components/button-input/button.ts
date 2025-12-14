import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-button',
  imports: [CommonModule],
  templateUrl: './button.html',
  styleUrl: './button.css',
})
export class Button {
  @Input() label: string = 'Botón';
  @Input() type: 'primary' | 'success' | 'danger' | 'warning' | 'default' = 'primary';
  @Input() disabled = false;

  onClick() {
    if (!this.disabled) {
      console.log('botón guardar pulsado!');
    }
  }
}
