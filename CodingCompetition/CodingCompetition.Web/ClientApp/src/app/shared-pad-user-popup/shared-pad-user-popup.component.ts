import { Component } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'shared-pad-user-popup',
  templateUrl: './shared-pad-user-popup.component.html',
  styleUrls: ['./shared-pad-user-popup.component.css']
})
export class SharedPadUserPopupComponent {

  draftData: any;

  constructor(public dialogRef: MatDialogRef<SharedPadUserPopupComponent>) { }

  ngOnInit(): void {

    this.draftData = {
      nickname: localStorage.getItem('shared-pad-user'),
    };
    this.dialogRef.backdropClick().subscribe(e => {
      this.close();
    });
  }

  save() {
    if (this.draftData.nickname) {
      localStorage.setItem('shared-pad-user', this.draftData.nickname);
      this.close();
    }
  }

  close(): void {
    this.dialogRef.close();
  }
}
