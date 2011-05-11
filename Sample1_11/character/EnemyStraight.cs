using Microsoft.Xna.Framework.Graphics;

namespace Sample1_11.character
{

	/// <summary>自機めがけて直進する敵機の情報。</summary>
	class EnemyStraight
		: Enemy
	{

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="speed">基準速度。</param>
		public EnemyStraight(float speed) :
			base(speed, Color.Red)
		{
		}
	}
}
