import { Injectable } from '@angular/core';

@Injectable()
export class PlayerManager {
  nickname: string;
  email: string;

  get hasPlayer(): boolean {
    if (this.nickname && this.email) {
      return true;
    }
    return false;
  }

  setPlayer(nickname: string, email: string): void {
    this.nickname = nickname;
    this.email = email;
  }

}
