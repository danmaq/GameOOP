using Sample1_15.core;
using Sample1_15.state;

namespace Sample1_15.task.entity
{

	/// <summary>Stateパターンにおける実体となるクラス。</summary>
	class Entity
		: ITask
	{

		/// <summary>Stateパターンにおける実体へのアクセサ。</summary>
		public class EntityAccessor
			: IEntityAccessor
		{

			/// <summary>コンストラクタ。</summary>
			/// <param name="entity">実体オブジェクト。</param>
			public EntityAccessor(Entity entity)
			{
				this.entity = entity;
			}

			/// <summary>実体オブジェクト。</summary>
			public Entity entity
			{
				get;
				private set;
			}

			/// <summary>汎用カウンタ。</summary>
			public int counter
			{
				get
				{
					return entity.counter;
				}
				set
				{
					entity.counter = value;
				}
			}
		}

		/// <summary>次に変化する状態。</summary>
		public IState nextState;

		/// <summary>コンストラクタ。</summary>
		/// <param name="firstState">初期の状態。</param>
		public Entity(IState firstState)
		{
			accessor = createAccessor();
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

		/// <summary>汎用カウンタ。</summary>
		public int counter
		{
			get;
			private set;
		}

		/// <summary>この状態を適用されたオブジェクトへのアクセサ。</summary>
		protected IEntityAccessor accessor
		{
			get;
			private set;
		}

		/// <summary>1フレーム分の更新を行います。</summary>
		public virtual void update()
		{
			currentState.update(accessor);
			commitNextState();
			counter++;
		}

		/// <summary>1フレーム分の描画を行います。</summary>
		/// <param name="graphics">グラフィック データ。</param>
		public virtual void draw(Graphics graphics)
		{
			currentState.draw(accessor, graphics);
		}

		/// <summary>オブジェクトをリセットします。</summary>
		public virtual void reset()
		{
			nextState = StateEmpty.instance;
			commitNextState();
			counter = 0;
		}

		/// <summary>Stateパターンにおける実体へのアクセサを生成します。</summary>
		/// <returns>Stateパターンにおける実体へのアクセサ。</returns>
		protected virtual IEntityAccessor createAccessor()
		{
			return new EntityAccessor(this);
		}

		/// <summary>予約していた次の状態を強制的に確定します。</summary>
		private void commitNextState()
		{
			if (nextState != null)
			{
				IState prev = currentState;
				currentState = nextState;
				nextState = null;
				prev.teardown(accessor);
				currentState.setup(accessor);
			}
		}
	}
}
