import {Component, OnInit} from '@angular/core';
import {AuthenticationService} from "../../../../core/services/authentication.service";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit {
  profile: StudentProfile = {} as StudentProfile;
  fetchLoading: boolean = false;

  constructor(private authenticationService: AuthenticationService) {
  }

  ngOnInit(): void {
    this.fetchProfile();
  }

  fetchProfile() {
    this.fetchLoading = true;
    this.authenticationService.getStudentProfile().subscribe({
      next: () => {
        this.fetchLoading = false;
      },
      error: () => {
        this.fetchLoading = false;
      }
    });
  }
}
