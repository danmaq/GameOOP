using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sample1_12.core
{

	/// <summary>
	/// スプライト バッチやコンテンツなど描画周りのデータ一覧。
	/// </summary>
	class Graphics
		: IGraphicsData
	{

		/// <summary>画面矩形情報。</summary>
		public Rectangle screenSize
		{
			get;
			private set;
		}

		/// <summary>スプライト バッチ。</summary>
		public SpriteBatch spriteBatch
		{
			get;
			private set;
		}

		/// <summary>フォント画像。</summary>
		public SpriteFont spriteFont
		{
			get;
			private set;
		}

		/// <summary>キャラクタ用画像。</summary>
		public Texture2D gameThumbnail
		{
			get;
			private set;
		}

		/// <summary>キャラクタ用画像の中心位置。</summary>
		public Vector2 gameThumbnailOrigin
		{
			get;
			private set;
		}

		/// <summary>キャラクタ用画像の幅。</summary>
		public float gameThumbnailWidth
		{
			get;
			private set;
		}

		/// <summary>
		/// コンストラクタ。
		/// コンテンツを読み込みます。
		/// </summary>
		/// <param name="game">ゲーム メイン オブジェクト。</param>
		public Graphics(Game game)
		{
			screenSize = new Rectangle(0, 0, 800, 600);
			spriteBatch = new SpriteBatch(game.GraphicsDevice);
			spriteFont = game.Content.Load<SpriteFont>("SpriteFont");
			gameThumbnail = game.Content.Load<Texture2D>("GameThumbnail");
			gameThumbnailWidth = gameThumbnail.Width;
			gameThumbnailOrigin =
				new Vector2(gameThumbnail.Width, gameThumbnail.Height) * 0.5f;
		}
	}
}
