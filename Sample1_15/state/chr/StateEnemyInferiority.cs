using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sample1_15.task.entity.chr;

namespace Sample1_15.state.chr
{

	/// <summary>アバウトに自機を狙う敵機の状態。</summary>
	sealed class StateEnemyInferiority
		: StateEnemy
	{

		/// <summary>クラス オブジェクト。</summary>
		internal static readonly StateEnemy instance = new StateEnemyInferiority();

		/// <summary>コンストラクタ。</summary>
		private StateEnemyInferiority()
			: base(30, Color.Magenta)
		{
		}

		/// <summary>
		/// 敵機の移動速度と方角を初期化します。
		/// </summary>
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		/// <param name="speed">速度。</param>
		protected override void initVelocity(Character.CharacterAccessor accessor, float speed)
		{
			base.initVelocity(accessor, speed);
			// ここでベクトルをわざと乱して、精度を落とす
			Quaternion q = Quaternion.CreateFromAxisAngle(
				Vector3.UnitZ, (float)random.NextDouble() - 0.5f);
			accessor.velocity = Vector2.Transform(accessor.velocity, q);
		}
	}
}
