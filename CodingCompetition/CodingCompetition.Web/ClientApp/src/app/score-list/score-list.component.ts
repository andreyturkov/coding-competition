import { Component, OnInit } from '@angular/core';
import { ApiService } from '../shared/api.service';

import 'brace';
import 'brace/mode/csharp';
import 'brace/mode/java';

@Component({
  selector: 'score-list',
  templateUrl: './score-list.component.html',
  styleUrls: ['./score-list.component.css']
})
export class ScoreListComponent implements OnInit {

  players: Player[];
  displayedColumns: string[] = ['nickname', 'email', 'submissions', 'challenges'];

  constructor(private http: ApiService) {
  }

  ngOnInit(): void {
    this.http.get<Player[]>('Players').subscribe(result => {
      this.players = result;
    });
  }

  getPlayerSuccessSubmissions(player: Player): number {
    let count = 0;
    for (let i = 0; i < player.submissions.length; ++i) {
      if (player.submissions[i].success) {
        count++;
      }
    }
    return count;
  }
}
