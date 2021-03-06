using System;
using System.Collections.Generic;
using System.Text;
using App05MonoGame.Helpers;
using App05MonoGame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace App05MonoGame.Controllers
{
    /// <summary>
    /// This class handles the control of enemy sprites in
    /// the game. They act as non-playable characters (NPCs)
    /// that will chase after the player if they walk into
    /// their set field of view.
    /// </summary>
    public class EnemyController
    {
        public AnimatedSprite Enemy { get; set; }
        public AnimatedPlayer Player { get; set; }

        private double maxTime;
        private double timer;

        public EnemyController()
        {
            maxTime = 2.0;
            timer = maxTime;
        }

        /// <summary>
        /// Creates the enemy sprite, giving it a walking direction, starting position, and speed.
        /// </summary>
        public AnimatedSprite CreateEnemy(GraphicsDevice graphics,
            Texture2D enemySheet)
        {
            AnimationController controller = new
                AnimationController(graphics, enemySheet, 4, 3);

            string[] keys = new string[] { "Down", "Left", "Right", "Up" };
            controller.CreateAnimationGroup(keys);

            Enemy = new AnimatedSprite()
            {
                Scale = 2.0f,

                Position = new Vector2(1000, 200),
                Direction = new Vector2(-1, 0),
                Speed = 150,

                Rotation = MathHelper.ToRadians(0),
            };

            controller.AppendAnimationsTo(Enemy);
            Enemy.PlayAnimation("Left");

            return Enemy;
        }

        /// <summary>
        /// Resets the enemy to their starting position, direction
        /// and speed. If the player has collided with them at the
        /// enemy's starting position, the enemy will be moved
        /// elsewhere. 
        /// </summary>
        public void StartEnemy()
        {
            double fieldOfView = Vector2.Distance(Player.Position,
                Enemy.Position);

            if (fieldOfView < 500)
            {
                Enemy.Position = new Vector2(850, 100);
            }
            else
            {
                Enemy.Position = new Vector2(1000, 200);
            }

            Enemy.Direction = new Vector2(-1, 0);
            Enemy.Speed = 100;
        }

        /// The enemy will be removed if the player has won the game 
        /// (score reaches 800) to avoid hitting them while they're 
        /// reading the on-screen message.
        public void RemoveEnemy()
        {
            Enemy.IsActive = false;
            Enemy.IsAlive = false;
            Enemy.IsVisible = false;
        }

        /// <summary>
        /// If the enemy collides with the player (hitting them), the
        /// player will lose 10% of their health. If their health
        /// drops to 0, they will lose the game and disappear.
        /// </summary>
        public void HasCollided(AnimatedPlayer player)
        {
            if (Enemy.HasCollided(player))
            {
                if (player.Health > 10)
                {
                    player.Health -= 10;
                    StartEnemy();
                }
                else
                {
                    player.Health = 0;

                    player.IsActive = false;
                    player.IsAlive = false;
                    player.IsVisible = false;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;

            double fieldOfView = Vector2.Distance(Player.Position,
                Enemy.Position);

            // Enemy can see player in field of view and changes direction
            if (fieldOfView < 500)
            {
                Vector2 direction = Player.Position - Enemy.Position;
                direction.Normalize();

                Enemy.Direction = direction;
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