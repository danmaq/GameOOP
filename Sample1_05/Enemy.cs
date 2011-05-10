namespace Sample1_05
{

	/// <summary>敵機の情報。</summary>
	struct Enemy
	{

		/// <summary>大きさ。</summary>
		public const float SIZE = 32;

		/// <summary>最大数。</summary>
		public const int MAX = 100;

		/// <summary>ホーミング確率。</summary>
		public const int HOMING_PERCENTAGE = 20;

		/// <summary>ホーミング時間。</summary>
		public const int HOMING_LIMIT = 60;

		/// <summary>X座標。</summary>
		public float x;

		/// <summary>Y座標。</summary>
		public float y;

		/// <summary>移動速度。</summary>
		public float speed;

		/// <summary>移動角度。</summary>
		public double angle;

		/// <summary>ホーミング対応かどうか。</summary>
		public bool homing;

		/// <summary>ホーミング有効時間。</summary>
		public int homingAmount;
	}
}
