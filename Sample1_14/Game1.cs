using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sample1_14.core;
using Sample1_14.state;
using Sample1_14.state.scene;
using Sample1_14.task;
using Sample1_14.task.entity;

namespace Sample1_14
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
		private Entity mgrScene = new Entity(SceneTitle.instance);

		/// <summary>タスク管理クラス。</summary>
		private readonly TaskManager<ITask> mgrTask = new TaskManager<ITask>();

		/// <summary>Constructor.</summary>
		public Game1()
		{
			new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			mgrTask.tasks.AddRange(new ITask[] { KeyStatus.instance, Score.instance, mgrScene });
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			graphics = new Graphics(this);
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			mgrTask.update();
			if (mgrScene.currentState == StateEmpty.instance)
			{
				mgrTask.reset();
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
