using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game2D.BasicElements;

public static class GameTextures
{
    public static Texture2D ButtonTexture;
    public static Texture2D Background;
    public static Texture2D StaminaBar;

    public static Texture2D Map;

    public static Texture2D MainCharacterSprite;
    public static Texture2D MainCharacterSpritesheet;
    public static Texture2D Teacher;
    public static Texture2D PupilOrangeMan;
    public static Texture2D PupilBlueMan;
    public static Texture2D PupilGreenWoman;
    public static Texture2D PupilBlueWoman;

    public static Texture2D TableCol;
    public static Texture2D FlowerCol;
    public static Texture2D HorizontalWall;
    public static Texture2D VerticalWall;

    public static Texture2D NoExam;
    public static Texture2D DeadPrinter;
    public static Texture2D DeadKeyboard;
    public static Texture2D DeadMonitor;
    public static Texture2D DeadMouse;
    public static Texture2D NoInterpreter;
    public static Texture2D NoPrograms;
    public static Texture2D NotActivated;
    public static Texture2D StuckPC;
    public static Texture2D ProblemWritingDownNotify;

    public static void LoadTextures(ContentManager content)
    {
        LoadMainTextures(content);
        LoadColliders(content);
        LoadProblems(content);
    }

    private static void LoadMainTextures(ContentManager content)
    {
        Background = content.Load<Texture2D>("Images\\cosmos");
        ButtonTexture = content.Load<Texture2D>("Images\\yellow button");
        StaminaBar = content.Load<Texture2D>("Images\\stamina bar");

        Map = content.Load<Texture2D>("Images\\newest map");
        MainCharacterSprite = content.Load<Texture2D>("Characters\\only_man-transformed");
        MainCharacterSpritesheet = content.Load<Texture2D>("Images\\second player sprites");
        Teacher = content.Load<Texture2D>("Characters\\teacher");
        PupilOrangeMan = content.Load<Texture2D>("Characters\\only_citizen1-transformed");
        PupilBlueMan = content.Load<Texture2D>("Characters\\only_citizen2-transformed");
        PupilGreenWoman = content.Load<Texture2D>("Characters\\only_citizen3-transformed");
        PupilBlueWoman = content.Load<Texture2D>("Characters\\only_citizen4-transformed");
    }

    private static void LoadColliders(ContentManager content)
    {
        TableCol = content.Load<Texture2D>("Colliders\\table_col");
        FlowerCol = content.Load<Texture2D>("Colliders\\flower");
        HorizontalWall = content.Load<Texture2D>("Colliders\\horizontal_col");
        VerticalWall = content.Load<Texture2D>("Colliders\\vertical_col");
    }

    private static void LoadProblems(ContentManager content)
    {
        NoExam = content.Load<Texture2D>("Problems\\can't start exam");
        DeadPrinter = content.Load<Texture2D>("Problems\\dead printer");
        DeadKeyboard = content.Load<Texture2D>("Problems\\kb dead");
        DeadMonitor = content.Load<Texture2D>("Problems\\monitor dead");
        DeadMouse = content.Load<Texture2D>("Problems\\mouse dead");
        NoInterpreter = content.Load<Texture2D>("Problems\\no interpeter");
        NoPrograms = content.Load<Texture2D>("Problems\\no programs");
        NotActivated = content.Load<Texture2D>("Problems\\not activated");
        StuckPC = content.Load<Texture2D>("Problems\\pc stuck");
        ProblemWritingDownNotify = content.Load<Texture2D>("Problems\\write down the problem");
    }
}
