using Microsoft.Xna.Framework.Graphics;
using Sample1_15.task.entity;
using Sample1_15.task.entity.chr;

namespace Sample1_15.state.chr
{

	/// <summary>自機をめがけホーミングする敵機の状態。</summary>
	sealed class StateEnemyHoming
		: StateEnemy
	{

		/// <summary>ホーミング時間。</summary>
		private const int HOMING_LIMIT = 60;

		/// <summary>クラス オブジェクト。</summary>
		internal static readonly StateEnemy instance = new StateEnemyHoming();

		/// <summary>コンストラクタ。</summary>
		private StateEnemyHoming()
			: base(20, Color.Yellow)
		{
		}

		/// <summary>
		/// 1フレーム分の更新を行う時に呼び出されます。
		/// </summary>
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		public override void update(IEntityAccessor accessor)
		{
			if (accessor.counter >= HOMING_LIMIT)
			{
				accessor.entity.nextState = StateEnemyStraight.homing;
			}
			Character.CharacterAccessor chr = (Character.CharacterAccessor)accessor;
			initVelocity(chr, chr.velocity.Length());
			base.update(accessor);
		}
	}
}
