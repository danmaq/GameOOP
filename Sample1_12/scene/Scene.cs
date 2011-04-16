using Microsoft.Xna.Framework;
using Sample1_12.core;

namespace Sample1_12.scene
{

	/// <summary>
	/// シーンの基底となるクラス。
	/// </summary>
	class Scene
		: DrawableGameComponent
	{

		/// <summary>次に遷移されるシーン。</summary>
		protected IGameComponent next;

		/// <summary>描画周りのデータ。</summary>
		protected IGraphicsData graphics
		{
			get;
			private set;
		}

		/// <summary>キー入力情報。</summary>
		protected IKeyStatus keyStatus
		{
			get;
			private set;
		}

		/// <summary>スコア管理クラス。</summary>
		protected IScore score
		{
			get;
			private set;
		}

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="game">ゲーム メイン オブジェクト。</param>
		public Scene(Game game)
			: base(game)
		{
			Enabled = false;
			Visible = false;
		}

		/// <summary>
		/// ゲーム シーンの初期化を行います。
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();
			graphics = (IGraphicsData)Game.Services.GetService(typeof(IGraphicsData));
			keyStatus = (IKeyStatus)Game.Services.GetService(typeof(IKeyStatus));
			score = (IScore)Game.Services.GetService(typeof(IScore));
			Enabled = true;
			Visible = true;
		}

		/// <summary>
		/// 1フレーム分の更新を行います。
		/// </summary>
		/// <param name="gameTime">前フレームからの経過時間。</param>
		public override void Update(GameTime gameTime)
		{
			if (next != null)
			{
				Game.Components.Remove(this);
				Game.Components.Add(next);
			}
			base.Update(gameTime);
		}
	}
}
