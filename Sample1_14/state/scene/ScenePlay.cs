using Microsoft.Xna.Framework;
using Sample1_14.core;
using Sample1_14.state.chr;
using Sample1_14.task;
using Sample1_14.task.entity;
using Sample1_14.task.entity.chr;

namespace Sample1_14.state.scene
{

	/// <summary>ゲームプレイ シーンの状態。</summary>
	sealed class ScenePlay
		: IState
	{

		/// <summary>クラス オブジェクト。</summary>
		public static readonly ScenePlay instance = new ScenePlay();

		/// <summary>プレイヤー。</summary>
		public readonly Character player = new Character();

		/// <summary>タスク管理クラス。</summary>
		private readonly TaskManager<ITask> mgrTask = new TaskManager<ITask>();

		/// <summary>敵機管理クラス。</summary>
		private readonly EnemyManager mgrEnemy = new EnemyManager();

		/// <summary>コンストラクタ。</summary>
		private ScenePlay()
		{
			mgrTask.tasks.AddRange(new ITask[] { mgrEnemy, player });
		}

		/// <summary>
		/// <para>状態が開始された時に呼び出されます。</para>
		/// <para>このメソッドは、遷移元の<c>teardown</c>よりも後に呼び出されます。</para>
		/// </summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		public void setup(Entity entity)
		{
			player.nextState = StatePlayer.instance;
			Score.instance.drawNowScore = true;
			entity.counter = 0;
		}

		/// <summary>1フレーム分の更新処理を実行します。</summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		public void update(Entity entity)
		{
			mgrTask.update();
			createEnemy(entity);
			if (mgrEnemy.hitTest(player) && !player.damage(1))
			{
				entity.nextState = SceneTitle.instance;
			}
		}

		/// <summary>1フレーム分の描画処理を実行します。</summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		/// <param name="graphics">グラフィック データ。</param>
		public void draw(Entity entity, Graphics graphics)
		{
			mgrTask.draw(graphics);
		}

		/// <summary>
		/// <para>状態が開始された時に呼び出されます。</para>
		/// <para>このメソッドは、遷移元の<c>teardown</c>よりも後に呼び出されます。</para>
		/// </summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		public void teardown(Entity entity)
		{
			mgrTask.reset();
			Score.instance.reset();
		}

		/// <summary>
		/// 敵機を作成します。
		/// </summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		private void createEnemy(Entity entity)
		{
			int counter = entity.counter;
			if (counter % (int)MathHelper.Max(60 - counter * 0.01f, 1) == 0)
			{
				mgrEnemy.create(counter * 0.001f);
				if(Score.instance.add(10))
				{
					player.damage(-1);
				}
			}
		}
	}
}
