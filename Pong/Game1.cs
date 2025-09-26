using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _paddle1, _paddle2, _ball;
        private SpriteFont _score, _win;
        private string _winner;
        private double _elapsed;
        private int _blueScore = 0;
        private int _redScore = 0;
        private int _paddle1XPos;
        private int _paddle1YPos;
        private int _paddle1Width = 50;
        private int _paddle1Height = 250;
        private int _paddle2XPos;
        private int _paddle2YPos;
        private int _paddle2Width = 50;
        private int _paddle2Height = 250;
        private int _ballXPos;
        private int _ballYPos;
        private int _ballWidth = 50;
        private int _ballHeight = 50;
        private int _ballSpeedUpDown = 4;
        private int _ballSpeedLeftRight = 8;
        private int _ballSpeedMultUD = 0;
        private int _ballSpeedMultLR = 0;
        private bool _ballMovingUp = false;
        private bool _ballMovingDown = false;
        private bool _ballMovingLeft = true;
        private bool _ballMovingRight = false;
        private bool _gameLost = false;
        private Vector2 _scorePos, _winPos;
        private Color _ballColor = Color.White;
        Random _random = new Random();
        private Rectangle _paddle1Rectangle, _paddle2Rectangle, _ballRectangle;
        Stopwatch _stopwatch = new();
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _paddle1XPos = 0;
            _paddle1YPos = (_graphics.PreferredBackBufferHeight / 2) - (_paddle1Height / 2);
            _paddle2XPos = (_graphics.PreferredBackBufferWidth) - (_paddle2Width);   
            _paddle2YPos = (_graphics.PreferredBackBufferHeight / 2) - (_paddle2Height / 2);
            _ballXPos = (_graphics.PreferredBackBufferWidth / 2) - (_ballWidth / 2);
            _ballYPos = (_graphics.PreferredBackBufferHeight / 2) - (_ballHeight / 2);

            Stopwatch.StartNew();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _paddle1 = Content.Load<Texture2D>("ball");
            _paddle2 = Content.Load<Texture2D>("paddle1");
            _ball = Content.Load<Texture2D>("paddle2");
            _score = Content.Load<SpriteFont>("score");
            _win = Content.Load<SpriteFont>("win");

            _paddle1Rectangle = new Rectangle(_paddle1XPos, _paddle1YPos, _paddle1Width, _paddle1Height);
            _paddle2Rectangle = new Rectangle(_paddle1XPos, _paddle2YPos, _paddle2Width, _paddle2Height);
            _ballRectangle = new Rectangle(_ballXPos, _ballYPos, _ballWidth, _ballHeight);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _paddle1Rectangle.X = _paddle1XPos;
            _paddle1Rectangle.Y = _paddle1YPos;
            _paddle2Rectangle.X = _paddle2XPos;
            _paddle2Rectangle.Y = _paddle2YPos;
            _ballRectangle.X = _ballXPos;
            _ballRectangle.Y = _ballYPos;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (_paddle1YPos > 0)
                {
                    _paddle1YPos -= 8;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (_paddle1YPos + _paddle1Height <= _graphics.PreferredBackBufferHeight)
                {
                    _paddle1YPos += 8;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (_paddle2YPos > 0)
                {
                    _paddle2YPos -= 8;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (_paddle2YPos + _paddle2Height <= _graphics.PreferredBackBufferHeight)
                {
                    _paddle2YPos += 8;
                }
            }

            if (_ballXPos < 0 || _ballRectangle.Intersects(_paddle1Rectangle))
            {
                _ballMovingLeft = false;
                _ballMovingRight = true;
            }

            if (_ballXPos > (_graphics.PreferredBackBufferWidth - _ballWidth) || _ballRectangle.Intersects(_paddle2Rectangle))
            {
                _ballMovingLeft = true;
                _ballMovingRight = false;
            }

            if (_ballYPos < 0)
            {
                _ballMovingUp = false;
                _ballMovingDown = true;
            }

            if (_ballYPos > (_graphics.PreferredBackBufferHeight - _ballHeight))
            {
                _ballMovingUp = true;
                _ballMovingDown = false;
            }

            if (_ballMovingLeft == true)
            {
                _ballXPos -= (_ballSpeedLeftRight + _ballSpeedMultLR);
            }

            if (_ballMovingRight == true)
            {
                _ballXPos += (_ballSpeedLeftRight + _ballSpeedMultLR);
            }

            if (_ballMovingUp == true)
            {
                _ballYPos -= (_ballSpeedUpDown + _ballSpeedMultUD);
            }

            if (_ballMovingDown == true)
            {
                _ballYPos += (_ballSpeedUpDown + _ballSpeedMultUD);
            }

            if (_ballXPos < 0)
            {
                _ballXPos = (_graphics.PreferredBackBufferWidth / 2) - (_ballWidth / 2);
                _ballYPos = (_graphics.PreferredBackBufferHeight / 2) - (_ballHeight / 2);
                _paddle1XPos = 0;
                _paddle1YPos = (_graphics.PreferredBackBufferHeight / 2) - (_paddle1Height / 2);
                _paddle2XPos = (_graphics.PreferredBackBufferWidth) - (_paddle2Width);
                _paddle2YPos = (_graphics.PreferredBackBufferHeight / 2) - (_paddle2Height / 2);
                _blueScore += 1;
                _ballMovingUp = false;
                _ballMovingDown = false;
                _ballColor = Color.White;
                _ballSpeedMultLR = 0;
                _ballSpeedMultUD = 0;
            }

            if (_ballXPos > (_graphics.PreferredBackBufferWidth - _ballWidth))
            {
                _ballXPos = (_graphics.PreferredBackBufferWidth / 2) - (_ballWidth / 2);
                _ballYPos = (_graphics.PreferredBackBufferHeight / 2) - (_ballHeight / 2);
                _paddle1XPos = 0;
                _paddle1YPos = (_graphics.PreferredBackBufferHeight / 2) - (_paddle1Height / 2);
                _paddle2XPos = (_graphics.PreferredBackBufferWidth) - (_paddle2Width);
                _paddle2YPos = (_graphics.PreferredBackBufferHeight / 2) - (_paddle2Height / 2);
                _redScore += 1;
                _ballMovingUp = false;
                _ballMovingDown = false;
                _ballColor = Color.White;
                _ballSpeedMultLR = 0;
                _ballSpeedMultUD = 0;
            }

            if(_ballRectangle.Intersects(_paddle1Rectangle) && _ballMovingUp == false && _ballMovingDown == false)
            {
                int _randomDirection = _random.Next(1, 3);

                if (_randomDirection == 1)
                {
                    _ballMovingUp = true;
                }
                if (_randomDirection == 2)
                {
                    _ballMovingDown = true;
                }
            }

            if (_ballRectangle.Intersects(_paddle2Rectangle) && _ballMovingUp == false && _ballMovingDown == false)
            {
                int _randomDirection = _random.Next(1, 3);

                if (_randomDirection == 1)
                {
                    _ballMovingUp = true;
                }

                if (_randomDirection == 2)
                {
                    _ballMovingDown = true;
                }
            }

            if (_ballRectangle.Intersects(_paddle1Rectangle))
            {
                _ballColor = Color.Red;
                _ballSpeedMultUD += 1;
                _ballSpeedMultLR += 2;
            }

            if (_ballRectangle.Intersects(_paddle2Rectangle))
            {
                _ballColor = Color.Blue;
                _ballSpeedMultUD += 1;
                _ballSpeedMultLR += 2;
            }

            if (_blueScore > 9)
            {
                _gameLost = true;
                _winner = "Blue";
                _blueScore = 0;
                _redScore = 0;
            }

            if (_redScore > 9)
            {
                _gameLost = true;
                _winner = "Red";
                _blueScore = 0;
                _redScore = 0;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            if (_gameLost == false)
            {
                _spriteBatch.Draw(_paddle1, _paddle1Rectangle, Color.Red);
                _spriteBatch.Draw(_paddle2, _paddle2Rectangle, Color.Blue);
                _spriteBatch.Draw(_ball, _ballRectangle, _ballColor);
                string _scoreText = _blueScore + " - " + _redScore;
                Vector2 textSize = _score.MeasureString(_scoreText);
                _scorePos = new Vector2((_graphics.PreferredBackBufferWidth / 2f) - (textSize.X / 2f), 20f);
                _spriteBatch.DrawString(_score, _scoreText, _scorePos, Color.White);
            }

            if (_gameLost == true)
            {
                string _winText = $"{_winner} Wins!";
                Vector2 textSizeWin = _win.MeasureString(_winText);
                _winPos = new Vector2((_graphics.PreferredBackBufferWidth / 2f) - (textSizeWin.X / 2f), (_graphics.PreferredBackBufferHeight / 2f) - (textSizeWin.Y / 2f));
                _spriteBatch.DrawString(_win, _winText, _winPos, Color.White);
                _elapsed += gameTime.ElapsedGameTime.TotalSeconds;
                if (_elapsed > 2)
                {
                    _gameLost = false;
                    _elapsed = 0;
                }
            }
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
