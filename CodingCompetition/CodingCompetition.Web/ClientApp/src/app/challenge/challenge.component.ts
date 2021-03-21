import { Component, ViewChild, OnInit, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
  selectedTheme: string;
  challenge: Challenge;
  selectedTemplate: Template;
  languages: Language[];
  compileResult: Result;
  isRunning = false;
  isSubmitting = false;
  themeLoading = false;
  options: any = { printMargin: false, enableBasicAutocompletion: true };

  @ViewChild('editor') editor;

  constructor(private http: ApiService, private route: ActivatedRoute) {
  }

  ngOnInit(): void {

    this.route.params.subscribe(params => {
      const challengeId = params['id'];
      this.http.get<Challenge>(`Challenge/${challengeId}`).subscribe(result => {
        this.challenge = result;
        this.selectedTemplate = this.challenge.templates[0];
      });
    });
  }

  loadTheme(theme: string): void {
    this.themeLoading = true;
    import(`brace/theme/${theme}`).then((module) => {
      this.editor.setTheme(theme);
      this.themeLoading = false;
    });
  }

  getLanguageName(template): string {
    switch (template.language) {
      case 0:
        return 'C#';
      case 1:
        return 'Java';
    }
    return null;
  }

  getEditorMode(): string {
    switch (this.selectedTemplate.language) {
      case 0:
        return 'csharp';
      case 1:
        return 'java';
    }
    return null;
  }

  runSolution(): void {

    const request = {
      challengeId: this.challenge.id,
      language: this.selectedTemplate.language,
      solutionCode: this.selectedTemplate.templateCode
    } as RunRequest;

    this.isRunning = true;
    this.http.post<Result>('Competition/RunSolution', request).subscribe(result => {
      this.compileResult = result;
      this.isRunning = false;
    });
  }

  submitSolution(): void {

    const request = {
      challengeId: this.challenge.id,
      language: this.selectedTemplate.language,
      solutionCode: this.selectedTemplate.templateCode,
      player: { nickname: 'andrew', email: 'andrew@turkov.com' } as Player
    } as SubmitRequest;

    this.isSubmitting = true;
    this.http.post<Result>('Competition/SubmitSolution', request).subscribe(result => {
      this.compileResult = result;
      this.isSubmitting = false;
    });
  }
}
