using System.Collections.Generic;

namespace Sample1_11.core
{

	/// <summary>
	/// タスク管理クラス。
	/// </summary>
	class TaskManager<T>
		: ITask
		where T : ITask
	{

		/// <summary>タスク一覧。</summary>
		public readonly List<T> tasks = new List<T>();

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		public TaskManager()
		{
		}

		/// <summary>
		/// タスクを開始します。
		/// </summary>
		public void setup()
		{
			int length = tasks.Count;
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
			int length = tasks.Count;
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
			int length = tasks.Count;
			for (int i = 0; i < length; i++)
			{
				tasks[i].draw(graphics);
			}
		}
	}
}
