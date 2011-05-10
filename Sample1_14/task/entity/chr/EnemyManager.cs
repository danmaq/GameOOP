using System;
using Sample1_14.state.chr;
using Microsoft.Xna.Framework;

namespace Sample1_14.task.entity.chr
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
		public EnemyManager()
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
				result = percentage < 0;
				if (result)
				{
					create(speed, state);
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
		public void create(float speed, StateEnemy state)
		{
			Character chr;
			bool created = false;
			for (int i = tasks.Count; !created && --i >= 0; )
			{
				chr = tasks[i];
				created = !chr.contains;
				if (created)
				{
					chr.nextState = state;
					chr.velocity = Vector2.UnitX * speed;
				}
			}
			if (!created)
			{
				chr = new Character();
				chr.nextState = state;
				tasks.Add(chr);
			}
		}

		/// <summary>
		/// 敵機の接触判定をします。
		/// </summary>
		/// <param name="expr">対象キャラクタ。</param>
		/// <returns>接触した場合、true。</returns>
		public bool hitTest(Character expr)
		{
			bool result = false;
			for (int i = tasks.Count; !result && --i >= 0; )
			{
				result = tasks[i].hitTest(expr);
			}
			if (result)
			{
				for (int i = tasks.Count; !result && --i >= 0; )
				{
					tasks[i].damage(1);
				}
			}
			return result;
		}
	}
}
