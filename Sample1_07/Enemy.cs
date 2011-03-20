using Microsoft.Xna.Framework;
using System;

namespace Sample1_07
{

	/// <summary>
	/// 敵機の情報。
	/// </summary>
	struct Enemy
	{

		/// <summary>大きさ。</summary>
		public const float SIZE = 32;

		/// <summary>最大数。</summary>
		public const int MAX = 100;

		/// <summary>ホーミング確率。</summary>
		public const int HOMING_PERCENTAGE = 20;

		/// <summary>ホーミング時間。</summary>
		public const int HOMING_LIMIT = 60;

		/// <summary>敵機一覧データ。</summary>
		public static Enemy[] enemies = new Enemy[MAX];

		/// <summary>現在座標。</summary>
		public Vector2 position;

		/// <summary>移動速度と方角。</summary>
		public Vector2 velocity;

		/// <summary>ホーミング対応かどうか。</summary>
		public bool homing;

		/// <summary>ホーミング有効時間。</summary>
		public int homingAmount;

		/// <summary>
		/// 敵機を初期状態にリセットします。
		/// </summary>
		public static void reset()
		{
			Vector2 firstPosition = new Vector2(-SIZE);
			for (int i = 0; i < Enemy.MAX; i++)
			{
				enemies[i].position = firstPosition;
				enemies[i].velocity = Vector2.Zero;
			}
		}

		/// <summary>
		/// 敵機を作成します。
		/// </summary>
		/// <param name="playerPosition">自機の座標。</param>
		/// <param name="speed">基準速度。</param>
		/// <returns>敵機を作成できた場合、true。</returns>
		public static bool create(Vector2 playerPosition, float speed)
		{
			bool result = false;
			for (int i = 0; !result && i < Enemy.MAX; i++)
			{
				result = !Game1.SCREEN.Contains(
					(int)enemies[i].position.X, (int)enemies[i].position.Y);
				if (result)
				{
					enemies[i].initEnemy(playerPosition, speed);
				}
			}
			return result;
		}

		/// <summary>
		/// 敵機の移動、及び接触判定をします。
		/// </summary>
		/// <param name="playerPosition">自機の座標。</param>
		/// <returns>接触した場合、true。</returns>
		public static bool enemyMoveAndHitTest(Vector2 playerPosition)
		{
			bool hit = false;
			const float HITAREA = Player.SIZE * 0.5f + SIZE * 0.5f;
			const float HITAREA_SQUARED = HITAREA * HITAREA;
			for (int i = 0; i < Enemy.MAX; i++)
			{
				if (Vector2.DistanceSquared(Enemy.enemies[i].position, playerPosition) <
					HITAREA_SQUARED)
				{
					hit = true;
					break;
				}
				if (enemies[i].homing && --enemies[i].homingAmount > 0)
				{
					enemies[i].initEnemy(playerPosition, enemies[i].velocity.Length());
				}
				enemies[i].position += enemies[i].velocity;
			}
			return hit;
		}

		/// <summary>
		/// 敵機をアクティブにします。
		/// </summary>
		/// <param name="playerPosition">自機の座標。</param>
		/// <param name="speed">基準速度。</param>
		public void initEnemy(Vector2 playerPosition, float speed)
		{
			Random rnd = new Random();
			float AROUND_HALF = Game1.SCREEN.Width + Game1.SCREEN.Height;
			float AROUND_HALF_QUARTER = Game1.SCREEN.Width * 2 + Game1.SCREEN.Height;
			int AROUND = (int)AROUND_HALF * 2;
			int p = rnd.Next(AROUND);
			if (p < Game1.SCREEN.Width || p >= AROUND_HALF &&
				p < AROUND_HALF_QUARTER)
			{
				position.X = p % Game1.SCREEN.Width;
				position.Y = p < AROUND_HALF ? 0 : Game1.SCREEN.Height;
			}
			else
			{
				position.X = p < AROUND_HALF ? 0 : Game1.SCREEN.Width;
				position.Y = p % Game1.SCREEN.Height;
			}
			initVelocity(playerPosition, rnd.Next(1, 3) + speed);
			homing = rnd.Next(100) < Enemy.HOMING_PERCENTAGE;
			homingAmount = Enemy.HOMING_LIMIT;
		}

		/// <summary>
		/// 敵機の移動速度と方角を初期化します。
		/// </summary>
		/// <param name="playerPosition">プレイヤーの位置。</param>
		/// <param name="speed">速度。</param>
		public void initVelocity(Vector2 playerPosition, float speed)
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
