using System;
using Sample1_12.core;

namespace Sample1_12.character
{

	/// <summary>敵機の情報。</summary>
	class EnemyManager
		: TaskManager<Enemy>
	{

		/// <summary>ホーミング敵機の確率。</summary>
		private const int HOMING_PERCENTAGE = 20;

		/// <summary>粗悪精度敵機の確率。</summary>
		private const int INFERIORITY_PERCENTAGE = 30;

		/// <summary>疑似乱数ジェネレータ。</summary>
		private readonly Random rnd = new Random();

		/// <summary>敵機を作成します。</summary>
		/// <param name="speed">基準速度。</param>
		public bool create(float speed)
		{
			bool result = false;
			int percentage = rnd.Next(100);
			if (percentage - HOMING_PERCENTAGE < 0)
			{
				result = create<EnemyHoming>(speed);
			}
			else if (percentage - HOMING_PERCENTAGE - INFERIORITY_PERCENTAGE < 0)
			{
				result = create<EnemyInferiority>(speed);
			}
			else
			{
				result = create<EnemyStraight>(speed);
			}
			return result;
		}

		/// <summary>敵機を作成します。</summary>
		/// <param name="speed">基準速度。</param>
		/// <returns>敵機を作成できた場合、true。</returns>
		public bool create<T>(float speed)
			where T : Enemy, new()
		{
			bool result = false;
			int length = tasks.Count;
			for (int i = 0; !result && i < length; i++)
			{
				if (tasks[i] is T)
				{
					result = tasks[i].start(speed);
				}
			}
			if (!result)
			{
				T enemy = new T();
				enemy.start(speed);
				tasks.Add(enemy);
				result = true;
			}
			return result;
		}

		/// <summary>敵機の移動、及び接触判定をします。</summary>
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
				setup();
			}
			return hit;
		}
	}
}
