using Sample1_14.state.score;

namespace Sample1_14.task.entity
{

	/// <summary>スコア情報。</summary>
	class Score
		: Entity
	{

		/// <summary>クラス オブジェクト。</summary>
		public static readonly Score instance = new Score();

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		private Score()
			: base(StateHiScoreOnly.instance)
		{
		}

		/// <summary>エクステンドの閾値。</summary>
		private const int EXTEND_THRESHOLD = 500;

		/// <summary>前フレームのスコア。</summary>
		private int prev;

		/// <summary>現在のスコアを描画するかどうか。</summary>
		public bool drawNowScore
		{
			set
			{
				nextState = value ? StateAllScore.instance : StateHiScoreOnly.instance;
			}
		}

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

		/// <summary>オブジェクトをリセットします。</summary>
		public override void release()
		{
			now = 0;
			prev = 0;
			base.release();
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
