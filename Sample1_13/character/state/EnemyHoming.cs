using Microsoft.Xna.Framework.Graphics;

namespace Sample1_13.character.state
{

	/// <summary>自機めがけてホーミングする敵機の情報。</summary>
	sealed class EnemyHoming
		: IEnemyState
	{

		/// <summary>ホーミング時間。</summary>
		private const int HOMING_LIMIT = 60;

		/// <summary>クラス インスタンス。</summary>
		public static readonly IEnemyState instance = new EnemyHoming();

		/// <summary>コンストラクタ。</summary>
		private EnemyHoming()
		{
		}

		/// <summary>敵機がアクティブになったときに呼び出されます。</summary>
		/// <param name="entity">対象の敵機。</param>
		public void onStarted(Enemy entity)
		{
			entity.color = Color.Orange;
			entity.homingAmount = HOMING_LIMIT;
		}

		/// <summary>1フレーム分の更新を行う時に呼び出されます。</summary>
		/// <param name="entity">対象の敵機。</param>
		public void onUpdate(Enemy entity)
		{
			if (--entity.homingAmount > 0)
			{
				entity.initVelocity(entity.velocity.Length());
			}
		}

		/// <summary>敵機の移動速度と方角が初期化された時に呼び出されます。</summary>
		/// <param name="entity">対象の敵機。</param>
		public void onInitializedVelocity(Enemy entity)
		{
		}
	}
}
