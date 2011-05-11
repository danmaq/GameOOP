namespace Sample1_12.core
{

	/// <summary>シーン インターフェイス。</summary>
	interface IScene
		: ITask
	{

		/// <summary>次に遷移するシーン。</summary>
		IScene next
		{
			get;
		}
	}
}
