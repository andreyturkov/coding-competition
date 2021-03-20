class ChallengeResult {
  id: number;
  runtime: string;
  success: string;
  warnings: string;
  errors: string;
  message: string;
  solution: ChallengeSolution;
  failedTests: ChallengeTestResult[];

}
