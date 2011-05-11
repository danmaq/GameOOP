using Sample1_15.core;
using Sample1_15.state;

namespace Sample1_15.task.entity
{

	/// <summary>Stateパターンにおける実体となるクラス。</summary>
	class Entity
		: ITask
	{

		/// <summary>次に変化する状態。</summary>
		internal IState nextState;

		/// <summary>コンストラクタ。</summary>
		/// <param name="firstState">初期の状態。</param>
		internal Entity(IState firstState)
		{
			currentState = StateEmpty.instance;
			nextState = firstState;
		}

		/// <summary>現在の状態を取得します。</summary>
		/// <value>現在の状態。初期値は<c>CState.empty</c>。</value>
		internal IState currentState
		{
			get;
			private set;
		}

		/// <summary>汎用カウンタ。</summary>
		internal int counter
		{
			get;
			private set;
		}

		/// <summary>1フレーム分の更新を行います。</summary>
		public virtual void update()
		{
			currentState.update(this);
			commitNextState();
			counter++;
		}

		/// <summary>1フレーム分の描画を行います。</summary>
		/// <param name="graphics">グラフィック データ。</param>
		public virtual void draw(Graphics graphics)
		{
			currentState.draw(this, graphics);
		}

		/// <summary>オブジェクトをリセットします。</summary>
		public virtual void reset()
		{
			nextState = StateEmpty.instance;
			commitNextState();
			resetCounter();
		}

		/// <summary>汎用カウンタをリセットします。</summary>
		internal void resetCounter()
		{
			counter = 0;
		}

		/// <summary>予約していた次の状態を強制的に確定します。</summary>
		private void commitNextState()
		{
			if (nextState != null)
			{
				IState prev = currentState;
				currentState = nextState;
				nextState = null;
				prev.teardown(this);
				currentState.setup(this);
			}
		}
	}
}
