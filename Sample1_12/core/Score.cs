using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sample1_12.core
{

	/// <summary>
	/// スコア情報。
	/// </summary>
	class Score
		: DrawableGameComponent, IScore
	{

		/// <summary>エクステンドの閾値。</summary>
		private const int EXTEND_THRESHOLD = 500;

		/// <summary>前フレームのスコア。</summary>
		private int prev;

		/// <summary>描画周りのデータ。</summary>
		private IGraphicsData graphics;

		/// <summary>現在のスコアを描画するかどうか。</summary>
		public bool drawNowScore
		{
			get;
			set;
		}

		/// <summary>現在のスコア。</summary>
		public int now
		{
			get;
			private set;
		}

		/// <summary>ハイスコア。</summary>
		public int highest
		{
			get;
			private set;
		}

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="game">ゲーム メイン オブジェクト。</param>
		public Score(Game game)
			: base(game)
		{
			Enabled = false;
			Visible = false;
		}

		/// <summary>
		/// スコアをリセットします。
		/// </summary>
		public void setup()
		{
			now = 0;
			prev = 0;
			drawNowScore = true;
		}

		/// <summary>
		/// スコアを加算します。
		/// </summary>
		/// <param name="score">加算されるスコア値。</param>
		/// <returns>エクステンド該当となる場合、true。</returns>
		public bool add(int score)
		{
			now += score;
			bool extend = now % EXTEND_THRESHOLD < prev % EXTEND_THRESHOLD;
			prev = now;
			if (highest < now)
			{
				highest = now;
			}
			return extend;
		}

		/// <summary>
		/// コンポーネントの初期化を行います。
		/// </summary>
		public override void Initialize()
		{
			graphics = (IGraphicsData)Game.Services.GetService(typeof(IGraphicsData));
			Enabled = true;
			Visible = true;
			base.Initialize();
		}

		/// <summary>
		/// 1フレーム分の描画を行います。
		/// </summary>
		/// <param name="gameTime">前フレームからの経過時間。</param>
		public override void Draw(GameTime gameTime)
		{
			if (drawNowScore)
			{
				graphics.spriteBatch.DrawString(graphics.spriteFont,
					"SCORE: " + now.ToString(),
					new Vector2(300, 560), Color.Black);
			}
			graphics.spriteBatch.DrawString(graphics.spriteFont,
				"HISCORE: " + highest.ToString(), new Vector2(0, 560), Color.Black);
			base.Draw(gameTime);
		}
	}
}
