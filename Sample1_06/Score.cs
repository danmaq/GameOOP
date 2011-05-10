namespace Sample1_06
{

	/// <summary>スコア情報。</summary>
	struct Score
	{

		/// <summary>エクステンドの閾値。</summary>
		public const int EXTEND_THRESHOLD = 500;

		/// <summary>現在のスコア。</summary>
		public int now;

		/// <summary>前フレームのスコア。</summary>
		public int prev;

		/// <summary>ハイスコア。</summary>
		public int highest;
	}
}
