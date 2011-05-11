using Microsoft.Xna.Framework.Graphics;

namespace Sample1_12.character
{

	/// <summary>自機めがけてホーミングする敵機の情報。</summary>
	class EnemyHoming
		: Enemy
	{

		/// <summary>ホーミング時間。</summary>
		private const int HOMING_LIMIT = 60;

		/// <summary>ホーミング有効時間。</summary>
		private int homingAmount;

		/// <summary>コンストラクタ。</summary>
		public EnemyHoming() :
			base(Color.Orange)
		{
		}

		/// <summary>1フレーム分の更新を行います。</summary>
		public override void update()
		{
			base.update();
			if (--homingAmount > 0)
			{
				initVelocity(velocity.Length());
			}
		}

		/// <summary>敵機をアクティブにします。</summary>
		/// <param name="speed">基準速度。</param>
		/// <returns>敵機をアクティブにできた場合、true。</returns>
		public override bool start(float speed)
		{
			bool result = base.start(speed);
			if (result)
			{
				homingAmount = HOMING_LIMIT;
			}
			return result;
		}
	}
}
