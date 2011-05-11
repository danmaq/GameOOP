namespace Sample1_11.core
{

	/// <summary>タスク インターフェイス。</summary>
	interface ITask
	{

		/// <summary>タスクを開始します。</summary>
		void setup();

		/// <summary>1フレーム分の更新を行います。</summary>
		void update();

		/// <summary>1フレーム分の描画を行います。</summary>
		/// <param name="graphics">グラフィック データ。</param>
		void draw(Graphics graphics);

	}
}
