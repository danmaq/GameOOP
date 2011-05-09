using System;
using Sample1_14.state.chr;

namespace Sample1_14.task.entity.chr
{

	/// <summary>敵機の情報。</summary>
	sealed class EnemyManager
		: TaskManager<Character>
	{

		/// <summary>クラス オブジェクト。</summary>
		public static readonly EnemyManager instance = new EnemyManager();

		/// <summary>疑似乱数ジェネレータ。</summary>
		private readonly Random random = new Random();

		/// <summary>敵機の種類。</summary>
		private readonly StateEnemy[] enemyTypeList =
		{
			StateEnemyStraight.normal,
			StateEnemyHoming.instance,
			StateEnemyInferiority.instance,
		};

		/// <summary>確率の母数。</summary>
		private readonly int denominator;

		/// <summary>コンストラクタ。</summary>
		private EnemyManager()
		{
			int d = 0;
			for (int i = enemyTypeList.Length; --i >= 0; )
			{
				d += enemyTypeList[i].percentage;
			}
			denominator = d;
		}

		/// <summary>
		/// 敵機を作成します。
		/// </summary>
		/// <param name="speed">基準速度。</param>
		/// <returns>敵機が生成された場合、true。</returns>
		public bool create(float speed)
		{
			bool result = false;
			int percentage = random.Next(denominator);
			for (int i = enemyTypeList.Length; --i >= 0; )
			{
				StateEnemy state = enemyTypeList[i];
				percentage -= state.percentage;
				if (percentage < 0)
				{
					result = create(speed, state);
				}
			}
			return result;
		}

		/// <summary>
		/// 敵機を作成します。
		/// </summary>
		/// <param name="speed">基準速度。</param>
		/// <param name="state">敵機の状態。</param>
		/// <returns>敵機が生成された場合、true。</returns>
		public bool create(float speed, StateEnemy state)
		{
			bool result = false;
			return result;
		}
	}
}
