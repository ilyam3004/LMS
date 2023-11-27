import {Component, Input} from '@angular/core';
import {NgbActiveModal} from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: 'app-confirmation-modal',
  templateUrl: './confirmation-modal.component.html',
  styleUrl: './confirmation-modal.component.scss'
})
export class ConfirmationModalComponent {
  @Input() title: string = '';
  @Input() message: string = '';
  @Input() isWarning: boolean = false;
  @Input() warningMessage: string = 'This operation cannot be undone.';

  constructor(public modal: NgbActiveModal) {}

  confirm(): void {
    this.modal.close(true);
  }

  cancel(): void {
    this.modal.dismiss(false);
  }
}
