using System;

namespace Game2D.BasicElements;

public static class GameStateData
{
    public static double BadScorePercentage => 20;
    public static double NotSoBadScorePercentage => 65;
    public static double AverageScorePercentage => 90;

    public static string FiredText = "You're fired!";

    public static string ReplacementText = "Not so bad.\n But should've been better";

    public static string GreatPotentialText = "Good job! You have great potential\n to become the best.";

    public static string LimitlessPotentialText = "Amazing work over here!\n You've got limitless potential!";

    public static float MinTimeBetweenProblems => 2;
    public static float MaxTimeBetweenProblems => 14;
    public static float ProblemAddChance => 0.05f;
    public static float ChanceIncreasingInterval => 20;

    public static float ProblemChance { get; set; }
    public static bool GameOver { get; set; }
    public static bool MenuStateRequired { get; set;  }
    public static bool ShowGuide { get; set; }
    public static bool Quit { get; set; }

    public static bool NeedToStartGame { get; set; }

    public static bool Paused { get; set; }

    public static bool IsDelayBeforeSwitchingStatesActive { get; set; }

    public static int ResultScore { get; set; }

    public static int IdealScore { get; set; }

    public static int Lives { get; set; }

    public static double GameSeconds = 235;

    public static double RemainedSeconds { get; set; }

    public static void ResetAllInfo()
    {
        GameOver = false;
        Paused = false;
        IsDelayBeforeSwitchingStatesActive = false;
        ResultScore = 0;
        IdealScore = 0;
        Lives = 5;
        RemainedSeconds = GameSeconds;
        ProblemChance = 0.35f;
    }
}
