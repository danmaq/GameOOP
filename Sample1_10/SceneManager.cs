namespace Sample1_10
{

	/// <summary>
	/// シーン管理クラス。
	/// </summary>
	class SceneManager
		: ITask
	{

		/// <summary>現在のシーン。</summary>
		public IScene nowScene;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="first">最初のシーン。</param>
		public SceneManager(IScene first)
		{
			changeScene(first);
		}

		/// <summary>
		/// タスクを開始します。
		/// </summary>
		public void setup()
		{
			// 特にすることはない。
		}

		/// <summary>
		/// 1フレーム分の更新を行います。
		/// </summary>
		public void update()
		{
			nowScene.update();
			changeScene(nowScene.next);
		}

		/// <summary>
		/// 描画します。
		/// </summary>
		/// <param name="graphics">グラフィック データ。</param>
		public void draw(Graphics graphics)
		{
			nowScene.draw(graphics);
		}

		/// <summary>
		/// シーンを切り替えます。
		/// </summary>
		/// <param name="next">次のシーン。</param>
		private void changeScene(IScene next)
		{
			if (nowScene != next)
			{
				nowScene = next;
				if (next != null)
				{
					next.setup();
				}
			}
		}
	}
}
