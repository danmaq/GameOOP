using System;
using Sample1_11.core;

namespace Sample1_11.character
{

	/// <summary>
	/// 敵機の情報。
	/// </summary>
	class EnemyManager
		: TaskManager<Enemy>
	{

		/// <summary>ホーミング敵機の確率。</summary>
		private const int HOMING_PERCENTAGE = 20;

		/// <summary>粗悪精度敵機の確率。</summary>
		private const int INFERIORITY_PERCENTAGE = 30;

		/// <summary>疑似乱数ジェネレータ。</summary>
		private readonly Random rnd = new Random();

		/// <summary>
		/// 敵機を作成します。
		/// </summary>
		/// <param name="speed">基準速度。</param>
		public void create(float speed)
		{
			Enemy result = null;
			int percentage = rnd.Next(100);
			if (percentage - HOMING_PERCENTAGE < 0)
			{
				result = new EnemyHoming(speed);
			}
			else if (percentage - HOMING_PERCENTAGE - INFERIORITY_PERCENTAGE < 0)
			{
				result = new EnemyInferiority(speed);
			}
			else
			{
				result = new EnemyStraight(speed);
			}
			if (result != null)
			{
				tasks.Add(result);
			}
		}

		/// <summary>
		/// 敵機の移動、及び接触判定をします。
		/// </summary>
		/// <returns>接触した場合、true。</returns>
		public bool hitTest()
		{
			bool hit = false;
			int length = tasks.Count;
			for (int i = 0; !hit && i < length; i++)
			{
				hit = tasks[i].hitTest();
			}
			if (hit)
			{
				tasks.Clear();
			}
			return hit;
		}
	}
}
