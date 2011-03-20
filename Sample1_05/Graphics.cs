using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sample1_05
{

	/// <summary>
	/// スプライト バッチやコンテンツなど描画周りのデータ一覧。
	/// </summary>
	struct Graphics
	{

		/// <summary>スプライト バッチ。</summary>
		public SpriteBatch spriteBatch;

		/// <summary>キャラクタ用画像。</summary>
		public Texture2D gameThumbnail;

		/// <summary>フォント画像。</summary>
		public SpriteFont spriteFont;
	}
}
