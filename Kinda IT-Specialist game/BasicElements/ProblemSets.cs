

using System.Collections.Generic;

namespace Game2D.BasicElements;

public static class ProblemSets
{
    public static List<Problem> PupilProblems = new List<Problem>()
    {
        new Problem(GameTextures.DeadKeyboard, 20, int.MaxValue, false, false),
        new Problem(GameTextures.DeadMouse, 20, int.MaxValue, false, false),
        new Problem(GameTextures.DeadMonitor, 20, int.MaxValue, false, false),
        new Problem(GameTextures.StuckPC, 25, int.MaxValue, false, false),
        new Problem(GameTextures.NoExam, 30, 1, true, true),
        new Problem(GameTextures.NoInterpreter, 30, 1, true, true),
        new Problem(GameTextures.NoPrograms, 30, 1, true, true),
        new Problem(GameTextures.NotActivated, 40, 1, true, false)
    };

    public static List<Problem> TeacherProblems = new List<Problem>()
    {
        new Problem(GameTextures.DeadPrinter, 20, int.MaxValue, false, false),
    };

    public static List<Problem> MainCharacterProblems = new List<Problem>()
    {
        new Problem(GameTextures.ProblemWritingDownNotify, 0, int.MaxValue, false, false),
    };
}
