namespace Sample1_14.task.entity.chr
{

	/// <summary>敵機の情報。</summary>
	class EnemyManager
		: TaskManager<Character>
	{

		/// <summary>クラス オブジェクト。</summary>
		public static readonly EnemyManager instance = new EnemyManager();

		/// <summary>
		/// 敵機を作成します。
		/// </summary>
		/// <param name="speed">基準速度。</param>
		/// <returns>敵機が生成された場合、true。</returns>
		public bool create(float speed)
		{
			bool result = false;
			return result;
		}
	}
}
