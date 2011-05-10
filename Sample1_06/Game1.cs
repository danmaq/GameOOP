using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sample1_06
{

	/// <summary>This is the main type for your game</summary>
	public class Game1
		: Game
	{

		/// <summary>画像サイズ。</summary>
		const float RECT = 64;

		/// <summary>画面矩形情報。</summary>
		Rectangle SCREEN = new Rectangle(0, 0, 800, 600);

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
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			player.acceptInputKeyList =
				new Keys[] { Keys.Up, Keys.Down, Keys.Left, Keys.Right };
			player.velocity = new Dictionary<Keys, Vector2>();
			player.velocity.Add(Keys.Up, new Vector2(0, -Player.SPEED));
			player.velocity.Add(Keys.Down, new Vector2(0, Player.SPEED));
			player.velocity.Add(Keys.Left, new Vector2(-Player.SPEED, 0));
			player.velocity.Add(Keys.Right, new Vector2(Player.SPEED, 0));
			base.Initialize();
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
			Vector2 prev = player.position;
			for (int i = 0; i < player.acceptInputKeyList.Length; i++)
			{
				Keys key = player.acceptInputKeyList[i];
				if (keyState.IsKeyDown(key))
				{
					player.position += player.velocity[key];
				}
			}
			if (!SCREEN.Contains((int)player.position.X, (int)player.position.Y))
			{
				player.position = prev;
			}
		}

		/// <summary>敵機を作成します。</summary>
		private void createEnemy()
		{
			if (counter % (int)MathHelper.Max(60 - counter * 0.01f, 1) == 0)
			{
				float AROUND_HALF = SCREEN.Width + SCREEN.Height;
				float AROUND_HALF_QUARTER = SCREEN.Width * 2 + SCREEN.Height;
				int AROUND = (int)AROUND_HALF * 2;
				for (int i = 0; i < Enemy.MAX; i++)
				{
					if (!SCREEN.Contains(
						(int)enemies[i].position.X, (int)enemies[i].position.Y))
					{
						Random rnd = new Random();
						int p = rnd.Next(AROUND);
						if (p < SCREEN.Width || p >= AROUND_HALF &&
							p < AROUND_HALF_QUARTER)
						{
							enemies[i].position.X = p % SCREEN.Width;
							enemies[i].position.Y = p < AROUND_HALF ? 0 : SCREEN.Height - 1;
						}
						else
						{
							enemies[i].position.X = p < AROUND_HALF ? 0 : SCREEN.Width - 1;
							enemies[i].position.Y = p % SCREEN.Height;
						}
						enemies[i].velocity = createVelocity(
							enemies[i].position, rnd.Next(1, 3) + counter * 0.001f);
						enemies[i].homing = rnd.Next(100) < Enemy.HOMING_PERCENTAGE;
						enemies[i].homingAmount = Enemy.HOMING_LIMIT;
						addScore(10);
						break;
					}
				}
			}
		}

		/// <summary>スコアを加算します。</summary>
		/// <param name="add">加算されるスコア値。</param>
		private void addScore(int add)
		{
			score.now += add;
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
		}

		/// <summary>敵機の移動、及び接触判定をします。</summary>
		/// <returns>接触した場合、true。</returns>
		private bool enemyMoveAndHitTest()
		{
			bool hit = false;
			const float HITAREA = Player.SIZE * 0.5f + Enemy.SIZE * 0.5f;
			const float HITAREA_SQUARED = HITAREA * HITAREA;
			for (int i = 0; i < Enemy.MAX; i++)
			{
				if (Vector2.DistanceSquared(enemies[i].position, player.position) <
					HITAREA_SQUARED)
				{
					hit = true;
					game = --player.amount >= 0;
					break;
				}
				if (enemies[i].homing && --enemies[i].homingAmount > 0)
				{
					enemies[i].velocity =
						createVelocity(enemies[i].position, enemies[i].velocity.Length());
				}
				enemies[i].position += enemies[i].velocity;
			}
			return hit;
		}

		/// <summary>敵機の移動速度と方角を計算します。</summary>
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

		/// <summary>敵機を初期状態にリセットします。</summary>
		private void enemyReset()
		{
			Vector2 firstPosition = new Vector2(-Enemy.SIZE);
			for (int i = 0; i < Enemy.MAX; i++)
			{
				enemies[i].position = firstPosition;
				enemies[i].velocity = Vector2.Zero;
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
				Point center = SCREEN.Center;
				player.position = new Vector2(center.X, center.Y);
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
				"HISCORE: " + score.highest.ToString(), new Vector2(0, 560), Color.Black);
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
			graphics.spriteBatch.Draw(graphics.gameThumbnail, player.position,
				null, Color.White, 0f, new Vector2(RECT * 0.5f),
				Player.SIZE / RECT, SpriteEffects.None, 0f);
		}

		/// <summary>敵機を1フレーム分の描画を行います。</summary>
		private void drawEnemy()
		{
			const float SCALE = Enemy.SIZE / RECT;
			Vector2 origin = new Vector2(RECT * 0.5f);
			for (int i = 0; i < Enemy.MAX; i++)
			{
				graphics.spriteBatch.Draw(graphics.gameThumbnail, enemies[i].position,
					null, enemies[i].homing ? Color.Orange : Color.Red,
					0f, origin, SCALE, SpriteEffects.None, 0f);
			}
		}

		/// <summary> HUDを1フレーム分の描画を行います。</summary>
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
