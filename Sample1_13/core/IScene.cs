namespace Sample1_13.core
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
