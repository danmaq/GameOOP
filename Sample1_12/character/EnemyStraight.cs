using Microsoft.Xna.Framework;
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
		/// <param name="game">ゲーム メイン オブジェクト。</param>
		public EnemyStraight(Game game)
			: base(game, Color.Red)
		{
		}
	}
}
