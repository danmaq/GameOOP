using Microsoft.Xna.Framework.Graphics;

namespace Sample1_11.character
{

	/// <summary>
	/// 自機めがけてホーミングする敵機の情報。
	/// </summary>
	class EnemyHoming
		: Enemy
	{

		/// <summary>ホーミング時間。</summary>
		private const int HOMING_LIMIT = 60;

		/// <summary>ホーミング有効時間。</summary>
		private int homingAmount;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="speed">基準速度。</param>
		public EnemyHoming(float speed) :
			base(speed, Color.Orange)
		{
			homingAmount = HOMING_LIMIT;
		}

		/// <summary>
		/// 1フレーム分の更新を行います。
		/// </summary>
		public override void update()
		{
			base.update();
			if (--homingAmount > 0)
			{
				initVelocity(velocity.Length());
			}
		}
	}
}
