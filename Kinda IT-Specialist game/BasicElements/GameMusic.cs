
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Game2D.BasicElements;

public static class GameMusic
{
    public static SoundEffect ProblemAppear;

    public static SoundEffect CorrectedProblem;

    public static SoundEffect FailedToCorrect;

    public static SoundEffect BadResult;

    public static SoundEffect NotSoBadResult;

    public static SoundEffect GoodResult;

    public static SoundEffect GreatResult;

    public static SoundEffect ButtonHover;

    public static SoundEffect ButtonClick;

    public static Song MenuMusic;

    public static Song InGameMusic;

    public static void LoadMusicAndSounds(ContentManager content)
    {
        ButtonClick = content.Load<SoundEffect>("Music\\btn click");
        ButtonHover = content.Load<SoundEffect>("Music\\btn hover");

        ProblemAppear = content.Load<SoundEffect>("Music\\problem appear");
        CorrectedProblem = content.Load<SoundEffect>("Music\\correct");
        FailedToCorrect = content.Load<SoundEffect>("Music\\incorrect");
        BadResult = content.Load<SoundEffect>("Music\\too bad");
        NotSoBadResult = content.Load<SoundEffect>("Music\\neutral");
        GoodResult = content.Load<SoundEffect>("Music\\good");
        GreatResult = content.Load<SoundEffect>("Music\\great");
        MenuMusic = content.Load<Song>("Music\\menu");
        InGameMusic = content.Load<Song>("Music\\in game");
    }
}
