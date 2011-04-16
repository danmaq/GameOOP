using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sample1_12.core
{

	/// <summary>
	/// スプライト バッチやコンテンツなど描画周りのデータ
	/// サービスを取得するためのインターフェイス。
	/// </summary>
	interface IGraphicsData
	{

		/// <summary>画面矩形情報。</summary>
		Rectangle screenSize
		{
			get;
		}

		/// <summary>スプライト バッチ。</summary>
		SpriteBatch spriteBatch
		{
			get;
		}

		/// <summary>フォント画像。</summary>
		SpriteFont spriteFont
		{
			get;
		}

		/// <summary>キャラクタ用画像。</summary>
		Texture2D gameThumbnail
		{
			get;
		}

		/// <summary>キャラクタ用画像の中心位置。</summary>
		Vector2 gameThumbnailOrigin
		{
			get;
		}

		/// <summary>キャラクタ用画像の幅。</summary>
		float gameThumbnailWidth
		{
			get;
		}
	}
}
