using Sample1_14.task.entity;
using Sample1_14.task.entity.chr;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;

namespace Sample1_14.state.chr
{

	/// <summary>敵機の基底の状態。</summary>
	abstract class StateEnemy
		: StateCharacter
	{

		/// <summary>大きさ。</summary>
		private const float SIZE = 32;

		/// <summary>疑似乱数ジェネレータ。</summary>
		protected static readonly Random rnd = new Random();

		/// <summary>確率。</summary>
		public readonly int percentage;

		/// <summary>乗算色。</summary>
		public readonly Color color;

		/// <summary>コンストラクタ。</summary>
		/// <param name="percentage">確率。</param>
		/// <param name="color">乗算色。</param>
		public StateEnemy(int percentage, Color color)
		{
			this.percentage = percentage;
			this.color = color;
		}

		/// <summary>
		/// <para>状態が開始された時に呼び出されます。</para>
		/// <para>このメソッドは、遷移元の<c>teardown</c>よりも後に呼び出されます。</para>
		/// </summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		public override void setup(Entity entity)
		{
			Character chr = (Character)entity;
			chr.size = SIZE;
			chr.color = color;
			if (!chr.contains)
			{
				start(entity);
			}
		}

		private void start(Entity entity)
		{
			float aroundHalf = Game1.SCREEN.Width + Game1.SCREEN.Height;
			float aroundHalf_plusQ = Game1.SCREEN.Width * 2 + Game1.SCREEN.Height;
			int around = (int)aroundHalf * 2;
			int p = rnd.Next(around);
			Vector2 pos;
			if (p < Game1.SCREEN.Width || p >= aroundHalf &&
				p < aroundHalf_plusQ)
			{
				pos.X = p % Game1.SCREEN.Width;
				pos.Y = p < aroundHalf ? 0 : Game1.SCREEN.Height;
			}
			else
			{
				pos.X = p < aroundHalf ? 0 : Game1.SCREEN.Width;
				pos.Y = p % Game1.SCREEN.Height;
			}
			Character chr = (Character)entity;
			chr.position = pos;
		}
	}
}
