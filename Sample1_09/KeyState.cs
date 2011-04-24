using Microsoft.Xna.Framework.Input;

namespace Sample1_09
{

	/// <summary>
	/// キー入力管理クラス。
	/// </summary>
	class KeyState
		: ITask
	{

		/// <summary>キーボードの入力状態。</summary>
		public KeyboardState keyboardState
		{
			get;
			private set;
		}

		/// <summary>
		/// タスクを開始します。
		/// </summary>
		public void reset()
		{
			// 特にすることはない。
		}

		/// <summary>
		/// 1フレーム分の更新を行います。
		/// </summary>
		public void update(KeyboardState keyState)
		{
			keyboardState = Keyboard.GetState();
		}

		/// <summary>
		/// 1フレーム分の描画を行います。
		/// </summary>
		/// <param name="graphics">グラフィック データ。</param>
		public void draw(Graphics graphics)
		{
			// このクラスでは別段何かを描画する必要はない。
		}
	}
}
