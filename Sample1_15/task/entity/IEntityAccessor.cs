namespace Sample1_15.task.entity
{

	/// <summary>Stateパターンにおける実体へのアクセサのためのインターフェイス。</summary>
	interface IEntityAccessor
	{

		/// <summary>実体オブジェクト。</summary>
		Entity entity
		{
			get;
		}

		/// <summary>汎用カウンタ。</summary>
		int counter
		{
			get;
			set;
		}
	}
}
