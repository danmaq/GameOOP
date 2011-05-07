using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Sample1_02
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game
	{

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
		float[] enemyX = new float[100];

		/// <summary>敵のY座標一覧。</summary>
		float[] enemyY = new float[100];

		/// <summary>敵の移動速度一覧。</summary>
		float[] enemySpeed = new float[100];

		/// <summary>敵の移動角度一覧。</summary>
		double[] enemyAngle = new double[100];

		/// <summary>敵のホーミング有効時間。</summary>
		int[] enemyHomingAmount = new int[100];

		/// <summary>ホーミング対応の敵かどうか。</summary>
		bool[] enemyHoming = new bool[100];

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
				// ゲーム画面

				// プレイヤー移動処理
				if (keyState.IsKeyDown(Keys.Left))
				{
					playerX -= 3;
				}
				if (keyState.IsKeyDown(Keys.Right))
				{
					playerX += 3;
				}
				if (keyState.IsKeyDown(Keys.Up))
				{
					playerY -= 3;
				}
				if (keyState.IsKeyDown(Keys.Down))
				{
					playerY += 3;
				}
				if (playerX < 0)
				{
					playerX = 0;
				}
				if (playerX > 800)
				{
					playerX = 800;
				}
				if (playerY < 0)
				{
					playerY = 0;
				}
				if (playerY > 600)
				{
					playerY = 600;
				}

				// 弾の生成
				if (counter % (int)MathHelper.Max(60 - counter * 0.01f, 1) == 0)
				{
					for (int i = 0; i < enemyX.Length; i++)
					{
						if (enemyX[i] > 800 || enemyX[i] < 0 &&
							enemyY[i] > 600 || enemyY[i] < 0)
						{
							Random rnd = new Random();
							int p = rnd.Next((800 + 600) * 2);
							if (p < 800 || p >= 1400 && p < 2200)
							{
								enemyX[i] = p % 800;
								enemyY[i] = p < 1400 ? 0 : 600;
							}
							else
							{
								enemyX[i] = p < 1400 ? 0 : 800;
								enemyY[i] = p % 600;
							}
							enemySpeed[i] = rnd.Next(1, 3) + counter * 0.001f;
							enemyAngle[i] = Math.Atan2(
								playerY - enemyY[i], playerX - enemyX[i]);
							enemyHoming[i] = rnd.Next(100) >= 80;
							enemyHomingAmount[i] = enemyHoming[i] ? 60 : 0;
							score += 10;
							if (score % 500 < prevScore % 500)
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

				// 弾の移動、及び接触判定
				bool hit = false;
				for (int i = 0; i < enemyX.Length; i++)
				{
					if (Math.Abs(playerX - enemyX[i]) < 48 &&
						Math.Abs(playerY - enemyY[i]) < 48)
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
				if (hit)
				{
					for (int i = 0; i < enemyX.Length; i++)
					{
						enemyX[i] = -32;
						enemyY[i] = -32;
						enemySpeed[i] = 0;
					}
				}
				counter++;
			}
			else
			{
				// タイトル画面
				if (keyState.IsKeyDown(Keys.Escape))
				{
					Exit();
				}
				if (keyState.IsKeyDown(Keys.Space))
				{
					// ゲーム開始
					game = true;
					playerX = 400;
					playerY = 300;
					counter = 0;
					score = 0;
					prevScore = 0;
					playerAmount = 2;
					for (int i = 0; i < enemyX.Length; i++)
					{
						enemyX[i] = -32;
						enemyY[i] = -32;
						enemySpeed[i] = 0;
					}
				}
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
			spriteBatch.Begin();
			if (game)
			{
				// ゲーム描画

				// 自機の描画
				spriteBatch.Draw(
					gameThumbnail, new Vector2(playerX, playerY), null,
					Color.White, 0f, new Vector2(32, 32), 1f,
					SpriteEffects.None, 0f);
				// 敵機の描画
				for (int i = 0; i < enemyX.Length; i++)
				{
					spriteBatch.Draw(
						gameThumbnail, new Vector2(enemyX[i], enemyY[i]), null,
						enemyHoming[i] ? Color.Orange : Color.Red, 0f,
						new Vector2(32, 32), 0.5f, SpriteEffects.None, 0f);
				}
				// HUDの描画
				spriteBatch.DrawString(spriteFont, "SCORE: " + score.ToString(),
					new Vector2(300, 560), Color.Black);
				spriteBatch.DrawString(spriteFont,
					"PLAYER: " + new string('*', playerAmount),
					new Vector2(600, 560), Color.Black);
			}
			else
			{
				// タイトル画面
				spriteBatch.DrawString(spriteFont, "SAMPLE 1", new
					Vector2(200, 100), Color.Black, 0f, Vector2.Zero, 5f,
					SpriteEffects.None, 0f);
				spriteBatch.DrawString(spriteFont, "PUSH SPACE KEY.",
					new Vector2(340, 400), Color.Black);
			}
			spriteBatch.DrawString(spriteFont, "HISCORE: " + hiScore.ToString(),
				new Vector2(0, 560), Color.Black);
			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
