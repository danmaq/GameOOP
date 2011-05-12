using Microsoft.Xna.Framework.Graphics;

namespace Sample1_15.state.chr
{

	/// <summary>正確に自機を狙う敵機の状態。</summary>
	sealed class StateEnemyStraight
		: StateEnemy
	{

		/// <summary>クラス オブジェクト。</summary>
		internal static readonly StateEnemy normal = new StateEnemyStraight(Color.Red);

		/// <summary>ホーミングした後のオブジェクト。</summary>
		internal static readonly StateEnemy homing =
			new StateEnemyStraight(StateEnemyHoming.instance.color);

		/// <summary>コンストラクタ。</summary>
		/// <param name="color">乗算色。</param>
		private StateEnemyStraight(Color color)
			: base(50, color)
		{
		}
	}
}
