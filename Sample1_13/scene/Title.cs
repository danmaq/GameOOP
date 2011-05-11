using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sample1_13.core;

namespace Sample1_13.scene
{

	/// <summary>タイトル画面。</summary>
	class Title
		: IScene
	{

		/// <summary>クラス オブジェクト。</summary>
		public static readonly IScene instance = new Title();

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		private Title()
		{
			next = this;
		}

		/// <summary>次に遷移するシーン。</summary>
		public IScene next
		{
			get;
			private set;
		}

		/// <summary>ゲーム シーンの初期化を行います。</summary>
		public void setup()
		{
			Score.instance.drawNowScore = false;
		}

		/// <summary>1フレーム分の更新を行います。</summary>
		public void update()
		{
			next = this;
			KeyboardState keyState = KeyStatus.instance.keyboardState;
			if (keyState.IsKeyDown(Keys.Escape))
			{
				next = null;
			}
			if (keyState.IsKeyDown(Keys.Space))
			{
				// ゲーム開始
				next = GamePlay.instance;
			}
		}

		/// <summary>1フレーム分の描画を行います。</summary>
		/// <param name="graphics">グラフィック データ。</param>
		public void draw(Graphics graphics)
		{
			graphics.spriteBatch.DrawString(
				graphics.spriteFont, "SAMPLE 1", new Vector2(200, 100),
				Color.Black, 0f, Vector2.Zero, 5f, SpriteEffects.None, 0f);
			graphics.spriteBatch.DrawString(graphics.spriteFont,
				"PUSH SPACE KEY.", new Vector2(340, 400), Color.Black);
			Score.instance.draw(graphics);
		}
	}
}
