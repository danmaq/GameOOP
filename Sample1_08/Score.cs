using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sample1_08
{

	/// <summary>スコア情報。</summary>
	class Score
	{

		/// <summary>エクステンドの閾値。</summary>
		private const int EXTEND_THRESHOLD = 500;

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
		/// <param name="nowScore">現在のスコアも描画するかどうか。</param>
		public void draw(Graphics graphics, bool nowScore)
		{
			if (nowScore)
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
