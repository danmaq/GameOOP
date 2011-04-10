using Microsoft.Xna.Framework;

namespace Sample1_10
{

	/// <summary>
	/// ゲームプレイ画面。
	/// </summary>
	class GamePlay
		: IScene
	{

		/// <summary>クラス オブジェクト。</summary>
		public static readonly IScene instance = new GamePlay();

		/// <summary>敵機一覧データ。</summary>
		private readonly Enemies enemies = new Enemies();

		/// <summary>自機データ。</summary>
		private readonly Player player = new Player();

		/// <summary>タスク管理クラス。</summary>
		private readonly TaskManager mgrTask;

		/// <summary>ゲームの進行カウンタ。</summary>
		private int counter;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		private GamePlay()
		{
			next = this;
			mgrTask = new TaskManager(new ITask[] { enemies, player, Score.instance });
		}

		/// <summary>次に遷移するシーン。</summary>
		public IScene next
		{
			get;
			private set;
		}

		/// <summary>
		/// ゲーム シーンの初期化を行います。
		/// </summary>
		public void setup()
		{
			mgrTask.setup();
			counter = 0;
		}

		/// <summary>
		/// 1フレーム分の更新を行います。
		/// </summary>
		public void update()
		{
			next = this;
			mgrTask.update();
			createEnemy();
			next = (enemies.hitTest(player.position) && !player.miss()) ?
				Title.instance : this;
			counter++;
		}

		/// <summary>
		/// 敵機を作成します。
		/// </summary>
		private void createEnemy()
		{
			if (counter % (int)MathHelper.Max(60 - counter * 0.01f, 1) == 0 &&
				enemies.create(player.position, counter * 0.001f) &&
				Score.instance.add(10))
			{
				player.extend();
			}
		}

		/// <summary>
		/// 1フレーム分の描画を行います。
		/// </summary>
		/// <param name="graphics">グラフィック データ。</param>
		public void draw(Graphics graphics)
		{
			mgrTask.draw(graphics);
		}
	}
}
