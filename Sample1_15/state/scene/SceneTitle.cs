using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sample1_15.core;
using Sample1_15.task;
using Sample1_15.task.entity;
using Sample1_15.task.entity.score;

namespace Sample1_15.state.scene
{

	/// <summary>タイトル シーンの状態。</summary>
	sealed class SceneTitle
		: IState
	{

		/// <summary>クラス オブジェクト。</summary>
		internal static readonly IState instance = new SceneTitle();

		/// <summary>コンストラクタ。</summary>
		private SceneTitle()
		{
		}

		/// <summary>
		/// <para>状態が開始された時に呼び出されます。</para>
		/// <para>このメソッドは、遷移元の<c>teardown</c>よりも後に呼び出されます。</para>
		/// </summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		public void setup(Entity entity)
		{
			Score.instance.drawNowScore = false;
		}

		/// <summary>1フレーム分の更新処理を実行します。</summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		public void update(Entity entity)
		{
			KeyboardState keyState = KeyStatus.instance.keyboardState;
			IState nextState = null;
			if (keyState.IsKeyDown(Keys.Escape))
			{
				nextState = StateEmpty.instance;
			}
			if (keyState.IsKeyDown(Keys.Space))
			{
				// ゲーム開始
				nextState = ScenePlay.instance;
			}
			entity.nextState = nextState;
		}

		/// <summary>1フレーム分の描画処理を実行します。</summary>
		/// <param name="entity">この状態を適用されたオブジェクト。</param>
		/// <param name="graphics">グラフィック データ。</param>
		public void draw(Entity entity, Graphics graphics)
		{
			graphics.spriteBatch.DrawString(
				graphics.spriteFont, "SAMPLE 1", new Vector2(200, 100),
				Color.Black, 0f, Vector2.Zero, 5f, SpriteEffects.None, 0f);
			graphics.spriteBatch.DrawString(graphics.spriteFont,
				"PUSH SPACE KEY.", new Vector2(340, 400), Color.Black);
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
