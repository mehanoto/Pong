using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D ballTexture;
        Vector2 ballPosition;
        Vector2 ballSpeedVector;
        float ballSpeed;
        
        public Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                                       _graphics.PreferredBackBufferHeight / 2);
            ballSpeed = 100f;

            ballSpeedVector = new Vector2(1, -1);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture = Content.Load<Texture2D>("ball");
        }


        private void checkBallCollision()
        {
            //проверка за допир с края на екрана
            if (this.ballPosition.X > _graphics.PreferredBackBufferWidth - ballTexture.Width / 2)
            {
                this.ballSpeedVector.X = -this.ballSpeedVector.X;
            }
            else if (this.ballPosition.X < ballTexture.Width / 2)
            {
                this.ballSpeedVector.X = -this.ballSpeedVector.X;
            }

            if (this.ballPosition.Y > _graphics.PreferredBackBufferHeight - ballTexture.Height / 2)
            {
                this.ballSpeedVector.Y = -this.ballSpeedVector.Y;
            }
            else if (this.ballPosition.Y < ballTexture.Height / 2)
            {
                this.ballSpeedVector.Y = -this.ballSpeedVector.Y;
            }
        }

        private void updateBallPosition(float updatedBallSpeed)
        {
            float ratio = this.ballSpeedVector.X / this.ballSpeedVector.Y;
            float deltaY = updatedBallSpeed / (float)Math.Sqrt(1 + ratio * ratio);
            float deltaX = Math.Abs(ratio * deltaY);

            if (this.ballSpeedVector.X > 0)
            {
                this.ballPosition.X += deltaX;
            }
            else
            {
                this.ballPosition.X -= deltaX;
            }

            if (this.ballSpeedVector.Y > 0)
            {
                this.ballPosition.Y += deltaY;
            }
            else
            {
                this.ballPosition.Y -= deltaY;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            this.checkBallCollision();
            float updatedBallSpeed = ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.updateBallPosition(updatedBallSpeed);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(
                ballTexture,
                ballPosition,
                null,
                Color.White,
                0f,
                new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
