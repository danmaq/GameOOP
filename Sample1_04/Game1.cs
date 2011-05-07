using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sample1_04
{

	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1
		: Game
	{

		/// <summary>自機サイズ。</summary>
		const float RECT_SIZE = 64;

		/// <summary>自機の移動速度。</summary>
		const float PLAYER_SPEED = 3;

		/// <summary>自機の初期残機。</summary>
		const int PLAYER_AMOUNT = 2;

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

		/// <summary>エクステンドの閾値。</summary>
		const int EXTEND_THRESHOLD = 500;

		/// <summary>敵機の最大数。</summary>
		const int ENEMY_MAX = 100;

		/// <summary>敵機の自機に対する大きさ。</summary>
		const float ENEMY_SCALE = 0.5f;

		/// <summary>ホーミング確率。</summary>
		const int HOMING_PERCENTAGE = 20;

		/// <summary>ホーミング時間。</summary>
		const int HOMING_LIMIT = 60;

		/// <summary>スプライト バッチ。</summary>
		SpriteBatch spriteBatch;

		/// <summary>キャラクタ用画像。</summary>
		Texture2D gameThumbnail;

		/// <summary>フォント画像。</summary>
		SpriteFont spriteFont;

		/// <summary>ゲーム中かどうか。</summary>
		bool game;

		/// <summary>ゲームの進行カウンタ。</summary>
		int counter;

		/// <summary>現在のスコア。</summary>
		int score;

		/// <summary>前フレームのスコア。</summary>
		int prevScore;

		/// <summary>ハイスコア。</summary>
		int hiScore;

		/// <summary>ミス猶予(残機)数。</summary>
		int playerAmount;

		/// <summary>プレイヤーのX座標。</summary>
		float playerX;

		/// <summary>プレイヤーのY座標。</summary>
		float playerY;

		/// <summary>敵のX座標一覧。</summary>
		float[] enemyX = new float[ENEMY_MAX];

		/// <summary>敵のY座標一覧。</summary>
		float[] enemyY = new float[ENEMY_MAX];

		/// <summary>敵の移動速度一覧。</summary>
		float[] enemySpeed = new float[ENEMY_MAX];

		/// <summary>敵の移動角度一覧。</summary>
		double[] enemyAngle = new double[ENEMY_MAX];

		/// <summary>敵のホーミング有効時間。</summary>
		int[] enemyHomingAmount = new int[ENEMY_MAX];

		/// <summary>ホーミング対応の敵かどうか。</summary>
		bool[] enemyHoming = new bool[ENEMY_MAX];

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
			spriteBatch = new SpriteBatch(GraphicsDevice);

			gameThumbnail = Content.Load<Texture2D>("GameThumbnail");
			spriteFont = Content.Load<SpriteFont>("SpriteFont");
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
				playerX -= PLAYER_SPEED;
			}
			if (keyState.IsKeyDown(Keys.Right))
			{
				playerX += PLAYER_SPEED;
			}
			if (keyState.IsKeyDown(Keys.Up))
			{
				playerY -= PLAYER_SPEED;
			}
			if (keyState.IsKeyDown(Keys.Down))
			{
				playerY += PLAYER_SPEED;
			}
			if (playerX < SCREEN_LEFT)
			{
				playerX = SCREEN_LEFT;
			}
			if (playerX > SCREEN_RIGHT)
			{
				playerX = SCREEN_RIGHT;
			}
			if (playerY < SCREEN_TOP)
			{
				playerY = SCREEN_TOP;
			}
			if (playerY > SCREEN_BOTTOM)
			{
				playerY = SCREEN_BOTTOM;
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
				for (int i = 0; i < ENEMY_MAX; i++)
				{
					if ((enemyX[i] > SCREEN_RIGHT || enemyX[i] < SCREEN_LEFT) &&
						(enemyY[i] > SCREEN_BOTTOM || enemyY[i] < SCREEN_TOP))
					{
						Random rnd = new Random();
						int p = rnd.Next(AROUND);
						if (p < SCREEN_WIDTH || p >= AROUND_HALF &&
							p < AROUND_HALF_QUARTER)
						{
							enemyX[i] = p % SCREEN_WIDTH;
							enemyY[i] = p < AROUND_HALF ? 0 : SCREEN_HEIGHT;
						}
						else
						{
							enemyX[i] = p < AROUND_HALF ? 0 : SCREEN_WIDTH;
							enemyY[i] = p % SCREEN_HEIGHT;
						}
						enemySpeed[i] = rnd.Next(1, 3) + counter * 0.001f;
						enemyAngle[i] = Math.Atan2(
							playerY - enemyY[i], playerX - enemyX[i]);
						enemyHoming[i] = rnd.Next(100) < HOMING_PERCENTAGE;
						enemyHomingAmount[i] = enemyHoming[i] ? HOMING_LIMIT : 0;
						score += 10;
						if (score % EXTEND_THRESHOLD < prevScore % EXTEND_THRESHOLD)
						{
							playerAmount++;
						}
						prevScore = score;
						if (hiScore < score)
						{
							hiScore = score;
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
			const float HITAREA = RECT_SIZE * 0.5f + RECT_SIZE * ENEMY_SCALE * 0.5f;
			for (int i = 0; i < ENEMY_MAX; i++)
			{
				if (Math.Abs(playerX - enemyX[i]) < HITAREA &&
					Math.Abs(playerY - enemyY[i]) < HITAREA)
				{
					hit = true;
					game = --playerAmount >= 0;
					break;
				}
				if (--enemyHomingAmount[i] > 0)
				{
					enemyAngle[i] = Math.Atan2(
						playerY - enemyY[i], playerX - enemyX[i]);
				}
				enemyX[i] += (float)Math.Cos(enemyAngle[i]) * enemySpeed[i];
				enemyY[i] += (float)Math.Sin(enemyAngle[i]) * enemySpeed[i];
			}
			return hit;
		}

		/// <summary>敵機を初期状態にリセットします。</summary>
		private void enemyReset()
		{
			const float FIRST_POSITION = -RECT_SIZE * ENEMY_SCALE;
			for (int i = 0; i < ENEMY_MAX; i++)
			{
				enemyX[i] = FIRST_POSITION;
				enemyY[i] = FIRST_POSITION;
				enemySpeed[i] = 0;
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
				playerX = SCREEN_LEFT + SCREEN_WIDTH * 0.5f;
				playerY = SCREEN_TOP + SCREEN_HEIGHT * 0.5f;
				counter = 0;
				score = 0;
				prevScore = 0;
				playerAmount = PLAYER_AMOUNT;
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
			spriteBatch.Begin();
			if (game)
			{
				drawGame();
			}
			else
			{
				drawTitle();
			}
			spriteBatch.DrawString(spriteFont, "HISCORE: " + hiScore.ToString(),
				new Vector2(0, 560), Color.Black);
			spriteBatch.End();
			base.Draw(gameTime);
		}

		/// <summary>タイトル画面を描画します。</summary>
		private void drawTitle()
		{
			spriteBatch.DrawString(spriteFont, "SAMPLE 1", new Vector2(200, 100),
				Color.Black, 0f, Vector2.Zero, 5f, SpriteEffects.None, 0f);
			spriteBatch.DrawString(spriteFont, "PUSH SPACE KEY.",
				new Vector2(340, 400), Color.Black);
		}

		/// <summary>ゲーム画面を描画します。</summary>
		private void drawGame()
		{
			drawPlayer();
			drawEnemy();
			drawHUD();
		}

		/// <summary>自機を描画します。</summary>
		private void drawPlayer()
		{
			spriteBatch.Draw(
				gameThumbnail, new Vector2(playerX, playerY), null,
				Color.White, 0f, new Vector2(RECT_SIZE * 0.5f), 1f,
				SpriteEffects.None, 0f);
		}

		/// <summary>敵機を描画します。</summary>
		private void drawEnemy()
		{
			for (int i = 0; i < ENEMY_MAX; i++)
			{
				spriteBatch.Draw(
					gameThumbnail, new Vector2(enemyX[i], enemyY[i]), null,
					enemyHoming[i] ? Color.Orange : Color.Red, 0f,
					new Vector2(RECT_SIZE * 0.5f), ENEMY_SCALE, SpriteEffects.None, 0f);
			}
		}

		/// <summary>HUDを描画します。</summary>
		private void drawHUD()
		{
			spriteBatch.DrawString(spriteFont, "SCORE: " + score.ToString(),
				new Vector2(300, 560), Color.Black);
			spriteBatch.DrawString(spriteFont,
				"PLAYER: " + new string('*', playerAmount),
				new Vector2(600, 560), Color.Black);
		}
	}
}
