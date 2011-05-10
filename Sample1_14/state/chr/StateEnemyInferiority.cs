using Microsoft.Xna.Framework;
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

		/// <summary>
		/// 敵機の移動速度と方角を初期化します。
		/// </summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		/// <param name="speed">速度。</param>
		protected override void initVelocity(Entity entity, float speed)
		{
			base.initVelocity(entity, speed);
			// ここでベクトルをわざと乱して、精度を落とす
			Quaternion q = Quaternion.CreateFromAxisAngle(
				Vector3.UnitZ, (float)rnd.NextDouble() - 0.5f);
			Character chr = (Character)entity;
			chr.velocity = Vector2.Transform(chr.velocity, q);
		}
	}
}
