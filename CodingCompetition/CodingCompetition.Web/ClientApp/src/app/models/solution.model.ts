class Solution {
  id: number;
  playerId: number;
  challengeId: number;
  language: Language;
  solutionCode: string;
  success: boolean;
  message: string;
  runtime: string;
  warnings: string;
  errors: string;

  player: Player;
  challenge: Challenge;
  testResults: TestResult[];
}
