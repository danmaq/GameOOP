using Microsoft.Xna.Framework;
using Sample1_11.core;

namespace Sample1_11.character
{

	/// <summary>
	/// 敵機の情報。
	/// </summary>
	class EnemyManager
		: TaskManager
	{

		/// <summary>
		/// 敵機を作成します。
		/// </summary>
		/// <param name="playerPosition">自機の座標。</param>
		/// <param name="speed">基準速度。</param>
		/// <returns>敵機を作成できた場合、true。</returns>
		public bool create(Vector2 playerPosition, float speed)
		{
			bool result = false;
			int length = tasks.Count;
			for (int i = 0; !result && i < length; i++)
			{
				result = ((Enemy)tasks[i]).start(playerPosition, speed);
			}
			if (!result)
			{
				Enemy enemy = new Enemy();
				enemy.start(playerPosition, speed);
				tasks.Add(enemy);
				result = true;
			}
			return result;
		}

		/// <summary>
		/// 敵機の移動、及び接触判定をします。
		/// </summary>
		/// <param name="playerPosition">自機の座標。</param>
		/// <returns>接触した場合、true。</returns>
		public bool hitTest(Vector2 playerPosition)
		{
			bool hit = false;
			int length = tasks.Count;
			for (int i = 0; !hit && i < length; i++)
			{
				hit = ((Enemy)tasks[i]).hitTest(playerPosition);
			}
			if (hit)
			{
				setup();
			}
			return hit;
		}
	}
}
