import { Component, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ApiService } from '../shared/api.service';

import 'brace';
import 'brace/mode/csharp';
import 'brace/mode/java';
import 'brace/theme/iplastic';
import 'brace/theme/eclipse';

import { PlayerPopupComponent } from '../player-popup/player-popup.component';
import { PlayerManager } from '../shared/player.manager';

@Component({
  selector: 'challenge',
  templateUrl: './challenge.component.html',
  styleUrls: ['./challenge.component.css']
})
export class ChallengeComponent implements OnInit {

  themes: string[] = ['ambiance', 'chaos', 'chrome', 'clouds', 'clouds_midnight', 'cobalt', 'crimson_editor', 'dawn', 'dracula', 'dreamweaver', 'eclipse', 'github', 'gob', 'gruvbox',
    'idle_fingers', 'iplastic', 'katzenmilch', 'kr_theme', 'kuroir', 'merbivore', 'merbivore_soft', 'monokai', 'mono_industrial', 'pastel_on_dark', 'solarized_dark', 'solarized_light',
    'sqlserver', 'terminal', 'textmate', 'tomorrow', 'tomorrow_night', 'tomorrow_night_blue', 'tomorrow_night_bright', 'tomorrow_night_eighties', 'twilight', 'vibrant_ink', 'xcode'];

  languageMap = new Map([
    [Language.CSharp, { languageName: 'C#', editorTheme: 'iplastic', editorMode: 'csharp' }],
    [Language.Java, { languageName: 'Java', editorTheme: 'eclipse', editorMode: 'java' }]
  ]);
  defaultTheme: string;
  selectedTheme: string;
  defaultMode: string;
  selectedMode: string;
  challenge: Challenge;
  selectedTemplate: Template;
  languages: Language[];
  compileResult: Result;
  failedTests: CompetitionTestResult[];
  isRunning = false;
  isSubmitting = false;
  themeLoading = false;
  options: any = { printMargin: false, enableBasicAutocompletion: true };

  @ViewChild('editor') editor;

  constructor(
    private http: ApiService,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private playerManager: PlayerManager) {
  }

  ngOnInit(): void {

    this.route.params.subscribe(params => {
      const challengeId = params['id'];
      this.http.get<Challenge>(`Challenge/${challengeId}`).subscribe(result => {
        this.challenge = result;
        this.selectedTemplate = this.challenge.templates[0];
        const langConfig = this.languageMap.get(this.selectedTemplate.language);
        this.defaultTheme = langConfig.editorTheme;
        this.defaultMode = langConfig.editorMode;
        this.updateEditor();
      });
    });
  }

  loadEditorTheme(theme: string): void {
    this.themeLoading = true;
    import(`brace/theme/${theme}`).then(() => {
      if (this.editor) {
        this.editor.setTheme(theme);
      }
      this.themeLoading = false;
    });
  }

  loadEditorMode(mode: string): void {
    import(`brace/mode/${mode}`).then(() => {
      if (this.editor) {
        this.editor.setMode(mode);
      }
    });
  }

  getLanguageName(template: Template): string {
    const langConfig = this.languageMap.get(template.language);
    return langConfig.languageName;
  }

  updateEditor(): void {
    const langConfig = this.languageMap.get(this.selectedTemplate.language);
    this.selectedTheme = langConfig.editorTheme;
    this.selectedMode = langConfig.editorMode;
    this.loadEditorTheme(this.selectedTheme);
    this.loadEditorMode(this.selectedMode);
  }

  runSolution(): void {

    const request = {
      challengeId: this.challenge.id,
      language: this.selectedTemplate.language,
      solutionCode: this.selectedTemplate.templateCode
    } as RunRequest;

    this.isRunning = true;
    this.http.post<Result>('Competition/RunSolution', request).subscribe(result => {
      this.setCompileResult(result);
      this.isRunning = false;
    });
  }

  openPlayerDialog(): void {
    const dialogRef = this.dialog.open(PlayerPopupComponent, { width: '250px' });

    dialogRef.afterClosed().subscribe(() => {
      this.submitSolution();
    });
  }

  submitSolution(): void {

    if (!this.playerManager.hasPlayer) {
      return;
    }

    const request = {
      challengeId: this.challenge.id,
      language: this.selectedTemplate.language,
      solutionCode: this.selectedTemplate.templateCode,
      player: { nickname: this.playerManager.nickname, email: this.playerManager.email } as Player
    } as SubmitRequest;

    this.isSubmitting = true;
    this.http.post<Result>('Competition/SubmitSolution', request).subscribe(result => {
      this.setCompileResult(result);
      this.isSubmitting = false;
    });
  }

  private setCompileResult(result: Result): void {
    this.compileResult = result;
    this.failedTests = this.compileResult.testResults.filter(x => !x.success);
  }

  log(event, txt) {
    console.clear();
    console.log(txt);
    console.log(event);
  }
}
