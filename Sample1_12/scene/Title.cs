using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sample1_12.scene
{

	/// <summary>
	/// タイトル画面。
	/// </summary>
	class Title
		: Scene
	{

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="game">ゲーム メイン オブジェクト。</param>
		public Title(Game game)
			: base(game)
		{
		}

		/// <summary>
		/// ゲーム シーンの初期化を行います。
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();
			score.drawNowScore = false;
		}

		/// <summary>
		/// 1フレーム分の更新を行います。
		/// </summary>
		/// <param name="gameTime">前フレームからの経過時間。</param>
		public override void Update(GameTime gameTime)
		{
			KeyboardState keyState = keyStatus.keyboardState;
			if (keyState.IsKeyDown(Keys.Escape))
			{
				next = null;
			}
			if (keyState.IsKeyDown(Keys.Space))
			{
				// ゲーム開始
				next = new GamePlay(Game);
			}
			base.Update(gameTime);
		}

		/// <summary>
		/// 1フレーム分の描画を行います。
		/// </summary>
		/// <param name="gameTime">前フレームからの経過時間。</param>
		public override void Draw(GameTime gameTime)
		{
			graphics.spriteBatch.DrawString(
				graphics.spriteFont, "SAMPLE 1", new Vector2(200, 100),
				Color.Black, 0f, Vector2.Zero, 5f, SpriteEffects.None, 0f);
			graphics.spriteBatch.DrawString(graphics.spriteFont,
				"PUSH SPACE KEY.", new Vector2(340, 400), Color.Black);
			base.Draw(gameTime);
		}
	}
}
