using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sample1_12.core;
using Sample1_12.scene;

namespace Sample1_12
{

	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1
		: Game
	{

		/// <summary>スプライトバッチ。</summary>
		private SpriteBatch spriteBatch;

		/// <summary>
		/// Constructor.
		/// </summary>
		public Game1()
		{
			new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void Initialize()
		{
			IGraphicsData graphicsData = new Graphics(this);
			Services.AddService(typeof(IGraphicsData), graphicsData);
			spriteBatch = graphicsData.spriteBatch;
			IKeyStatus keyStatus = new KeyStatus(this);
			Services.AddService(typeof(IKeyStatus), keyStatus);
			Components.Add(keyStatus);
			IScore score = new Score(this);
			Services.AddService(typeof(IScore), score);
			Components.Add(score);
			Components.Add(new Title(this));
			base.Initialize();
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin();
			base.Draw(gameTime);
			spriteBatch.End();
		}
	}
}
