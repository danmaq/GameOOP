namespace Sample1_13.character.state
{

	/// <summary>敵機の状態を示すインターフェイス。</summary>
	interface IEnemyState
	{

		/// <summary>敵機がアクティブになったときに呼び出されます。</summary>
		/// <param name="entity">対象の敵機。</param>
		void onStarted(Enemy entity);

		/// <summary>1フレーム分の更新を行う時に呼び出されます。</summary>
		/// <param name="entity">対象の敵機。</param>
		void onUpdate(Enemy entity);

		/// <summary>敵機の移動速度と方角が初期化された時に呼び出されます。</summary>
		/// <param name="entity">対象の敵機。</param>
		void onInitializedVelocity(Enemy entity);
	}
}
