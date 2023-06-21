using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2D.BasicElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2D.UI;

public static class GameUICreator
{
    private static List<Component> BuildSimpleInterface(List<Button> buttons, List<EventHandler> actions,
            List<Component> additional)
    {
        var components = new List<Component>()
            {
                new Sprite(GameTextures.Background, Vector2.Zero, Vector2.One, SpriteEffects.None, false)
            };

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Click += actions[i];
            components.Add(buttons[i]);
        }

        foreach (var comp in additional)
            components.Add(comp);

        return components;
    }

    private static Button CreateButton(Vector2 delta, string labelText)
    {
        return new Button(GameTextures.ButtonTexture, new Vector2(0, 0), Vector2.One,
            SpriteEffects.None, delta,
            new Label(null, Vector2.One, Vector2.One * 1.5f, SpriteEffects.None, USE_Game.StandardFont, Color.Black, Vector2.Zero, labelText),
            Color.White);
    }

    private static Label CreateLabel(Vector2 scale, float xDelta, float yDelta, string text)
    {
        return new Label(null, Vector2.Zero, scale, SpriteEffects.None, USE_Game.StandardFont, Color.Yellow,
            new Vector2(xDelta, yDelta), text);
    }

    public static List<Component> BuildMenu()
    {
        var buttons = new List<Button>()
            {
                CreateButton(new Vector2(0, -40), "Start"),
                CreateButton(new Vector2(0, 40), "Guide"),
                CreateButton(new Vector2(0, 120), "Quit")
            };
        var title = CreateLabel(Vector2.One * 4, 0, -USE_Game.ScreenHeight / 2 + 70, "Once at informatics exam");
        var actions = new List<EventHandler>() { StartButton_Click, GuideButton_Click, QuitButton_Click };
        return BuildSimpleInterface(buttons, actions, new List<Component>() { title });
    }

    public static List<Component> BuildPause()
    {
        var buttons = new List<Button>()
            {
                CreateButton(new Vector2(0, -100), "Continue"),
                CreateButton(new Vector2(0, -20), "Restart"),
                CreateButton(new Vector2(0, 60), "Go to menu"),
                CreateButton(new Vector2(0, 140), "Quit")
            };
        var actions = new List<EventHandler>() { ContinueButton_Click, RestartButton_Click, MenuButton_Click, QuitButton_Click };
        return BuildSimpleInterface(buttons, actions, new List<Component>());
    }

    public static List<Component> BuildEnding()
    {
        var buttons = new List<Button>()
            {
                CreateButton(new Vector2(0, -40), "Restart"),
                CreateButton(new Vector2(0, 40), "Go to menu"),
                CreateButton(new Vector2(0, 120), "Quit")
            };

        var actions = new List<EventHandler>() { RestartButton_Click, MenuButton_Click, QuitButton_Click };
        var resultMessage = new ResultMessage(null, new Vector2(0, 0), new Vector2(3, 3), SpriteEffects.None,
            USE_Game.StandardFont, Color.White, new Vector2(0, -USE_Game.ScreenHeight / 2 + 120));
        return BuildSimpleInterface(buttons, actions, new List<Component>() { resultMessage });
    }

    private static List<(Sprite image, Label title, Label livesTakeAmount, Label amountAppearance, Label timeAppearance)> CreateGuideData()
    {
        return new List<(Sprite image, Label title, Label livesTakeAmount, Label amountAppearance, Label timeAppearance)>()
        {
            CreateProblemDescription(GameTextures.NoExam, new Vector2(50, 150), "Can't start exam", "ALL", "Once", "Only at start"),
            CreateProblemDescription(GameTextures.NoInterpreter, new Vector2(300, 150), "No interpreter", "ALL", "Once", "Only at start"),
            CreateProblemDescription(GameTextures.NoPrograms, new Vector2(550, 150), "No programs", "ALL", "Once", "Only at start"),
            CreateProblemDescription(GameTextures.NotActivated, new Vector2(800, 150), "Not activated program", "ALL", "Once", "Any time"),
            CreateProblemDescription(GameTextures.StuckPC, new Vector2(1050, 150), "Stuck PC", "1", "Infinity", "Any time"),
            CreateProblemDescription(GameTextures.DeadPrinter, new Vector2(50, 390), "Broken printer", "1", "Infinity", "Any time"),
            CreateProblemDescription(GameTextures.DeadKeyboard, new Vector2(300, 390), "Broken keyboard", "1", "Infinity", "Any time"),
            CreateProblemDescription(GameTextures.DeadMonitor, new Vector2(550, 390), "Disabled monitor", "1", "Infinity", "Any time"),
            CreateProblemDescription(GameTextures.DeadMouse, new Vector2(800, 390), "Broken mouse", "1", "Infinity", "Any time"),
            CreateProblemDescription(GameTextures.ProblemWritingDownNotify, new Vector2(1050, 390), "Additional activity", "0", "Infinity", "After solving"),
        };
    }

    private static (Sprite image, Label title, Label livesTakeAmount, Label amountAppearance, Label timeAppearance) CreateProblemDescription(
        Texture2D image, Vector2 position, string title, string livesTakeAmount, string amountAppearance, string timeAppearance)
    {
        var problemSprite = new Sprite(image, position, Vector2.One * 0.5f, SpriteEffects.None);

        var titleProblem = CreateLabel(Vector2.One, -USE_Game.ScreenWidth / 2 + position.X + 75,
            -USE_Game.ScreenHeight / 2 + position.Y - 20, title);
        var takenLivesAmount = CreateLabel(Vector2.One, -USE_Game.ScreenWidth / 2 + position.X + 75,
            -USE_Game.ScreenHeight / 2 + position.Y + 100, "Takes lives: " + livesTakeAmount);
        var appearanceAmount = CreateLabel(Vector2.One, -USE_Game.ScreenWidth / 2 + position.X + 75,
            -USE_Game.ScreenHeight / 2 + position.Y + 130, "Amount in game: " + amountAppearance);
        var appearanceTime = CreateLabel(Vector2.One, -USE_Game.ScreenWidth / 2 + position.X + 75,
            -USE_Game.ScreenHeight / 2 + position.Y + 160, "Appears at: " + timeAppearance);

        return (problemSprite, titleProblem, takenLivesAmount, appearanceAmount, appearanceTime);
    }

    private static void BuildGuideAdditionalComponents(List<Component> components)
    {
        components.Add(new Label(null, Vector2.Zero, Vector2.One * 1.3f, SpriteEffects.None, USE_Game.StandardFont, Color.White,
            new Vector2(-USE_Game.ScreenWidth / 2 + 670, -USE_Game.ScreenHeight / 2 + 60),
            "You play as IT Specialist at informatics exam. Your task is to help people with problems through all the time." +
            "\nDon't mess up! Try to solve all the problems, and remember, you have only 5 lives and might be punished!" +
            "\nDon't forget to write down solved problems (last picture) or you lose 0.002% of the score."));

        var data = CreateGuideData();

        foreach (var obj in data)
        {
            (var image, var title, var lives, var amount, var time) = obj;
            components.Add(image);
            components.Add(title);
            components.Add(lives);
            components.Add(amount);
            components.Add(time);
        }
    }

    public static List<Component> BuildGuide()
    {
        var buttons = new List<Button>()
        {
            CreateButton(new Vector2(0, USE_Game.ScreenHeight / 2 - 80), "Go back")
        };

        var actions = new List<EventHandler>() { BackFromGuideButton_Click };
        var guideAdditionalComponents = new List<Component>();
        BuildGuideAdditionalComponents(guideAdditionalComponents);
        return BuildSimpleInterface(buttons, actions, guideAdditionalComponents);
    }

    public static void AddUIToGameProcess(List<Component> components)
    {
        var timer = new Timer(null, new Vector2(150, 150), new Vector2(2.2f, 2.2f), SpriteEffects.None,
           USE_Game.StandardFont, Color.MediumVioletRed, new Vector2(20, 20));
        var score = new GameScore(null, new Vector2(0, 0), new Vector2(2.2f, 2.2f), SpriteEffects.None,
            USE_Game.StandardFont, Color.LightGoldenrodYellow, new Vector2(20, 70));
        var remainedLives = new Lives(null, new Vector2(0, 0), new Vector2(2.2f, 2.2f), SpriteEffects.None,
            USE_Game.StandardFont, Color.NavajoWhite, new Vector2(20, 120));
        var controlsInfo = new ControlsInfoLabel(null, new Vector2(0, 0), new Vector2(1.5f, 1.5f), SpriteEffects.None,
            USE_Game.StandardFont, Color.NavajoWhite, new Vector2(20, 120));
        var staminaBar = new StaminaBar(GameTextures.StaminaBar, Vector2.Zero, Vector2.One, SpriteEffects.None);

        components.Add(timer);
        components.Add(score);
        components.Add(remainedLives);
        components.Add(controlsInfo);
        components.Add(staminaBar);
    }

    private static void StartButton_Click(object sender, EventArgs e)
    {
        GameStateData.NeedToStartGame = true;
    }

    private static void RestartButton_Click(object sender, EventArgs e)
    {
        GameStateData.NeedToStartGame = true;
    }

    private static void ContinueButton_Click(object sender, EventArgs e)
    {
        GameStateData.Paused = false;
    }

    private static void QuitButton_Click(object sender, EventArgs e)
    {
        GameStateData.Quit = true;
    }

    private static void GuideButton_Click(object sender, EventArgs e)
    {
        GameStateData.ShowGuide = true;
    }

    private static void BackFromGuideButton_Click(object sender, EventArgs e)
    {
        GameStateData.ShowGuide = false;
    }

    private static void MenuButton_Click(object sender, EventArgs e)
    {
        if (GameStateData.GameOver) GameStateData.GameOver = false;
        if (GameStateData.Paused) GameStateData.Paused = false;
        GameStateData.MenuStateRequired = true;
    }
}