using Microsoft.Xna.Framework.Graphics;

namespace Sample1_13.character.state
{

	/// <summary>自機めがけて直進する敵機の状態。</summary>
	sealed class EnemyStraight
		: IEnemyState
	{

		/// <summary>クラス インスタンス。</summary>
		public static readonly IEnemyState instance = new EnemyStraight();

		/// <summary>コンストラクタ。</summary>
		private EnemyStraight()
		{
		}

		/// <summary>敵機がアクティブになったときに呼び出されます。</summary>
		/// <param name="entity">対象の敵機。</param>
		public void onStarted(Enemy entity)
		{
			entity.color = Color.Red;
		}

		/// <summary>1フレーム分の更新を行う時に呼び出されます。</summary>
		/// <param name="entity">対象の敵機。</param>
		public void onUpdate(Enemy entity)
		{
		}

		/// <summary>敵機の移動速度と方角が初期化された時に呼び出されます。</summary>
		/// <param name="entity">対象の敵機。</param>
		public void onInitializedVelocity(Enemy entity)
		{
		}
	}
}
