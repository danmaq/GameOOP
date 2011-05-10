using Microsoft.Xna.Framework.Input;

namespace Sample1_10
{

	/// <summary>キー入力管理クラス。</summary>
	class KeyState
		: ITask
	{

		/// <summary>クラス オブジェクト。</summary>
		public static readonly KeyState instance = new KeyState();

		/// <summary>コンストラクタ。</summary>
		private KeyState()
		{
		}

		/// <summary>キーボードの入力状態。</summary>
		public KeyboardState keyboardState
		{
			get;
			private set;
		}

		/// <summary>タスクを開始します。</summary>
		public void setup()
		{
			// 特にすることはない。
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
	}
}
