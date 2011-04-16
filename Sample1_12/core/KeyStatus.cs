using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sample1_12.core
{

	/// <summary>
	/// キー入力管理クラス。
	/// </summary>
	class KeyStatus
		: GameComponent, IKeyStatus
	{

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="game">ゲーム メイン オブジェクト。</param>
		public KeyStatus(Game game)
			: base(game)
		{
		}

		/// <summary>キーボードの入力状態。</summary>
		public KeyboardState keyboardState
		{
			get;
			private set;
		}

		/// <summary>
		/// 1フレーム分の更新を行います。
		/// </summary>
		/// <param name="gameTime">前フレームからの経過時間。</param>
		public override void  Update(GameTime gameTime)
		{
			keyboardState = Keyboard.GetState();
			base.Update(gameTime);
		}
	}
}
