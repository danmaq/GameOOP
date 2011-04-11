using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sample1_11.core;
using Sample1_11.scene;

namespace Sample1_11
{

	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1
		: Game
	{

		/// <summary>画面矩形情報。</summary>
		public static readonly Rectangle SCREEN = new Rectangle(0, 0, 800, 600);

		/// <summary>描画周りデータ。</summary>
		private Graphics graphics;

		/// <summary>シーン管理クラス。</summary>
		private SceneManager mgrScene = new SceneManager(Title.instance);

		/// <summary>タスク管理クラス。</summary>
		private readonly TaskManager mgrTask;

		/// <summary>
		/// Constructor.
		/// </summary>
		public Game1()
		{
			new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			mgrTask = new TaskManager();
			mgrTask.tasks.AddRange(new ITask[] { KeyStatus.instance, mgrScene });
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			graphics = new Graphics(this);
			mgrTask.setup();
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			mgrTask.update();
			if (mgrScene.nowScene == null)
			{
				Exit();
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
			graphics.spriteBatch.Begin();
			mgrTask.draw(graphics);
			graphics.spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
