using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sample1_12.core
{

	/// <summary>
	/// キー入力を取得するサービスのインターフェイス。
	/// </summary>
	interface IKeyStatus
		: IGameComponent
	{

		/// <summary>キーボードの入力状態。</summary>
		KeyboardState keyboardState
		{
			get;
		}
	
	}
}
