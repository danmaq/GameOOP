using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sample1_05
{

	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1
		: Game
	{

		/// <summary>画像サイズ。</summary>
		const float RECT = 64;

		/// <summary>画面横幅。</summary>
		const float SCREEN_WIDTH = 800;

		/// <summary>画面縦幅。</summary>
		const float SCREEN_HEIGHT = 600;

		/// <summary>画面左端。</summary>
		const float SCREEN_LEFT = 0;

		/// <summary>画面上端。</summary>
		const float SCREEN_TOP = 0;

		/// <summary>画面右端。</summary>
		const float SCREEN_RIGHT = SCREEN_LEFT + SCREEN_WIDTH;

		/// <summary>画面下端。</summary>
		const float SCREEN_BOTTOM = SCREEN_TOP + SCREEN_HEIGHT;

		/// <summary>ゲーム中かどうか。</summary>
		bool game;

		/// <summary>ゲームの進行カウンタ。</summary>
		int counter;

		/// <summary>描画周りデータ。</summary>
		Graphics graphics = new Graphics();

		/// <summary>スコア データ。</summary>
		Score score = new Score();

		/// <summary>自機 データ。</summary>
		Player player = new Player();

		/// <summary>敵機一覧データ。</summary>
		Enemy[] enemies = new Enemy[Enemy.MAX];

		/// <summary>Constructor.</summary>
		public Game1()
		{
			new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			graphics.spriteBatch = new SpriteBatch(GraphicsDevice);

			graphics.gameThumbnail = Content.Load<Texture2D>("GameThumbnail");
			graphics.spriteFont = Content.Load<SpriteFont>("SpriteFont");
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
				movePlayer(keyState);
				createEnemy();
				if (enemyMoveAndHitTest())
				{
					enemyReset();
				}
				counter++;
			}
			else
			{
				updateTitle(keyState);
			}
			base.Update(gameTime);
		}

		/// <summary>自機を移動します。</summary>
		/// <param name="keyState">現在のキー入力状態。</param>
		private void movePlayer(KeyboardState keyState)
		{
			if (keyState.IsKeyDown(Keys.Left))
			{
				player.x -= Player.SPEED;
			}
			if (keyState.IsKeyDown(Keys.Right))
			{
				player.x += Player.SPEED;
			}
			if (keyState.IsKeyDown(Keys.Up))
			{
				player.y -= Player.SPEED;
			}
			if (keyState.IsKeyDown(Keys.Down))
			{
				player.y += Player.SPEED;
			}
			if (player.x < SCREEN_LEFT)
			{
				player.x = SCREEN_LEFT;
			}
			if (player.x > SCREEN_RIGHT)
			{
				player.x = SCREEN_RIGHT;
			}
			if (player.y < SCREEN_TOP)
			{
				player.y = SCREEN_TOP;
			}
			if (player.y > SCREEN_BOTTOM)
			{
				player.y = SCREEN_BOTTOM;
			}
		}

		/// <summary>敵機を作成します。</summary>
		private void createEnemy()
		{
			if (counter % (int)MathHelper.Max(60 - counter * 0.01f, 1) == 0)
			{
				const float AROUND_HALF = SCREEN_WIDTH + SCREEN_HEIGHT;
				const float AROUND_HALF_QUARTER = SCREEN_WIDTH * 2 + SCREEN_HEIGHT;
				const int AROUND = (int)AROUND_HALF * 2;
				for (int i = 0; i < Enemy.MAX; i++)
				{
					if ((enemies[i].x > SCREEN_RIGHT || enemies[i].x < SCREEN_LEFT) &&
						(enemies[i].y > SCREEN_BOTTOM || enemies[i].y < SCREEN_TOP))
					{
						Random rnd = new Random();
						int p = rnd.Next(AROUND);
						if (p < SCREEN_WIDTH || p >= AROUND_HALF &&
							p < AROUND_HALF_QUARTER)
						{
							enemies[i].x = p % SCREEN_WIDTH;
							enemies[i].y = p < AROUND_HALF ? 0 : SCREEN_HEIGHT;
						}
						else
						{
							enemies[i].x = p < AROUND_HALF ? 0 : SCREEN_WIDTH;
							enemies[i].y = p % SCREEN_HEIGHT;
						}
						enemies[i].speed = rnd.Next(1, 3) + counter * 0.001f;
						enemies[i].angle = Math.Atan2(
							player.y - enemies[i].y, player.x - enemies[i].x);
						enemies[i].homing = rnd.Next(100) < Enemy.HOMING_PERCENTAGE;
						enemies[i].homingAmount =
							enemies[i].homing ? Enemy.HOMING_LIMIT : 0;
						score.now += 10;
						if (score.now % Score.EXTEND_THRESHOLD <
							score.prev % Score.EXTEND_THRESHOLD)
						{
							player.amount++;
						}
						score.prev = score.now;
						if (score.highest < score.now)
						{
							score.highest = score.now;
						}
						break;
					}
				}
			}
		}

		/// <summary>敵機の移動、及び接触判定をします。</summary>
		/// <returns>接触した場合、true。</returns>
		private bool enemyMoveAndHitTest()
		{
			bool hit = false;
			const float HITAREA = Player.SIZE * 0.5f + Enemy.SIZE * 0.5f;
			for (int i = 0; i < Enemy.MAX; i++)
			{
				if (Math.Abs(player.x - enemies[i].x) < HITAREA &&
					Math.Abs(player.y - enemies[i].y) < HITAREA)
				{
					hit = true;
					game = --player.amount >= 0;
					break;
				}
				if (--enemies[i].homingAmount > 0)
				{
					enemies[i].angle = Math.Atan2(
						player.y - enemies[i].y, player.x - enemies[i].x);
				}
				enemies[i].x += (float)Math.Cos(enemies[i].angle) * enemies[i].speed;
				enemies[i].y += (float)Math.Sin(enemies[i].angle) * enemies[i].speed;
			}
			return hit;
		}

		/// <summary>敵機を初期状態にリセットします。</summary>
		private void enemyReset()
		{
			const float FIRST_POSITION = -Enemy.SIZE;
			for (int i = 0; i < Enemy.MAX; i++)
			{
				enemies[i].x = FIRST_POSITION;
				enemies[i].y = FIRST_POSITION;
				enemies[i].speed = 0;
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
				player.x = SCREEN_LEFT + SCREEN_WIDTH * 0.5f;
				player.y = SCREEN_TOP + SCREEN_HEIGHT * 0.5f;
				counter = 0;
				score.now = 0;
				score.prev = 0;
				player.amount = Player.DEFAULT_AMOUNT;
				enemyReset();
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
				"HISCORE: " + score.highest.ToString(),
				new Vector2(0, 560), Color.Black);
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

		/// <summary>ゲーム画面を1フレーム分の描画を行います。</summary>
		private void drawGame()
		{
			drawPlayer();
			drawEnemy();
			drawHUD();
		}

		/// <summary>自機を1フレーム分の描画を行います。</summary>
		private void drawPlayer()
		{
			graphics.spriteBatch.Draw(graphics.gameThumbnail,
				new Vector2(player.x, player.y), null, Color.White, 0f,
				new Vector2(RECT * 0.5f), Player.SIZE / RECT, SpriteEffects.None, 0f);
		}

		/// <summary>敵機を1フレーム分の描画を行います。</summary>
		private void drawEnemy()
		{
			const float SCALE = Enemy.SIZE / RECT;
			Vector2 origin = new Vector2(RECT * 0.5f);
			for (int i = 0; i < Enemy.MAX; i++)
			{
				graphics.spriteBatch.Draw(
					graphics.gameThumbnail, new Vector2(enemies[i].x, enemies[i].y),
					null, enemies[i].homing ? Color.Orange : Color.Red,
					0f, origin, SCALE, SpriteEffects.None, 0f);
			}
		}

		/// <summary>HUDを1フレーム分の描画を行います。</summary>
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
