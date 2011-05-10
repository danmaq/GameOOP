using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sample1_06
{

	/// <summary>自機の情報。</summary>
	struct Player
	{
		/// <summary>大きさ。</summary>
		public const float SIZE = 64;

		/// <summary>移動速度。</summary>
		public const float SPEED = 3;

		/// <summary>自機の初期残機。</summary>
		public const int DEFAULT_AMOUNT = 2;

		/// <summary>入力を受け付けるキー一覧。</summary>
		public Keys[] acceptInputKeyList;

		/// <summary>キー入力に対応した移動方向。</summary>
		public Dictionary<Keys, Vector2> velocity;

		/// <summary>ミス猶予(残機)数。</summary>
		public int amount;

		/// <summary>現在座標。</summary>
		public Vector2 position;
	}
}
