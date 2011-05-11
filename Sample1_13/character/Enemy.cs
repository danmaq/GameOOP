using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sample1_13.character.state;
using Sample1_13.core;

namespace Sample1_13.character
{

	/// <summary>敵機の情報。</summary>
	sealed class Enemy
		: ITask
	{

		/// <summary>大きさ。</summary>
		private const float SIZE = 32;

		/// <summary>疑似乱数ジェネレータ。</summary>
		private static readonly Random rnd = new Random();

		/// <summary>初期位置。</summary>
		private static readonly Vector2 firstPosition = new Vector2(-SIZE);

		/// <summary>色。</summary>
		public Color color;

		/// <summary>移動速度と方角。</summary>
		public Vector2 velocity;

		/// <summary>ホーミング有効時間。</summary>
		public int homingAmount;

		/// <summary>現在の敵機の状態。</summary>
		public IEnemyState currentState;

		/// <summary>現在座標。</summary>
		private Vector2 position;

		/// <summary>コンストラクタ。</summary>
		/// <param name="firstState">敵機の状態。</param>
		public Enemy(IEnemyState firstState)
		{
			currentState = firstState;
			setup();
		}

		/// <summary>現在活動中かどうかを取得します。</summary>
		/// <value>現在活動中である場合、true。</value>
		public bool active
		{
			get
			{
				return Game1.SCREEN.Contains((int)position.X, (int)position.Y);
			}
		}

		/// <summary>敵機を強制的にスリープにします。</summary>
		public void setup()
		{
			position = firstPosition;
			velocity = Vector2.Zero;
		}

		/// <summary>1フレーム分の更新を行います。</summary>
		public void update()
		{
			position += velocity;
			currentState.onUpdate(this);
		}

		/// <summary>敵機の移動、及び接触判定をします。</summary>
		/// <returns>接触した場合、true。</returns>
		public bool hitTest()
		{
			const float HITAREA = Player.SIZE * 0.5f + SIZE * 0.5f;
			const float HITAREA_SQUARED = HITAREA * HITAREA;
			bool hit = (HITAREA_SQUARED > Vector2.DistanceSquared(position, Player.instance.position));
			return hit;
		}

		/// <summary>1フレーム分の描画を行います。</summary>
		/// <param name="graphics">グラフィック データ。</param>
		public void draw(Graphics graphics)
		{
			const float SCALE = SIZE / Graphics.RECT;
			Vector2 origin = new Vector2(Graphics.RECT * 0.5f);
			graphics.spriteBatch.Draw(graphics.gameThumbnail, position, null, color,
				0f, new Vector2(Graphics.RECT * 0.5f), SCALE, SpriteEffects.None, 0f);
		}

		/// <summary>敵機を強制的にアクティブにします。</summary>
		/// <param name="speed">基準速度。</param>
		public void start(float speed)
		{
			float AROUND_HALF = Game1.SCREEN.Width + Game1.SCREEN.Height;
			float AROUND_HALF_QUARTER = Game1.SCREEN.Width * 2 + Game1.SCREEN.Height;
			int AROUND = (int)AROUND_HALF * 2;
			int p = rnd.Next(AROUND);
			Vector2 pos;
			if (p < Game1.SCREEN.Width || p >= AROUND_HALF &&
				p < AROUND_HALF_QUARTER)
			{
				pos.X = p % Game1.SCREEN.Width;
				pos.Y = p < AROUND_HALF ? 0 : Game1.SCREEN.Height - 1;
			}
			else
			{
				pos.X = p < AROUND_HALF ? 0 : Game1.SCREEN.Width - 1;
				pos.Y = p % Game1.SCREEN.Height;
			}
			position = pos;
			currentState.onStarted(this);
			initVelocity(rnd.Next(1, 3) + speed);
		}

		/// <summary>敵機の移動速度と方角を初期化します。</summary>
		/// <param name="speed">速度。</param>
		public void initVelocity(float speed)
		{
			Vector2 v = Player.instance.position - position;
			if (v == Vector2.Zero)
			{
				// 長さが0だと単位ベクトル計算時にNaNが出るため対策
				v = Vector2.UnitX;
			}
			v.Normalize();
			velocity = v * speed;
			currentState.onInitializedVelocity(this);
		}
	}
}
