import { Component } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { PlayerManager } from '../shared/player.manager';

@Component({
  selector: 'player-popup',
  templateUrl: './player-popup.component.html',
  styleUrls: ['./player-popup.component.css']
})
export class PlayerPopupComponent {

  draftData: any;

  constructor(private playerManager: PlayerManager,
    public dialogRef: MatDialogRef<PlayerPopupComponent>) { }

  ngOnInit(): void {
    this.draftData = {
      nickname: this.playerManager.nickname,
      email: this.playerManager.email
    };
    this.dialogRef.backdropClick().subscribe(e => {
      this.close();
    });
  }

  save() {
    if (this.draftData.nickname && this.draftData.email) {
      this.playerManager.setPlayer(this.draftData.nickname, this.draftData.email);
    }
    this.close();
  }

  close(): void {
    this.dialogRef.close();
  }
}
