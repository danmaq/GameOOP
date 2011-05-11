using Microsoft.Xna.Framework;
using Sample1_13.character;
using Sample1_13.core;

namespace Sample1_13.scene
{

	/// <summary>ゲームプレイ画面。</summary>
	class GamePlay
		: IScene
	{

		/// <summary>クラス オブジェクト。</summary>
		public static readonly IScene instance = new GamePlay();

		/// <summary>敵機一覧データ。</summary>
		private readonly EnemyManager enemies = new EnemyManager();

		/// <summary>タスク管理クラス。</summary>
		private readonly TaskManager<ITask> mgrTask = new TaskManager<ITask>();

		/// <summary>ゲームの進行カウンタ。</summary>
		private int counter;

		/// <summary>コンストラクタ。</summary>
		private GamePlay()
		{
			next = this;
			mgrTask.tasks.AddRange(new ITask[] { enemies, Player.instance, Score.instance });
		}

		/// <summary>次に遷移するシーン。</summary>
		public IScene next
		{
			get;
			private set;
		}

		/// <summary>ゲーム シーンの初期化を行います。</summary>
		public void setup()
		{
			mgrTask.setup();
			counter = 0;
		}

		/// <summary>1フレーム分の更新を行います。</summary>
		public void update()
		{
			next = this;
			mgrTask.update();
			createEnemy();
			next = (enemies.hitTest() && !Player.instance.miss()) ?
				Title.instance : this;
			counter++;
		}

		/// <summary>敵機を作成します。</summary>
		private void createEnemy()
		{
			if (counter % (int)MathHelper.Max(60 - counter * 0.01f, 1) == 0 &&
				enemies.create(counter * 0.001f) &&
				Score.instance.add(10))
			{
				Player.instance.extend();
			}
		}

		/// <summary>1フレーム分の描画を行います。</summary>
		/// <param name="graphics">グラフィック データ。</param>
		public void draw(Graphics graphics)
		{
			mgrTask.draw(graphics);
		}
	}
}
