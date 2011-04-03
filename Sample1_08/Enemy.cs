using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sample1_08
{

	/// <summary>
	/// 敵機の情報。
	/// </summary>
	struct Enemy
	{

		/// <summary>大きさ。</summary>
		public const float SIZE = 32;

		/// <summary>ホーミング確率。</summary>
		private const int HOMING_PERCENTAGE = 20;

		/// <summary>ホーミング時間。</summary>
		private const int HOMING_LIMIT = 60;

		/// <summary>初期位置。</summary>
		private static readonly Vector2 firstPosition = new Vector2(-SIZE);

		/// <summary>移動速度と方角。</summary>
		private Vector2 velocity;

		/// <summary>ホーミング対応かどうか。</summary>
		private bool homing;

		/// <summary>ホーミング有効時間。</summary>
		private int homingAmount;

		/// <summary>現在座標。</summary>
		public Vector2 position
		{
			get;
			private set;
		}

		/// <summary>色。</summary>
		public Color color
		{
			get;
			private set;
		}

		/// <summary>
		/// 敵機の移動、及び接触判定をします。
		/// </summary>
		/// <param name="playerPosition">自機の座標。</param>
		/// <returns>接触した場合、true。</returns>
		public bool moveAndHitTest(Vector2 playerPosition)
		{
			const float HITAREA = Player.SIZE * 0.5f + SIZE * 0.5f;
			const float HITAREA_SQUARED = HITAREA * HITAREA;
			bool hit = (HITAREA_SQUARED > Vector2.DistanceSquared(position, playerPosition));
			if (homing && --homingAmount > 0)
			{
				initVelocity(playerPosition, velocity.Length());
			}
			position += velocity;
			return hit;
		}

		/// <summary>
		/// 敵機をアクティブにします。
		/// </summary>
		/// <param name="playerPosition">自機の座標。</param>
		/// <param name="speed">基準速度。</param>
		/// <returns>敵機をアクティブにできた場合、true。</returns>
		public bool start(Vector2 playerPosition, float speed)
		{
			bool result = !Game1.SCREEN.Contains((int)position.X, (int)position.Y);
			if (result)
			{
				startForce(playerPosition, speed);
			}
			return result;
		}

		/// <summary>
		/// 敵機を強制的にスリープにします。
		/// </summary>
		public void sleep()
		{
			position = firstPosition;
			velocity = Vector2.Zero;
		}

		/// <summary>
		/// 敵機を強制的にアクティブにします。
		/// </summary>
		/// <param name="playerPosition">自機の座標。</param>
		/// <param name="speed">基準速度。</param>
		private void startForce(Vector2 playerPosition, float speed)
		{
			Random rnd = new Random();
			float AROUND_HALF = Game1.SCREEN.Width + Game1.SCREEN.Height;
			float AROUND_HALF_QUARTER = Game1.SCREEN.Width * 2 + Game1.SCREEN.Height;
			int AROUND = (int)AROUND_HALF * 2;
			int p = rnd.Next(AROUND);
			Vector2 pos;
			if (p < Game1.SCREEN.Width || p >= AROUND_HALF &&
				p < AROUND_HALF_QUARTER)
			{
				pos.X = p % Game1.SCREEN.Width;
				pos.Y = p < AROUND_HALF ? 0 : Game1.SCREEN.Height;
			}
			else
			{
				pos.X = p < AROUND_HALF ? 0 : Game1.SCREEN.Width;
				pos.Y = p % Game1.SCREEN.Height;
			}
			position = pos;
			initVelocity(playerPosition, rnd.Next(1, 3) + speed);
			homing = rnd.Next(100) < Enemy.HOMING_PERCENTAGE;
			color = homing ? Color.Orange : Color.Red;
			homingAmount = Enemy.HOMING_LIMIT;
		}

		/// <summary>
		/// 敵機の移動速度と方角を初期化します。
		/// </summary>
		/// <param name="playerPosition">プレイヤーの位置。</param>
		/// <param name="speed">速度。</param>
		private void initVelocity(Vector2 playerPosition, float speed)
		{
			Vector2 v = playerPosition - position;
			if (v == Vector2.Zero)
			{
				// 長さが0だと単位ベクトル計算時にNaNが出るため対策
				v = Vector2.UnitX;
			}
			v.Normalize();
			velocity = v * speed;
		}
	}
}
