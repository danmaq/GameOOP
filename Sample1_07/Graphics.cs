using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sample1_07
{

	/// <summary>
	/// スプライト バッチやコンテンツなど描画周りのデータ一覧。
	/// </summary>
	class Graphics
	{

		/// <summary>画像サイズ。</summary>
		public const float RECT = 64;

		/// <summary>スプライト バッチ。</summary>
		public SpriteBatch spriteBatch;

		/// <summary>キャラクタ用画像。</summary>
		public Texture2D gameThumbnail;

		/// <summary>フォント画像。</summary>
		public SpriteFont spriteFont;

		/// <summary>
		/// コンストラクタ。
		/// コンテンツを読み込みます。
		/// </summary>
		/// <param name="game">ゲーム メイン オブジェクト。</param>
		public Graphics(Game game)
		{
			spriteBatch = new SpriteBatch(game.GraphicsDevice);
			gameThumbnail = game.Content.Load<Texture2D>("GameThumbnail");
			spriteFont = game.Content.Load<SpriteFont>("SpriteFont");
		}
	}
}
