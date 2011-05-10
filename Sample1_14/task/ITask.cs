using Sample1_14.core;

namespace Sample1_14.task
{

	/// <summary>タスク インターフェイス。</summary>
	interface ITask
	{

		/// <summary>1フレーム分の更新を行います。</summary>
		void update();

		/// <summary>1フレーム分の描画を行います。</summary>
		/// <param name="graphics">グラフィック データ。</param>
		void draw(Graphics graphics);

		/// <summary>オブジェクトをリセットします。</summary>
		void reset();
	}
}
