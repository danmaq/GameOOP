using Microsoft.Xna.Framework.Graphics;
using Sample1_14.task.entity;
using Sample1_14.task.entity.chr;

namespace Sample1_14.state.chr
{

	/// <summary>アバウトに自機を狙う敵機の状態。</summary>
	sealed class StateEnemyInferiority
		: StateEnemy
	{

		/// <summary>クラス オブジェクト。</summary>
		public static readonly StateEnemy instance = new StateEnemyInferiority();

		/// <summary>コンストラクタ。</summary>
		private StateEnemyInferiority()
			: base(50, Color.Magenta)
		{
		}

		/// <summary>
		/// <para>状態が開始された時に呼び出されます。</para>
		/// <para>このメソッドは、遷移元の<c>teardown</c>よりも後に呼び出されます。</para>
		/// </summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		public override void teardown(Entity entity)
		{
		}

		/// <summary>ダメージを与えます。</summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		/// <param name="value">ダメージ値(負数で回復)。</param>
		/// <returns>続行可能な場合、true。</returns>
		public override bool damage(Character entity, int value)
		{
			return true;
		}
	}
}
