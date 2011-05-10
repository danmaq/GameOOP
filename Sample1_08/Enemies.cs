using Microsoft.Xna.Framework;

namespace Sample1_08
{

	/// <summary>敵機の情報。</summary>
	class Enemies
	{

		/// <summary>最大数。</summary>
		private const int MAX = 100;

		/// <summary>敵機一覧データ。</summary>
		private Enemy[] list = new Enemy[MAX];

		/// <summary>敵機を作成します。</summary>
		/// <param name="playerPosition">自機の座標。</param>
		/// <param name="speed">基準速度。</param>
		/// <returns>敵機を作成できた場合、true。</returns>
		public bool create(Vector2 playerPosition, float speed)
		{
			bool result = false;
			for (int i = 0; !result && i < MAX; i++)
			{
				result = list[i].start(playerPosition, speed);
			}
			return result;
		}

		/// <summary>敵機の移動、及び接触判定をします。</summary>
		/// <param name="playerPosition">自機の座標。</param>
		/// <returns>接触した場合、true。</returns>
		public bool moveAndHitTest(Vector2 playerPosition)
		{
			bool hit = false;
			for (int i = 0; !hit && i < MAX; i++)
			{
				hit = list[i].moveAndHitTest(playerPosition);
			}
			if (hit)
			{
				reset();
			}
			return hit;
		}

		/// <summary>敵機を初期状態にリセットします。</summary>
		public void reset()
		{
			Vector2 firstPosition = new Vector2(-Enemy.SIZE);
			for (int i = 0; i < MAX; i++)
			{
				list[i].sleep();
			}
		}

		/// <summary>1フレーム分の描画を行います。</summary>
		/// <param name="graphics">グラフィック データ。</param>
		public void draw(Graphics graphics)
		{
			for (int i = 0; i < MAX; i++)
			{
				list[i].draw(graphics);
			}
		}
	}
}
