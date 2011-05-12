using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sample1_15.state.scene;
using Sample1_15.task.entity;
using Sample1_15.task.entity.chr;

namespace Sample1_15.state.chr
{

	/// <summary>敵機の基底の状態。</summary>
	abstract class StateEnemy
		: StateCharacter
	{

		/// <summary>大きさ。</summary>
		private const float SIZE = 32;

		/// <summary>疑似乱数ジェネレータ。</summary>
		protected static readonly Random random = new Random();

		/// <summary>確率。</summary>
		internal readonly int percentage;

		/// <summary>乗算色。</summary>
		internal readonly Color color;

		/// <summary>コンストラクタ。</summary>
		/// <param name="percentage">確率。</param>
		/// <param name="color">乗算色。</param>
		internal StateEnemy(int percentage, Color color)
		{
			this.percentage = percentage;
			this.color = color;
		}

		/// <summary>
		/// <para>状態が開始された時に呼び出されます。</para>
		/// <para>このメソッドは、遷移元の<c>teardown</c>よりも後に呼び出されます。</para>
		/// </summary>
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		public override void setup(IEntityAccessor accessor)
		{
			Character.CharacterAccessor chr = (Character.CharacterAccessor)accessor;
			chr.counter = 0;
			chr.size = SIZE;
			chr.color = color;
			if (!chr.entity.contains)
			{
				start(chr);
			}
		}

		/// <summary>ダメージを与えます。</summary>
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		/// <param name="value">ダメージ値(負数で回復)。</param>
		/// <returns>続行可能な場合、true。</returns>
		public override bool damage(Character.CharacterAccessor accessor, int value)
		{
			accessor.entity.reset();
			return true;
		}

		/// <summary>敵機の移動速度と方角を初期化します。</summary>
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		/// <param name="speed">速度。</param>
		protected virtual void initVelocity(Character.CharacterAccessor accessor, float speed)
		{
			Character player = ScenePlay.instance.player;
			Vector2 v = player.position - accessor.position;
			if (v == Vector2.Zero)
			{
				// 長さが0だと単位ベクトル計算時にNaNが出るため対策
				v = Vector2.UnitX;
			}
			v.Normalize();
			accessor.velocity = v * speed;
		}

		/// <summary>休眠している敵機を起動します。。</summary>
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		private void start(Character.CharacterAccessor accessor)
		{
			float aroundHalf = Game1.SCREEN.Width + Game1.SCREEN.Height;
			float aroundHalf_plusQ = Game1.SCREEN.Width * 2 + Game1.SCREEN.Height;
			int around = (int)aroundHalf * 2;
			int p = random.Next(around);
			Vector2 pos;
			if (p < Game1.SCREEN.Width || p >= aroundHalf &&
				p < aroundHalf_plusQ)
			{
				pos.X = p % Game1.SCREEN.Width;
				pos.Y = p < aroundHalf ? 0 : Game1.SCREEN.Height - 1;
			}
			else
			{
				pos.X = p < aroundHalf ? 0 : Game1.SCREEN.Width - 1;
				pos.Y = p % Game1.SCREEN.Height;
			}
			accessor.position = pos;
			initVelocity(accessor, accessor.entity.firstVelocity + random.Next(1, 3));
		}
	}
}
