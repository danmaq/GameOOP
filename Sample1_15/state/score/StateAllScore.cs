using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sample1_14.core;
using Sample1_14.task.entity;

namespace Sample1_14.state.score
{

	/// <summary>スコア全情報を描画する状態。</summary>
	sealed class StateAllScore
		: IState
	{

		/// <summary>クラス オブジェクト。</summary>
		public static readonly IState instance = new StateAllScore();

		/// <summary>コンストラクタ。</summary>
		private StateAllScore()
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
				"SCORE: " + score.now.ToString(), new Vector2(300, 560), Color.Black);
			StateHiScoreOnly.instance.draw(entity, graphics);
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
