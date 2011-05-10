using System;
using Sample1_13.character.state;
using Sample1_13.core;

namespace Sample1_13.character
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

		/// <summary>
		/// 敵機を作成します。
		/// </summary>
		/// <param name="speed">基準速度。</param>
		/// <returns>敵機が生成された場合、true。</returns>
		public bool create(float speed)
		{
			bool result = false;
			int percentage = rnd.Next(100);
			if (percentage - HOMING_PERCENTAGE < 0)
			{
				result = create(speed, EnemyHoming.instance);
			}
			else if (percentage - HOMING_PERCENTAGE - INFERIORITY_PERCENTAGE < 0)
			{
				result = create(speed, EnemyInferiority.instance);
			}
			else
			{
				result = create(speed, EnemyStraight.instance);
			}
			return result;
		}

		/// <summary>
		/// 敵機を作成します。
		/// </summary>
		/// <param name="speed">基準速度。</param>
		/// <param name="state">敵機の状態。</param>
		/// <returns>敵機を作成できた場合、true。</returns>
		public bool create(float speed, IEnemyState state)
		{
			bool result = false;
			int length = tasks.Count;
			for (int i = 0; !result && i < length; i++)
			{
				result = !tasks[i].active;
				if (result)
				{
					Enemy enemy = tasks[i];
					enemy.currentState = state;
					enemy.start(speed);
				}
			}
			if (!result)
			{
				Enemy enemy = new Enemy(state);
				enemy.start(speed);
				tasks.Add(enemy);
				result = true;
			}
			return result;
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
				setup();
			}
			return hit;
		}
	}
}
