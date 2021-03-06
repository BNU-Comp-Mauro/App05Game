using System;
using System.Collections.Generic;
using System.Text;
using App05MonoGame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace App05MonoGame.Controllers
{
    /// <summary>
    /// This class handles the control of the player,
    /// setting their start position in the game and
    /// the different animations that play when they
    /// press on an arrow key.
    /// </summary>
    public class PlayerController
    {
        public AnimatedPlayer Player { get; set; }

        public Vector2 StartPosition { get; set; }

        public PlayerController()
        {
            StartPosition = new Vector2(200, 200);
        }

        public AnimatedPlayer CreatePlayer(GraphicsDevice graphics,
            Texture2D playerSheet)
        {
            AnimationController controller = new
                AnimationController(graphics, playerSheet, 4, 3);

            string[] keys = new string[] { "Down", "Left", "Right", "Up" };
            controller.CreateAnimationGroup(keys);

            Player = new AnimatedPlayer()
            {
                CanWalk = true,
                Scale = 2.0f,

                Rotation = MathHelper.ToRadians(0),
                RotationSpeed = 0f,
            };

            controller.AppendAnimationsTo(Player);

            StartPlayer();
            return Player;
        }
        public void RemovePlayer()
        {
            Player.IsActive = false;
            Player.IsAlive = false;
            Player.IsVisible = false;
        }

        public void StartPlayer()
        {
            Player.Position = StartPosition;
            Player.Speed = 200;
            Player.Direction = new Vector2(1, 0);
        }
    }
}