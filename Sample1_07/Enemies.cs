using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Sample1_07
{

	/// <summary>
	/// 敵機の情報。
	/// </summary>
	class Enemies
	{

		/// <summary>大きさ。</summary>
		public const float SIZE = 32;

		/// <summary>最大数。</summary>
		public const int MAX = 100;

		/// <summary>敵機一覧データ。</summary>
		public Enemy[] list = new Enemy[MAX];

		/// <summary>
		/// 敵機を作成します。
		/// </summary>
		/// <param name="playerPosition">自機の座標。</param>
		/// <param name="speed">基準速度。</param>
		/// <returns>敵機を作成できた場合、true。</returns>
		public bool create(Vector2 playerPosition, float speed)
		{
			bool result = false;
			for (int i = 0; !result && i < Enemy.MAX; i++)
			{
				result = !Game1.SCREEN.Contains(
					(int)list[i].position.X, (int)list[i].position.Y);
				if (result)
				{
					list[i].initEnemy(playerPosition, speed);
				}
			}
			return result;
		}

		/// <summary>
		/// 敵機の移動、及び接触判定をします。
		/// </summary>
		/// <param name="playerPosition">自機の座標。</param>
		/// <returns>接触した場合、true。</returns>
		public bool moveAndHitTest(Vector2 playerPosition)
		{
			bool hit = false;
			for (int i = 0; !hit && i < Enemy.MAX; i++)
			{
				hit = list[i]._moveAndHitTest(playerPosition);
			}
			if (hit)
			{
				reset();
			}
			return hit;
		}

		/// <summary>
		/// 敵機を初期状態にリセットします。
		/// </summary>
		public void reset()
		{
			Vector2 firstPosition = new Vector2(-SIZE);
			for (int i = 0; i < Enemy.MAX; i++)
			{
				list[i].position = firstPosition;
				list[i].velocity = Vector2.Zero;
			}
		}
	}
}
