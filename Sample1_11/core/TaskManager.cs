namespace Sample1_11.core
{

	/// <summary>
	/// タスク管理クラス。
	/// </summary>
	class TaskManager
		: ITask
	{

		/// <summary>タスク一覧。</summary>
		private readonly ITask[] tasks;

		/// <summary>タスク数。</summary>
		private readonly int length;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="tasks">タスク一覧。</param>
		public TaskManager(ITask[] tasks)
		{
			this.tasks = tasks;
			length = tasks.Length;
		}

		/// <summary>
		/// タスクを開始します。
		/// </summary>
		public void setup()
		{
			for (int i = 0; i < length; i++)
			{
				tasks[i].setup();
			}
		}

		/// <summary>
		/// 1フレーム分の更新を行います。
		/// </summary>
		public void update()
		{
			for (int i = 0; i < length; i++)
			{
				tasks[i].update();
			}
		}

		/// <summary>
		/// 描画します。
		/// </summary>
		/// <param name="graphics">グラフィック データ。</param>
		public void draw(Graphics graphics)
		{
			for (int i = 0; i < length; i++)
			{
				tasks[i].draw(graphics);
			}
		}
	}
}
