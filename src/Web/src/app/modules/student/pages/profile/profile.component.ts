import {Component, OnInit} from '@angular/core';
import {AuthenticationService} from "../../../../core/services/authentication.service";
import {StudentProfile} from "../../../../core/models/user";
import {AlertService} from "../../../../core/services/alert.service";
import {group} from "@angular/animations";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit {
  profile: StudentProfile = {} as StudentProfile;
  fetchLoading: boolean = false;

  constructor(private authenticationService: AuthenticationService,
              private alertService: AlertService) { }

  ngOnInit(): void {
    this.fetchProfile();
  }

  fetchProfile() {
    this.fetchLoading = true;
    this.authenticationService.getStudentProfile().subscribe({
        next: (profile) => {
          this.profile = profile;
          console.log(this.profile);
          this.fetchLoading = false;
        },
        error: (err) => {
          this.alertService.error(err);
          this.fetchLoading = false;
        }
      });
  }
}
