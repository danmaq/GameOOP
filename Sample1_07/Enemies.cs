using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sample1_07
{

	/// <summary>敵機の情報。</summary>
	class Enemies
	{

		/// <summary>最大数。</summary>
		public const int MAX = 100;

		/// <summary>敵機一覧データ。</summary>
		public Enemy[] list = new Enemy[MAX];

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
				list[i].position = firstPosition;
				list[i].velocity = Vector2.Zero;
			}
		}

		/// <summary>1フレーム分の描画を行います。</summary>
		/// <param name="graphics">グラフィック データ。</param>
		public void draw(Graphics graphics)
		{
			const float SCALE = -Enemy.SIZE / Graphics.RECT;
			Vector2 origin = new Vector2(Graphics.RECT * 0.5f);
			for (int i = 0; i < MAX; i++)
			{
				graphics.spriteBatch.Draw(graphics.gameThumbnail, list[i].position,
					null, list[i].homing ? Color.Orange : Color.Red,
					0f, origin, SCALE, SpriteEffects.None, 0f);
			}
		}
	}
}
