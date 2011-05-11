using Sample1_14.core;
using Sample1_14.state;

namespace Sample1_14.task.entity
{

	/// <summary>Stateパターンにおける実体となるクラス。</summary>
	class Entity
		: ITask
	{

		/// <summary>次に変化する状態。</summary>
		public IState nextState;

		/// <summary>汎用カウンタ。</summary>
		public int counter;

		/// <summary>コンストラクタ。</summary>
		/// <param name="firstState">初期の状態。</param>
		public Entity(IState firstState)
		{
			currentState = StateEmpty.instance;
			nextState = firstState;
		}

		/// <summary>現在の状態を取得します。</summary>
		/// <value>現在の状態。初期値は<c>CState.empty</c>。</value>
		public IState currentState
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
			counter = 0;
		}

		/// <summary>予約していた次の状態を強制的に確定します。</summary>
		public void commitNextState()
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
