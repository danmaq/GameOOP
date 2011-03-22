namespace Sample1_08
{

	/// <summary>
	/// スコア情報。
	/// </summary>
	class Score
	{

		/// <summary>エクステンドの閾値。</summary>
		public const int EXTEND_THRESHOLD = 500;

		/// <summary>現在のスコア。</summary>
		public int now;

		/// <summary>前フレームのスコア。</summary>
		private int prev;

		/// <summary>ハイスコア。</summary>
		public int highest;

		/// <summary>
		/// スコアをリセットします。
		/// </summary>
		public void reset()
		{
			now = 0;
			prev = 0;
		}

		/// <summary>
		/// スコアを加算します。
		/// </summary>
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
	}
}
