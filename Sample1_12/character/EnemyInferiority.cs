using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sample1_12.character
{

	/// <summary>
	/// 粗悪な精度で自機めがけて直進する敵機の情報。
	/// </summary>
	class EnemyInferiority
		: Enemy
	{

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="game">ゲーム メイン オブジェクト。</param>
		public EnemyInferiority(Game game)
			: base(game, Color.Magenta)
		{
		}

		/// <summary>
		/// 敵機の移動速度と方角を初期化します。
		/// </summary>
		/// <param name="speed">速度。</param>
		protected override void initVelocity(float speed)
		{
			base.initVelocity(speed);
			// ここで精度を落とす
			Quaternion q = Quaternion.CreateFromAxisAngle(
				Vector3.UnitZ, (float)rnd.NextDouble() - 0.5f);
			velocity = Vector2.Transform(velocity, q);
		}
	}
}
