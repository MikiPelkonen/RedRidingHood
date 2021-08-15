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
        //Graphics
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        RenderTarget2D _renderTarget;
        Texture2D _primitiveSpriteSheetTexture;

        //Core
        InputController _inputController;

        // Entities
        EntityManager _entityManager;
        World _world;
        Player _player;

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
            // Screen resolution to 640 * 360 ( 16:9 )
            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 360;
            _graphics.ApplyChanges();

            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;

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

            _primitiveSpriteSheetTexture = Content.Load<Texture2D>("Primitives/PrimitiveSpriteSheet");

            _world = new WorldBuilder().CreateWorld(_primitiveSpriteSheetTexture);
            _player = new Player(new Location(1, 0, 0), new Sprite(_primitiveSpriteSheetTexture, 48, 16, 16, 32));
            _inputController = new InputController(_player, _world);

            _entityManager.Add(_world);
            _entityManager.Add(_player);
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _inputController.ProcessControls(gameTime);
            _entityManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected void DrawSceneToTexture(RenderTarget2D renderTarget, GameTime gameTime)
        {
            // Set rendertarget
            GraphicsDevice.SetRenderTarget(renderTarget);

            // Draw the scene
            GraphicsDevice.Clear(Color.Black);

            // TODO: GAME DRAW LOGIC HERE
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone);

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

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone);

            _spriteBatch.Draw(_renderTarget, new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
