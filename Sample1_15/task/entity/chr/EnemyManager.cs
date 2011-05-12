using System;
using Sample1_15.state.chr;

namespace Sample1_15.task.entity.chr
{

	/// <summary>敵機の情報。</summary>
	sealed class EnemyManager
		: TaskManager<Character>
	{

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
		internal EnemyManager()
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
		internal void create(float speed)
		{
			bool created = false;
			int percentage = random.Next(denominator);
			for (int i = enemyTypeList.Length; !created && --i >= 0; )
			{
				StateEnemy state = enemyTypeList[i];
				percentage -= state.percentage;
				created = percentage < 0;
				if (created)
				{
					create(speed, state);
				}
			}
		}

		/// <summary>
		/// 敵機を作成します。
		/// </summary>
		/// <param name="speed">基準速度。</param>
		/// <param name="state">敵機の状態。</param>
		internal void create(float speed, StateEnemy state)
		{
			Character chr = null;
			for (int i = tasks.Count; chr == null && --i >= 0; )
			{
				Character item = tasks[i];
				if (!item.contains)
				{
					chr = item;
				}
			}
			if (chr == null)
			{
				chr = new Character();
				tasks.Add(chr);
			}
			chr.firstVelocity = speed;
			chr.nextState = state;
		}

		/// <summary>
		/// 敵機の接触判定をします。
		/// </summary>
		/// <param name="expr">対象キャラクタ。</param>
		/// <returns>接触した場合、true。</returns>
		internal bool hitTest(Character expr)
		{
			bool result = false;
			for (int i = tasks.Count; !result && --i >= 0; )
			{
				result = tasks[i].hitTest(expr);
			}
			if (result)
			{
				for (int i = tasks.Count; --i >= 0; )
				{
					tasks[i].damage(1);
				}
			}
			return result;
		}
	}
}
