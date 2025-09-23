using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _paddle1, _paddle2, _ball;
        private int _paddle1XPos = 0;
        private int _paddle1YPos = 0;
        private int _paddle1Width = 50;
        private int _paddle1Height = 0;
        private int _paddle2XPos = 0;
        private int _paddle2YPos = 0;
        private int _paddle2Width = 0;
        private int _paddle2Height = 0;
        private int _ballXPos = 0;
        private int _ballYPos = 0;
        private int _ballWidth = 0;
        private int _ballHeight = 0;
        private Rectangle _paddle1Rectangle, _paddle2Rectangle, _ballRectangle;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _paddle1XPos = 50;
            _paddle1YPos = _graphics.PreferredBackBufferHeight / 2 + _paddle1Height / 2;
            _paddle2XPos = _graphics.PreferredBackBufferWidth - 50;   
            _paddle1YPos = _graphics.PreferredBackBufferHeight / 2 + _paddle2Height / 2;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Content.Load<Texture2D>("ball");
            Content.Load<Texture2D>("paddle1");
            Content.Load<Texture2D>("paddle2");

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

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Draw(_paddle1, _paddle1Rectangle, Color.White);
            _spriteBatch.Draw(_paddle2, _paddle2Rectangle, Color.White);
            _spriteBatch.Draw(_ball, _ballRectangle, Color.White);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
