using System.Collections.Generic;

namespace Sample1_12.core
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
		/// 1フレーム分の描画を行います。
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
