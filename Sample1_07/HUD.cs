using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sample1_07
{

	/// <summary>
	/// HUD表示のためのクラス。
	/// </summary>
	class HUD
	{

		/// <summary>自機データ。</summary>
		public Player player;

		/// <summary>スコア データ。</summary>
		public Score score;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="player">自機データ。</param>
		/// <param name="score">スコア データ。</param>
		public HUD(Player player, Score score)
		{
			this.player = player;
			this.score = score;
		}

		/// <summary>
		/// 描画します。
		/// </summary>
		/// <param name="graphics">グラフィック データ。</param>
		/// <param name="all">全情報を描画するかどうか。</param>
		public void draw(Graphics graphics, bool all)
		{
			if (all)
			{
				graphics.spriteBatch.DrawString(graphics.spriteFont,
					"SCORE: " + score.now.ToString(),
					new Vector2(300, 560), Color.Black);
				graphics.spriteBatch.DrawString(graphics.spriteFont,
					"PLAYER: " + new string('*', player.amount),
					new Vector2(600, 560), Color.Black);
			}
			graphics.spriteBatch.DrawString(graphics.spriteFont,
				"HISCORE: " + score.highest.ToString(), new Vector2(0, 560), Color.Black);
		}
	}
}
