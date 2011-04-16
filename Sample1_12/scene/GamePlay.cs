using Microsoft.Xna.Framework;
using Sample1_12.character;

namespace Sample1_12.scene
{

	/// <summary>
	/// ゲームプレイ画面。
	/// </summary>
	class GamePlay
		: Scene
	{

		/// <summary>敵機一覧データ。</summary>
		private readonly EnemyManager enemies;

		/// <summary>プレイヤー。</summary>
		private readonly IPlayer player;

		/// <summary>ゲームの進行カウンタ。</summary>
		private int counter;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="game">ゲーム メイン オブジェクト。</param>
		public GamePlay(Game game)
			: base(game)
		{
			player = new Player(game);
			enemies = new EnemyManager(game);
		}

		/// <summary>
		/// ゲーム シーンの初期化を行います。
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();
			Game.Components.Add(player);
			Game.Services.AddService(typeof(IPlayer), player);
			counter = 0;
		}

		/// <summary>
		/// 1フレーム分の更新を行います。
		/// </summary>
		/// <param name="gameTime">前フレームからの経過時間。</param>
		public override void Update(GameTime gameTime)
		{
			createEnemy();
			if (enemies.hitTest() && !player.miss())
			{
				next = new Title(Game);
			}
			counter++;
			base.Update(gameTime);
		}

		/// <summary>
		/// 1フレーム分の描画を行います。
		/// </summary>
		/// <param name="gameTime">前フレームからの経過時間。</param>
		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);
		}

		/// <summary>
		/// リソースを解放します。
		/// </summary>
		/// <param name="disposing">マネージ リソースも解放するかどうか。</param>
		protected override void Dispose(bool disposing)
		{
			enemies.Dispose();
			base.Dispose(disposing);
		}

		/// <summary>
		/// 敵機を作成します。
		/// </summary>
		private void createEnemy()
		{
			if (counter % (int)MathHelper.Max(60 - counter * 0.01f, 1) == 0 &&
				enemies.create(counter * 0.001f) &&
				score.add(10))
			{
				player.extend();
			}
		}
	}
}
