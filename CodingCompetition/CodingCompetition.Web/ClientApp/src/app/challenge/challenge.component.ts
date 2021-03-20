import { Component, ViewChild, OnInit, AfterViewInit, ElementRef } from '@angular/core';
import { ApiService } from '../shared/api.service';

import 'brace';
import 'brace/mode/csharp';
import 'brace/mode/java';

@Component({
  selector: 'challenge',
  templateUrl: './challenge.component.html',
  styleUrls: ['./challenge.component.css']
})
export class ChallengeComponent implements OnInit {
  themes: string[] = ['ambiance', 'chaos', 'chrome', 'clouds', 'clouds_midnight', 'cobalt', 'crimson_editor', 'dawn', 'dracula', 'dreamweaver', 'eclipse', 'github', 'gob',
    'gruvbox', 'idle_fingers', 'iplastic', 'katzenmilch', 'kr_theme', 'kuroir', 'merbivore', 'merbivore_soft', 'monokai', 'mono_industrial', 'pastel_on_dark', 'solarized_dark',
    'solarized_light', 'sqlserver', 'terminal', 'textmate', 'tomorrow', 'tomorrow_night', 'tomorrow_night_blue', 'tomorrow_night_bright', 'tomorrow_night_eighties',
    'twilight', 'vibrant_ink', 'xcode'];
  theme: string;

  challenges: Challenge[];
  challenge: Challenge;
  template: ChallengeTemplate;
  languages: Language[];
  compileResult: ChallengeResult;

  isRunning = false;
  isSubmitting = false;
  themeLoading = false;

  options: any = { printMargin: false, enableBasicAutocompletion: true };

  @ViewChild('editor') editor;

  constructor(private http: ApiService) {
  }

  ngOnInit(): void {
    this.http.get<Challenge[]>('Challenge').subscribe(result => {
      this.challenges = result;
    });
  }

  loadTheme(theme: string): void {
    this.themeLoading = true;
    import(`brace/theme/${theme}`)
      .then((module) => {
        this.editor.setTheme(theme);
        this.themeLoading = false;
      });
  }
  getLanguageName(template) {
    switch (template.language) {
      case 0:
        return 'C#';
      case 1:
        return 'Java';
    }
  }

  selectChallenge(challenge) {
    this.challenge = challenge;
    this.template = challenge.templates[0];
    this.compileResult = null;
  }

  runSolution(): void {

    const request = {
      challengeId: this.challenge.id,
      language: this.template.language,
      solutionCode: this.template.templateCode
    } as ChallengeSolution;

    this.isRunning = true;
    this.http.post<ChallengeResult>('Challenge/RunSolution', request).subscribe(result => {
      this.compileResult = result;
      this.isRunning = false;
    });
  }

  submitSolution(): void {

    const request = {
      challengeId: this.challenge.id,
      language: this.template.language,
      solutionCode: this.template.templateCode
    } as ChallengeSolution;

    this.isSubmitting = true;
    this.http.post<ChallengeResult>('Challenge/SubmitSolution', request).subscribe(result => {
      this.compileResult = result;
      this.isSubmitting = false;
    });
  }

}
