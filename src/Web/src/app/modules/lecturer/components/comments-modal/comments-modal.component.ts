import {Component, Input} from '@angular/core';
import {NgbActiveModal} from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: 'app-comments-modal',
  templateUrl: './comments-modal.component.html',
  styleUrl: './comments-modal.component.scss'
})
export class CommentsModalComponent {
  @Input() comments: Comment[] = [];
  newComment: Comment = { user: 'User123', text: '' };

  constructor(public activeModal: NgbActiveModal) {}

  addComment() {
    if (this.newComment.text.trim() !== '') {
      this.newComment.timestamp = new Date();
      this.comments.push({ ...this.newComment });
      this.newComment.text = '';
    }
  }
}

interface Comment {
  user: string;
  text: string;
  timestamp?: Date;
}
