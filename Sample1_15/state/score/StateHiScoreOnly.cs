using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sample1_15.core;
using Sample1_15.task.entity;
using Sample1_15.task.entity.score;

namespace Sample1_15.state.score
{

	/// <summary>ハイスコアを描画する状態。</summary>
	sealed class StateHiScoreOnly
		: IState
	{

		/// <summary>クラス オブジェクト。</summary>
		internal static readonly IState instance = new StateHiScoreOnly();

		/// <summary>コンストラクタ。</summary>
		private StateHiScoreOnly()
		{
		}

		/// <summary>
		/// <para>状態が開始された時に呼び出されます。</para>
		/// <para>このメソッドは、遷移元の<c>teardown</c>よりも後に呼び出されます。</para>
		/// </summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		public void setup(Entity entity)
		{
		}

		/// <summary>1フレーム分の更新処理を実行します。</summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		public void update(Entity entity)
		{
		}

		/// <summary>1フレーム分の描画処理を実行します。</summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		/// <param name="graphics">グラフィック データ。</param>
		public void draw(Entity entity, Graphics graphics)
		{
			Score score = (Score)entity;
			graphics.spriteBatch.DrawString(graphics.spriteFont,
				"HISCORE: " + score.highest.ToString(), new Vector2(0, 560), Color.Black);
		}

		/// <summary>
		/// <para>状態が開始された時に呼び出されます。</para>
		/// <para>このメソッドは、遷移元の<c>teardown</c>よりも後に呼び出されます。</para>
		/// </summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		public void teardown(Entity entity)
		{
		}
	}
}
