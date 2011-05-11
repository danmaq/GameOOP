namespace Sample1_15.task.entity.score
{

	/// <summary>スコア情報のインターフェイス。</summary>
	interface IScore
		: ITask
	{
		/// <summary>現在のスコアを描画するかどうか。</summary>
		bool drawNowScore
		{
			set;
		}

		/// <summary>現在のスコア。</summary>
		int now
		{
			get;
		}

		/// <summary>ハイスコア。</summary>
		int highest
		{
			get;
		}

		/// <summary>
		/// スコアを加算します。
		/// </summary>
		/// <param name="score">加算されるスコア値。</param>
		/// <returns>エクステンド該当となる場合、true。</returns>
		bool add(int score);
	}
}
