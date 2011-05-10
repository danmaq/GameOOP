using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sample1_09
{

	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1
		: Game
	{

		/// <summary>画面矩形情報。</summary>
		public static readonly Rectangle SCREEN = new Rectangle(0, 0, 800, 600);

		/// <summary>ゲーム中かどうか。</summary>
		private bool game;

		/// <summary>ゲームの進行カウンタ。</summary>
		private int counter;

		/// <summary>描画周りデータ。</summary>
		private Graphics graphics;

		/// <summary>キー入力管理クラス。</summary>
		private readonly KeyState mgrInput = new KeyState();

		/// <summary>スコア データ。</summary>
		private readonly Score score = new Score();

		/// <summary>敵機一覧データ。</summary>
		private readonly Enemies enemies = new Enemies();

		/// <summary>自機データ。</summary>
		private readonly Player player = new Player();

		/// <summary>タイトル画面のタスク一覧。</summary>
		private readonly ITask[] taskTitle;

		/// <summary>ゲームプレイ画面のタスク一覧。</summary>
		private readonly ITask[] taskGame;

		/// <summary>Constructor.</summary>
		public Game1()
		{
			new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			taskTitle = new ITask[] { score, mgrInput };
			taskGame = new ITask[] { enemies, player, score, mgrInput };
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
			KeyboardState keyState = mgrInput.keyboardState;
			ITask[] tasks = game ? taskGame : taskTitle;
			for (int i = 0; i < tasks.Length; i++)
			{
				tasks[i].update(keyState);
			}
			if (game)
			{
				createEnemy();
				if (enemies.hitTest(player.position))
				{
					game = player.miss();
					score.drawNowScore = game;
				}
				counter++;
			}
			else
			{
				updateTitle(keyState);
			}
			base.Update(gameTime);
		}

		/// <summary>敵機を作成します。</summary>
		private void createEnemy()
		{
			if (counter % (int)MathHelper.Max(60 - counter * 0.01f, 1) == 0 &&
				enemies.create(player.position, counter * 0.001f) &&
				score.add(10))
			{
				player.extend();
			}
		}

		/// <summary>タイトル画面を更新します。</summary>
		/// <param name="keyState">現在のキー入力状態。</param>
		private void updateTitle(KeyboardState keyState)
		{
			if (keyState.IsKeyDown(Keys.Escape))
			{
				Exit();
			}
			if (keyState.IsKeyDown(Keys.Space))
			{
				// ゲーム開始
				game = true;
				counter = 0;
				for (int i = 0; i < taskGame.Length; i++)
				{
					taskGame[i].reset();
				}
			}
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			graphics.spriteBatch.Begin();
			ITask[] tasks = game ? taskGame : taskTitle;
			for (int i = 0; i < tasks.Length; i++)
			{
				tasks[i].draw(graphics);
			}
			if (!game)
			{
				drawTitle();
			}
			graphics.spriteBatch.End();
			base.Draw(gameTime);
		}

		/// <summary>タイトル画面を1フレーム分の描画を行います。</summary>
		private void drawTitle()
		{
			graphics.spriteBatch.DrawString(
				graphics.spriteFont, "SAMPLE 1", new Vector2(200, 100),
				Color.Black, 0f, Vector2.Zero, 5f, SpriteEffects.None, 0f);
			graphics.spriteBatch.DrawString(graphics.spriteFont,
				"PUSH SPACE KEY.", new Vector2(340, 400), Color.Black);
		}
	}
}
