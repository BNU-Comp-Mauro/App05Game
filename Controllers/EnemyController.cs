﻿using App05MonoGame.Helpers;
using App05MonoGame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace App05MonoGame.Controllers
{
    public class EnemyController
    {
        public AnimatedSprite Enemy { get; set; }

        public AnimatedSprite Player { get; set; }

        private double maxTime;
        private double timer;

        public EnemyController()
        {
            maxTime = 2.0;
            timer = maxTime;
        }
        public AnimatedSprite CreateEnemy(GraphicsDevice graphics, Texture2D enemySheet)
        {
            AnimationController controller = new AnimationController(graphics, enemySheet, 4, 3);

            string[] keys = new string[] { "Down", "Left", "Right", "Up" };
            controller.CreateAnimationGroup(keys);

            Enemy = new AnimatedSprite()
            {
                Scale = 2.0f,

                Position = new Vector2(1000, 200),
                Direction = new Vector2(-1, 0),
                Speed = 50,

                Rotation = MathHelper.ToRadians(0),
            };

            controller.AppendAnimationsTo(Enemy);
            Enemy.PlayAnimation("Left");

            return Enemy;
        }

        public void StartEnemy()
        {
            Enemy.Position = new Vector2(1000, 200);
            Enemy.Direction = new Vector2(-1, 0);
            Enemy.Speed = 100;
        }

        public void Update(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;

            double fieldOfView = Vector2.Distance(Player.Position, Enemy.Position);

            //Enemy can see player in field of view.
            if (fieldOfView < 250)
            {
                Vector2 p1 = Player.Position;
                Enemy.Direction = p1 - Enemy.Position;
            }
            else if (timer < 0)
            {
                int x = RandomNumber.Generator.Next(3) - 1;
                int y = RandomNumber.Generator.Next(3) - 1;

                Enemy.Direction = new Vector2(x, y);
                timer = maxTime;
            }

            Enemy.Update(gameTime);
        }
    }
}
