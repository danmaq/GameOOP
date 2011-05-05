using Microsoft.Xna.Framework.Graphics;

namespace Sample1_12.character
{

	/// <summary>
	/// 自機めがけて直進する敵機の情報。
	/// </summary>
	class EnemyStraight
		: Enemy
	{

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		public EnemyStraight() :
			base(Color.Red)
		{
		}
	}
}
