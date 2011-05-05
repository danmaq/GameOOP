using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sample1_13.character.state
{

	/// <summary>
	/// 粗悪な精度で自機めがけて直進する敵機の状態。
	/// </summary>
	sealed class EnemyInferiority
		: IEnemyState
	{

		/// <summary>クラス インスタンス。</summary>
		public static readonly IEnemyState instance = new EnemyInferiority();

		/// <summary>疑似乱数ジェネレータ。</summary>
		private readonly Random rnd = new Random();

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		private EnemyInferiority()
		{
		}

		/// <summary>
		/// 敵機がアクティブになったときに呼び出されます。
		/// </summary>
		/// <param name="entity">対象の敵機。</param>
		public void onStarted(Enemy entity)
		{
			entity.color = Color.Magenta;
		}

		/// <summary>
		/// 1フレーム分の更新を行う時に呼び出されます。
		/// </summary>
		/// <param name="entity">対象の敵機。</param>
		public void onUpdate(Enemy entity)
		{
		}

		/// <summary>
		/// 敵機の移動速度と方角が初期化された時に呼び出されます。
		/// </summary>
		/// <param name="entity">対象の敵機。</param>
		public void onInitializedVelocity(Enemy entity)
		{
			// ここでベクトルをわざと乱して、精度を落とす
			Quaternion q = Quaternion.CreateFromAxisAngle(
				Vector3.UnitZ, (float)rnd.NextDouble() - 0.5f);
			entity.velocity = Vector2.Transform(entity.velocity, q);
		}
	}
}
