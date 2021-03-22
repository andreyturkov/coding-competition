export class CodePad {
  sent: string;
  theme: string;
  language: Language;

  constructor(public code: string = '') {
    this.sent = new Date().toISOString();
  }
}
