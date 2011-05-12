using Sample1_15.core;
using Sample1_15.task.entity;

namespace Sample1_15.state
{
	/// <summary>状態表現のためのインターフェイス。</summary>
	interface IState
	{

		/// <summary>
		/// <para>状態が開始された時に呼び出されます。</para>
		/// <para>このメソッドは、遷移元の<c>teardown</c>よりも後に呼び出されます。</para>
		/// </summary>
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		void setup(IEntityAccessor accessor);

		/// <summary>1フレーム分の更新処理を実行します。</summary>
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		void update(IEntityAccessor accessor);

		/// <summary>1フレーム分の描画処理を実行します。</summary>
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		/// <param name="graphics">グラフィック データ。</param>
		void draw(IEntityAccessor accessor, Graphics graphics);

		/// <summary>
		/// <para>状態が開始された時に呼び出されます。</para>
		/// <para>このメソッドは、遷移元の<c>teardown</c>よりも後に呼び出されます。</para>
		/// </summary>
		/// <param name="accessor">この状態を適用されたオブジェクトへのアクセサ。</param>
		void teardown(IEntityAccessor accessor);
	}
}
