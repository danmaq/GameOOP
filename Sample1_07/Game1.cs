using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sample1_07
{

	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1
		: Game
	{

		/// <summary>画像サイズ。</summary>
		const float RECT = 64;

		/// <summary>画面矩形情報。</summary>
		public static readonly Rectangle SCREEN = new Rectangle(0, 0, 800, 600);

		/// <summary>ゲーム中かどうか。</summary>
		bool game;

		/// <summary>ゲームの進行カウンタ。</summary>
		int counter;

		/// <summary>描画周りデータ。</summary>
		Graphics graphics;

		/// <summary>スコア データ。</summary>
		Score score = new Score();

		/// <summary>自機 データ。</summary>
		Player player = new Player();

		/// <summary>
		/// Constructor.
		/// </summary>
		public Game1()
		{
			new GraphicsDeviceManager(this);
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
			player.initialize();
			base.Initialize();
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
			KeyboardState keyState = Keyboard.GetState();
			if (game)
			{
				player.move(keyState);
				createEnemy();
				if (Enemy.enemyMoveAndHitTest(player.position))
				{
					game = --player.amount >= 0;
				}
				counter++;
			}
			else
			{
				updateTitle(keyState);
			}
			base.Update(gameTime);
		}

		/// <summary>
		/// 敵機を作成します。
		/// </summary>
		private void createEnemy()
		{
			if (counter % (int)MathHelper.Max(60 - counter * 0.01f, 1) == 0 &&
				Enemy.create(player.position, counter * 0.001f) &&
				score.add(10))
			{
				player.amount++;
			}
		}

		/// <summary>
		/// 敵機の移動、及び接触判定をします。
		/// </summary>
		/// <returns>接触した場合、true。</returns>
		private bool enemyMoveAndHitTest()
		{
			bool hit = false;
			const float HITAREA = Player.SIZE * 0.5f + Enemy.SIZE * 0.5f;
			const float HITAREA_SQUARED = HITAREA * HITAREA;
			for (int i = 0; i < Enemy.MAX; i++)
			{
				if (Vector2.DistanceSquared(Enemy.enemies[i].position, player.position) <
					HITAREA_SQUARED)
				{
					hit = true;
					game = --player.amount >= 0;
					break;
				}
				if (Enemy.enemies[i].homing && --Enemy.enemies[i].homingAmount > 0)
				{
					Enemy.enemies[i].velocity =
						createVelocity(Enemy.enemies[i].position, Enemy.enemies[i].velocity.Length());
				}
				Enemy.enemies[i].position += Enemy.enemies[i].velocity;
			}
			return hit;
		}

		/// <summary>
		/// 敵機の移動速度と方角を計算します。
		/// </summary>
		/// <param name="position">位置。</param>
		/// <param name="speed">速度。</param>
		/// <returns>計算された敵機の新しい移動速度と方角。</returns>
		private Vector2 createVelocity(Vector2 position, float speed)
		{
			Vector2 velocity = player.position - position;
			if (velocity == Vector2.Zero)
			{
				// 長さが0だと単位ベクトル計算時にNaNが出るため対策
				velocity = Vector2.UnitX;
			}
			velocity.Normalize();
			return (velocity * speed);
		}

		/// <summary>
		/// タイトル画面を更新します。
		/// </summary>
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
				Point center = SCREEN.Center;
				player.position = new Vector2(center.X, center.Y);
				counter = 0;
				score.reset();
				player.amount = Player.DEFAULT_AMOUNT;
				Enemy.reset();
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
			if (game)
			{
				drawGame();
			}
			else
			{
				drawTitle();
			}
			graphics.spriteBatch.DrawString(graphics.spriteFont,
				"HISCORE: " + score.highest.ToString(), new Vector2(0, 560), Color.Black);
			graphics.spriteBatch.End();
			base.Draw(gameTime);
		}

		/// <summary>
		/// タイトル画面を描画します。
		/// </summary>
		private void drawTitle()
		{
			graphics.spriteBatch.DrawString(
				graphics.spriteFont, "SAMPLE 1", new Vector2(200, 100),
				Color.Black, 0f, Vector2.Zero, 5f, SpriteEffects.None, 0f);
			graphics.spriteBatch.DrawString(graphics.spriteFont,
				"PUSH SPACE KEY.", new Vector2(340, 400), Color.Black);
		}

		/// <summary>
		/// ゲーム画面を描画します。
		/// </summary>
		private void drawGame()
		{
			drawPlayer();
			drawEnemy();
			drawHUD();
		}

		/// <summary>
		/// 自機を描画します。
		/// </summary>
		private void drawPlayer()
		{
			graphics.spriteBatch.Draw(graphics.gameThumbnail, player.position,
				null, Color.White, 0f, new Vector2(RECT * 0.5f),
				Player.SIZE / RECT, SpriteEffects.None, 0f);
		}

		/// <summary>
		/// 敵機を描画します。
		/// </summary>
		private void drawEnemy()
		{
			const float SCALE = Enemy.SIZE / RECT;
			Vector2 origin = new Vector2(RECT * 0.5f);
			for (int i = 0; i < Enemy.MAX; i++)
			{
				graphics.spriteBatch.Draw(graphics.gameThumbnail, Enemy.enemies[i].position,
					null, Enemy.enemies[i].homing ? Color.Orange : Color.Red,
					0f, origin, SCALE, SpriteEffects.None, 0f);
			}
		}

		/// <summary>
		/// HUDを描画します。
		/// </summary>
		private void drawHUD()
		{
			graphics.spriteBatch.DrawString(graphics.spriteFont,
				"SCORE: " + score.now.ToString(),
				new Vector2(300, 560), Color.Black);
			graphics.spriteBatch.DrawString(graphics.spriteFont,
				"PLAYER: " + new string('*', player.amount),
				new Vector2(600, 560), Color.Black);
		}
	}
}
