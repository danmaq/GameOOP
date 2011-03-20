using Microsoft.Xna.Framework;

namespace Sample1_06
{

	/// <summary>
	/// 敵機の情報。
	/// </summary>
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

		/// <summary>現在座標。</summary>
		public Vector2 position;

		/// <summary>移動速度と方角。</summary>
		public Vector2 velocity;

		/// <summary>ホーミング対応かどうか。</summary>
		public bool homing;

		/// <summary>ホーミング有効時間。</summary>
		public int homingAmount;
	}
}
