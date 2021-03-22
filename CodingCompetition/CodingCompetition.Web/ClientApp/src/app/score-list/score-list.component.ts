import { Component, OnInit } from '@angular/core';
import { ApiService } from '../shared/api.service';
import { ActivatedRoute } from '@angular/router';

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
  displayedColumns: string[] = ['nickname', 'email', 'score', 'totalSubmissions',
    'successfulSubmissions', 'successfulChallenges', 'failedSubmissions', 'failedChallenges'];

  constructor(private http: ApiService, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const top = params['top'] ? params['top'] : 3;
      this.http.get<Player[]>(`Players?top=${top}`).subscribe(result => {
        this.players = result;
      });
    });

  }

  getTotalScore(player: Player): number {
    return 100 * this.getSuccessSubmissions(player).length / player.submissions.length;
  }

  getSuccessSubmissions(player: Player): Solution[] {
    const result = [];
    for (let i = 0; i < player.submissions.length; ++i) {
      if (player.submissions[i].success) {
        result.push(player.submissions[i]);
      }
    }
    return result;
  }

  getFailSubmissions(player: Player): Solution[] {
    const result = [];
    for (let i = 0; i < player.submissions.length; ++i) {
      if (!player.submissions[i].success) {
        result.push(player.submissions[i]);
      }
    }
    return result;
  }
}
