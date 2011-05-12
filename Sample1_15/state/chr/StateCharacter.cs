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
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		public abstract void setup(IEntityAccessor accessor);

		/// <summary>1フレーム分の更新処理を実行します。</summary>
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		public virtual void update(IEntityAccessor accessor)
		{
			Character.CharacterAccessor chr = (Character.CharacterAccessor)accessor;
			chr.position += chr.velocity;
		}

		/// <summary>1フレーム分の描画処理を実行します。</summary>
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		/// <param name="graphics">グラフィック データ。</param>
		public virtual void draw(IEntityAccessor accessor, Graphics graphics)
		{
			Character.CharacterAccessor chr = (Character.CharacterAccessor)accessor;
			graphics.spriteBatch.Draw(graphics.gameThumbnail, chr.position,
				null, chr.color, 0f, new Vector2(Graphics.RECT * 0.5f),
				chr.size / Graphics.RECT, SpriteEffects.None, 0f);
		}

		/// <summary>
		/// <para>状態が開始された時に呼び出されます。</para>
		/// <para>このメソッドは、遷移元の<c>teardown</c>よりも後に呼び出されます。</para>
		/// </summary>
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		public void teardown(IEntityAccessor accessor)
		{
		}

		/// <summary>ダメージを与えます。</summary>
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		/// <param name="value">ダメージ値(負数で回復)。</param>
		/// <returns>続行可能な場合、true。</returns>
		public abstract bool damage(Character.CharacterAccessor accessor, int value);
	}
}
