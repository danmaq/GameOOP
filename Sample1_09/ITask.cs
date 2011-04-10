using Microsoft.Xna.Framework.Input;

namespace Sample1_09
{

	/// <summary>
	/// タスク インターフェイス。
	/// </summary>
	interface ITask
	{

		/// <summary>
		/// 初期化を行います。
		/// </summary>
		void reset();

		/// <summary>
		/// 1フレーム分の更新を行います。
		/// </summary>
		/// <param name="keyState">現在のキー入力状態。</param>
		void update(KeyboardState keyState);

		/// <summary>
		/// 1フレーム分の描画を行います。
		/// </summary>
		/// <param name="graphics">グラフィック データ。</param>
		void draw(Graphics graphics);

	}
}
