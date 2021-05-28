﻿using App05MonoGame.Helpers;
using App05MonoGame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace App05MonoGame.Controllers
{
    public enum CoinColours
    {
        copper = 100,
        Silver = 200,
        Gold = 500
    }

    /// <summary>
    /// This class creates a list of coins which
    /// can be updated and drawn and checked for
    /// collisions with the player sprite
    /// </summary>
    /// <authors>
    /// Mauro Nunes
    /// </authors>
    public class CoinsController
    {
        private SoundEffect coinEffect;
        private int coinValue;
        private double maxTime;
        private double timer;
        private AnimatedSprite spriteCoinTemplate;

        private readonly List<AnimatedSprite> Coins;        

        public CoinsController()
        {
            Coins = new List<AnimatedSprite>();
            maxTime = 5.0;
            timer = maxTime;
        }
        /// <summary>
        /// Create an animated sprite of a copper coin
        /// which could be collected by the player for a score
        /// </summary>
        public void CreateCoin(GraphicsDevice graphics, Texture2D coinSheet)
        {
            coinEffect = SoundController.GetSoundEffect("Coin");
            coinValue = (int)CoinColours.copper;
            Animation animation = new Animation("coin", coinSheet, 8);

            AnimatedSprite coin = new AnimatedSprite()
            {
                Animation = animation,
                Image = animation.SetMainFrame(graphics),
                Scale = 2.0f,
                Position = new Vector2(600, 100),
                Speed = 0,
            };

            spriteCoinTemplate = coin;

            Coins.Add(coin);
        }

        public int HasCollided(AnimatedPlayer player)
        {
            foreach (AnimatedSprite coin in Coins)
            {
                if (coin.HasCollided(player) && coin.IsAlive)
                {
                    coinEffect.Play();

                    coin.IsActive = false;
                    coin.IsAlive = false;
                    coin.IsVisible = false;

                    return coinValue;
                }
            }

            return 0;
        }

        public void Update(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;
            
            if (timer <= 0)
            {
                int x = RandomNumber.Generator.Next(1000) + 100;
                int y = RandomNumber.Generator.Next(500) + 100;

                AnimatedSprite coin = new AnimatedSprite()
                {
                    Animation = spriteCoinTemplate.Animation,
                    Image = spriteCoinTemplate.Image,
                    Scale = spriteCoinTemplate.Scale,
                    Position = new Vector2(x, y),
                    Speed = 0,
                };

                Coins.Add(coin);
                timer = maxTime;
            }

            foreach(AnimatedSprite coin in Coins)
            {
                coin.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (AnimatedSprite coin in Coins)
            {
                coin.Draw(spriteBatch);
            }
        }
    }
}
