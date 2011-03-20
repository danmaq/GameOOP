namespace Sample1_05
{

	/// <summary>
	/// 自機の情報。
	/// </summary>
	struct Player
	{

		/// <summary>大きさ。</summary>
		public const float SIZE = 64;

		/// <summary>移動速度。</summary>
		public const float SPEED = 3;

		/// <summary>自機の初期残機。</summary>
		public const int DEFAULT_AMOUNT = 2;

		/// <summary>ミス猶予(残機)数。</summary>
		public int amount;

		/// <summary>X座標。</summary>
		public float x;

		/// <summary>Y座標。</summary>
		public float y;
	}
}
