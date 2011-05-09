using Sample1_14.task.entity;
using Sample1_14.task.entity.chr;

namespace Sample1_14.state.chr
{

	/// <summary>敵機の基底の状態。</summary>
	abstract class StateEnemy
		: StateCharacter
	{

		/// <summary>大きさ。</summary>
		private const float SIZE = 32;

		/// <summary>コンストラクタ。</summary>
		private StateEnemy()
		{
		}

		/// <summary>確率。</summary>
		public abstract float percentage
		{
			get;
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
		}
	}
}
