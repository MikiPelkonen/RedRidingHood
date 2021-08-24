using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RedRidingHood.Core;
using RedRidingHood.Entities;
using RedRidingHood.Graphics;

namespace RedRidingHood
{
    public class RedRidingHoodGame : Game
    {
        // Graphics
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        RenderTarget2D _renderTarget;
        Texture2D _primitiveSpriteSheetTexture;
        Texture2D _worldSheet;
        Texture2D _houseInside;
        Texture2D _speechBubble;

        // Core
        InputController _inputController;
        NPController _npController;
        Camera _camera;

        // Fonts
        SpriteFont _testFont;

        // Entities
        EntityManager _entityManager;
        World _world;
        Player _player;
        RedGirl _redGirl;
        DialogueBoard _dialogueBoard;

        public static int ScreenHeight;
        public static int ScreenWidth;
        

        public RedRidingHoodGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _entityManager = new EntityManager();
        }

        protected override void Initialize()
        {
            // Screen resolution to 1280 * 720 ( 16:9 )
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;

            // Set renderTarget resolution to 640 * 360 ( 16 : 9 )
            _renderTarget = new RenderTarget2D(
                GraphicsDevice,
                ScreenWidth / 2,
                ScreenHeight / 2,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _primitiveSpriteSheetTexture = Content.Load<Texture2D>("Primitives/PrimitiveSpriteSheetSix");
            _worldSheet = Content.Load<Texture2D>("Primitives/WorldHouseless");
            _houseInside = Content.Load<Texture2D>("Primitives/InsideHouseFurnitureTwo");
            _speechBubble = Content.Load<Texture2D>("Primitives/SpeechBubbleSix");

            _testFont = Content.Load<SpriteFont>("TestFont");

            // Load Characters
            _player = new Player(new Location(10, 10, 0), _primitiveSpriteSheetTexture);
            _redGirl = new RedGirl(new Location(9, 3, 0), _primitiveSpriteSheetTexture);

            _world = new WorldBuilder().CreateWorld(_primitiveSpriteSheetTexture, _worldSheet, _houseInside, _player, _entityManager);
            
            _inputController = new InputController(_player, _world, _entityManager);
            _camera = new Camera();

            _npController = new NPController(_world, _redGirl, _player);

            _dialogueBoard = new DialogueBoard(_player, _redGirl, _speechBubble, _testFont);

            _entityManager.Add(_world);
            _entityManager.Add(_player);
            _entityManager.Add(_redGirl);
            _entityManager.Add(_dialogueBoard);
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            
            _inputController.ProcessControls(gameTime);
            _entityManager.Update(gameTime);
            _camera.Follow(_player);
            _npController.Update();


            base.Update(gameTime);
        }

        protected void DrawSceneToTexture(RenderTarget2D renderTarget, GameTime gameTime)
        {
            // Set rendertarget
            GraphicsDevice.SetRenderTarget(renderTarget);

            // Draw the scene
            GraphicsDevice.Clear(Color.Black);

            // TODO: GAME DRAW LOGIC HERE
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone, transformMatrix: _camera.Transform);

            _entityManager.Draw(_spriteBatch, gameTime);

            _spriteBatch.End();


            GraphicsDevice.SetRenderTarget(null);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DrawSceneToTexture(_renderTarget, gameTime);

            // Clear screen to black
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            _spriteBatch.Draw(_renderTarget, new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
