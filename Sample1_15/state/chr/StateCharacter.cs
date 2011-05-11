using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sample1_15.core;
using Sample1_15.task.entity;
using Sample1_15.task.entity.chr;

namespace Sample1_15.state.chr
{

	/// <summary>キャラクタの基底状態。</summary>
	abstract class StateCharacter
		: IState
	{

		/// <summary>
		/// <para>状態が開始された時に呼び出されます。</para>
		/// <para>このメソッドは、遷移元の<c>teardown</c>よりも後に呼び出されます。</para>
		/// </summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		public abstract void setup(Entity entity);

		/// <summary>1フレーム分の更新処理を実行します。</summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		public virtual void update(Entity entity)
		{
			Character chr = (Character)entity;
			chr.position += chr.velocity;
		}

		/// <summary>1フレーム分の描画処理を実行します。</summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		/// <param name="graphics">グラフィック データ。</param>
		public virtual void draw(Entity entity, Graphics graphics)
		{
			Character chr = (Character)entity;
			graphics.spriteBatch.Draw(graphics.gameThumbnail, chr.position,
				null, chr.color, 0f, new Vector2(Graphics.RECT * 0.5f),
				chr.size / Graphics.RECT, SpriteEffects.None, 0f);
		}

		/// <summary>
		/// <para>状態が開始された時に呼び出されます。</para>
		/// <para>このメソッドは、遷移元の<c>teardown</c>よりも後に呼び出されます。</para>
		/// </summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		public abstract void teardown(Entity entity);

		/// <summary>ダメージを与えます。</summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		/// <param name="value">ダメージ値(負数で回復)。</param>
		/// <returns>続行可能な場合、true。</returns>
		public abstract bool damage(Character entity, int value);
	}
}
