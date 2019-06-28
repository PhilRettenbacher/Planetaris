using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using HonzCore.ECS;

namespace Planetaris
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Scene scene;
        GameObject gm1;

        HonzCore.Helpers.IHelper[] helpers;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            helpers = new HonzCore.Helpers.IHelper[] { HonzCore.Helpers.ApplicationHelper.instance };

            foreach(var h in helpers)
            {
                h.Initialize();
            }

            scene = new Scene();

            gm1 = new GameObject();
            gm1.SetParent(scene.root);
            HonzCore.ECS.Component.TestComponent comp = new HonzCore.ECS.Component.TestComponent();
            gm1.AddComponent(comp);

            GameObject gm2 = gm1.Clone("Hannes");
            gm2.SetParent(gm1);
            HonzCore.Helpers.ApplicationHelper.instance.LoadScene(scene);
            System.Console.WriteLine(scene.root.FindChildren("Hannes", recursive: true, requireEnabled: true));

            gm1.Destroy();
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach(var h in helpers)
            {
                h.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            foreach(var h in helpers)
            {
                h.Draw(gameTime);
            }

            base.Draw(gameTime);
        }
    }
}
