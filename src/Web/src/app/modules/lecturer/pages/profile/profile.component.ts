import {Component} from '@angular/core';
import {LecturerProfile} from "../../../../core/models/user";
import {AuthenticationService} from "../../../../core/services/authentication.service";
import {AlertService} from "../../../../core/services/alert.service";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent {
  profile: LecturerProfile = {} as LecturerProfile;
  fetchLoading: boolean = false;

  constructor(private authenticationService: AuthenticationService,
              private alertService: AlertService) {
  }

  ngOnInit(): void {
    this.fetchProfile();
  }

  fetchProfile() {
    this.fetchLoading = true;
    this.authenticationService.getLecturerProfile().subscribe({
      next: (profile) => {
        this.profile = profile;
        this.fetchLoading = false;
      },
      error: (err) => {
        this.alertService.error(err);
        this.fetchLoading = false;
      }
    });
  }
}
