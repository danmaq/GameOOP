using Microsoft.Xna.Framework;
using Sample1_15.core;
using Sample1_15.state.chr;
using Sample1_15.task;
using Sample1_15.task.entity;
using Sample1_15.task.entity.chr;
using Sample1_15.task.entity.score;

namespace Sample1_15.state.scene
{

	/// <summary>ゲームプレイ シーンの状態。</summary>
	sealed class ScenePlay
		: IState
	{

		/// <summary>クラス オブジェクト。</summary>
		internal static readonly ScenePlay instance = new ScenePlay();

		/// <summary>プレイヤー。</summary>
		internal readonly Character player = new Character();

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
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		public void setup(IEntityAccessor accessor)
		{
			player.nextState = StatePlayer.instance;
			Score.instance.drawNowScore = true;
			accessor.counter = 0;
		}

		/// <summary>1フレーム分の更新処理を実行します。</summary>
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		public void update(IEntityAccessor accessor)
		{
			mgrTask.update();
			createEnemy(accessor);
			if (mgrEnemy.hitTest(player) && !player.damage(1))
			{
				accessor.entity.nextState = SceneTitle.instance;
			}
		}

		/// <summary>1フレーム分の描画処理を実行します。</summary>
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		/// <param name="graphics">グラフィック データ。</param>
		public void draw(IEntityAccessor accessor, Graphics graphics)
		{
			mgrTask.draw(graphics);
		}

		/// <summary>
		/// <para>状態が開始された時に呼び出されます。</para>
		/// <para>このメソッドは、遷移元の<c>teardown</c>よりも後に呼び出されます。</para>
		/// </summary>
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		public void teardown(IEntityAccessor accessor)
		{
			mgrTask.reset();
			Score.instance.reset();
		}

		/// <summary>
		/// 敵機を作成します。
		/// </summary>
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		private void createEnemy(IEntityAccessor accessor)
		{
			int counter = accessor.counter;
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
