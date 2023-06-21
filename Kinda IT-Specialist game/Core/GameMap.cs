using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2D.BasicElements;
using Game2D.Characters;
using Game2D.Different;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game2D.Core;

public static class GameMap
{
    public static List<Component> CreateFunctioningLevel()
    {
        var components = new List<Component>()
            {
                new Sprite(GameTextures.Map, Vector2.Zero, Vector2.One, SpriteEffects.None, false),
            };

        var player = new MainPlayer(GameTextures.MainCharacterSprite, GameTextures.MainCharacterSpritesheet,
            250f, new Vector2(738, 61), new Vector2(0.6f, 0.6f));
        USE_Game.Controller.SetPlayer(player);
        components.Add(player);
        var problematicCharacters = CreateAdditionalCharacters();
        player.SetProblematicCharacters(problematicCharacters);
        components.AddRange(problematicCharacters);
        var colliders = CreateColliders(problematicCharacters.Take(problematicCharacters.Count-2).ToList());
        player.LoadColliders(colliders);
        return components;
    }

    private static void CreatePupils(List<Component> components, Texture2D[] textures, int[] xValues, int[] yValues)
    {
        for (int i = 0; i < yValues.Length; i++)
        {
            for (int j = 0; j < xValues.Length; j++)
            {
                var texture = textures[USE_Game.Random.Next(0, textures.Length)];
                components.Add(new Pupil(texture, new Vector2(xValues[j], yValues[i]), new Vector2(1.5f, 1.5f), SpriteEffects.None));
            }
        }
    }

    private static List<Component> CreateAdditionalCharacters()
    {
        var textures = new Texture2D[]
        {
            GameTextures.PupilBlueMan,
            GameTextures.PupilOrangeMan,
            GameTextures.PupilGreenWoman,
            GameTextures.PupilBlueWoman
        };
        var xValues = new int[] { 556, 677, 807, 935, 1076, 1230 };
        var yValues = new int[] { 166, 268, 375, 481, 586, 691, 798 };
        var problemCharacters = new List<Component>();
        CreatePupils(problemCharacters, textures, xValues, yValues);
        problemCharacters.Add(new Teacher(GameTextures.Teacher, new Vector2(1055, 38), new Vector2(0.28f, 0.28f), SpriteEffects.None));
        problemCharacters.Add(new MainCharacterPlace(null, new Vector2(745, 61), Vector2.Zero, SpriteEffects.None));
        return problemCharacters;
    }

    private static List<Component> CreateColliders(List<Component> pupils)
    {
        var colliders = new List<Component>();
        foreach (var pupil in pupils)
        {
            colliders.Add(pupil);
            var pupilSprite = (Sprite)pupil;
            colliders.Add(new InvisibleCollider(GameTextures.TableCol, pupilSprite.Position + new Vector2(-17, -21), Vector2.One, SpriteEffects.None));
        }
        colliders.Add(new InvisibleCollider(GameTextures.HorizontalWall, new Vector2(467, 42), Vector2.One, SpriteEffects.None));
        colliders.Add(new InvisibleCollider(GameTextures.HorizontalWall, new Vector2(466, 849), Vector2.One, SpriteEffects.None));
        colliders.Add(new InvisibleCollider(GameTextures.VerticalWall, new Vector2(461, 42), Vector2.One, SpriteEffects.None));
        colliders.Add(new InvisibleCollider(GameTextures.VerticalWall, new Vector2(1359, 42), Vector2.One, SpriteEffects.None));
        colliders.Add(new InvisibleCollider(GameTextures.FlowerCol, new Vector2(481, 26), Vector2.One, SpriteEffects.None));
        colliders.Add(new InvisibleCollider(GameTextures.FlowerCol, new Vector2(1335, 27), Vector2.One, SpriteEffects.None));
        colliders.Add(new InvisibleCollider(GameTextures.FlowerCol, new Vector2(477, 807), Vector2.One, SpriteEffects.None));
        colliders.Add(new InvisibleCollider(GameTextures.FlowerCol, new Vector2(1335, 807), Vector2.One, SpriteEffects.None));
        colliders.Add(new InvisibleCollider(GameTextures.TableCol, new Vector2(776, 59), Vector2.One, SpriteEffects.None));
        colliders.Add(new InvisibleCollider(GameTextures.TableCol, new Vector2(1048, 57), Vector2.One, SpriteEffects.None));
        return colliders;
    }
}
