using Microsoft.Xna.Framework.Input;
using Sample1_14.core;

namespace Sample1_14.task
{

	/// <summary>キー入力管理クラス。</summary>
	class KeyStatus
		: ITask
	{

		/// <summary>クラス オブジェクト。</summary>
		public static readonly KeyStatus instance = new KeyStatus();

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		private KeyStatus()
		{
		}

		/// <summary>キーボードの入力状態。</summary>
		public KeyboardState keyboardState
		{
			get;
			private set;
		}

		/// <summary>1フレーム分の更新を行います。</summary>
		public void update()
		{
			keyboardState = Keyboard.GetState();
		}

		/// <summary>1フレーム分の描画を行います。</summary>
		/// <param name="graphics">グラフィック データ。</param>
		public void draw(Graphics graphics)
		{
			// このクラスでは別段何かを描画する必要はない。
		}

		/// <summary>オブジェクトをリセットします。</summary>
		public void release()
		{
			// 特にすることはない。
		}
	}
}
