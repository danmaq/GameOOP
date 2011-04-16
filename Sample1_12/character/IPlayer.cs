using Microsoft.Xna.Framework;

namespace Sample1_12.character
{

	/// <summary>
	/// 自機の情報。
	/// </summary>
	interface IPlayer
		: IGameComponent
	{
		/// <summary>現在座標。</summary>
		Vector2 position
		{
			get;
		}

		/// <summary>ミス猶予(残機)数。</summary>
		int amount
		{
			get;
		}

		/// <summary>
		/// 残機を増やします。
		/// </summary>
		void extend();

		/// <summary>
		/// 残機を減らします。
		/// </summary>
		/// <returns>ゲームが続行可能である場合、true。</returns>
		bool miss();
	}
}
