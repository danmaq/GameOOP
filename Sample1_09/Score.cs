using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sample1_09
{

	/// <summary>スコア情報。</summary>
	class Score
		: ITask
	{

		/// <summary>エクステンドの閾値。</summary>
		private const int EXTEND_THRESHOLD = 500;

		/// <summary>現在のスコアを描画するかどうか。</summary>
		public bool drawNowScore = false;

		/// <summary>前フレームのスコア。</summary>
		private int prev;

		/// <summary>現在のスコア。</summary>
		public int now
		{
			get;
			private set;
		}

		/// <summary>ハイスコア。</summary>
		public int highest
		{
			get;
			private set;
		}

		/// <summary>スコアをリセットします。</summary>
		public void reset()
		{
			now = 0;
			prev = 0;
			drawNowScore = true;
		}

		/// <summary>1フレーム分の更新を行います。</summary>
		/// <param name="keyState">現在のキー入力状態。</param>
		public void update(KeyboardState keyState)
		{
			// スコアクラスは別段毎フレーム更新するようなものはない。
		}

		/// <summary>スコアを加算します。</summary>
		/// <param name="score">加算されるスコア値。</param>
		/// <returns>エクステンド該当となる場合、true。</returns>
		public bool add(int score)
		{
			now += score;
			bool extend = now % EXTEND_THRESHOLD < prev % EXTEND_THRESHOLD;
			prev = now;
			if (highest < now)
			{
				highest = now;
			}
			return extend;
		}

		/// <summary>1フレーム分の描画を行います。</summary>
		/// <param name="graphics">グラフィック データ。</param>
		public void draw(Graphics graphics)
		{
			if (drawNowScore)
			{
				graphics.spriteBatch.DrawString(graphics.spriteFont,
					"SCORE: " + now.ToString(),
					new Vector2(300, 560), Color.Black);
			}
			graphics.spriteBatch.DrawString(graphics.spriteFont,
				"HISCORE: " + highest.ToString(), new Vector2(0, 560), Color.Black);
		}
	}
}
