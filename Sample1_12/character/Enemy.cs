using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sample1_12.core;

namespace Sample1_12.character
{

	/// <summary>
	/// 敵機の情報。
	/// </summary>
	abstract class Enemy
		: DrawableGameComponent
	{

		/// <summary>大きさ。</summary>
		private const float SIZE = 32;

		/// <summary>疑似乱数ジェネレータ。</summary>
		protected static readonly Random rnd = new Random();

		/// <summary>初期位置。</summary>
		private static readonly Vector2 firstPosition = new Vector2(-SIZE);

		/// <summary>移動速度と方角。</summary>
		protected Vector2 velocity;

		/// <summary>現在座標。</summary>
		private Vector2 position;

		/// <summary>色。</summary>
		private Color color;

		/// <summary>描画周りのデータ。</summary>
		private IGraphicsData graphics;

		/// <summary>プレイヤー情報。</summary>
		private IPlayer player;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="game">ゲーム メイン オブジェクト。</param>
		/// <param name="color">色。</param>
		public Enemy(Game game, Color color)
			: base(game)
		{
			this.color = color;
			Enabled = false;
			Visible = false;
		}

		/// <summary>
		/// ゲーム シーンの初期化を行います。
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();
			graphics = (IGraphicsData)Game.Services.GetService(typeof(IGraphicsData));
			player = (IPlayer)Game.Services.GetService(typeof(IPlayer));
			sleep();
			Enabled = true;
			Visible = true;
		}

		/// <summary>
		/// 1フレーム分の更新を行います。
		/// </summary>
		/// <param name="gameTime">前フレームからの経過時間。</param>
		public override void Update(GameTime gameTime)
		{
			position += velocity;
			base.Update(gameTime);
		}

		/// <summary>
		/// 1フレーム分の描画を行います。
		/// </summary>
		/// <param name="gameTime">前フレームからの経過時間。</param>
		public override void Draw(GameTime gameTime)
		{
			Vector2 origin = new Vector2(graphics.gameThumbnailWidth * 0.5f);
			graphics.spriteBatch.Draw(graphics.gameThumbnail, position, null, color,
				0f, graphics.gameThumbnailOrigin, SIZE / graphics.gameThumbnailWidth,
				SpriteEffects.None, 0f);
			base.Draw(gameTime);
		}

		/// <summary>
		/// 敵機を強制的にスリープにします。
		/// </summary>
		public void sleep()
		{
			position = firstPosition;
			velocity = Vector2.Zero;
		}

		/// <summary>
		/// 敵機の移動、及び接触判定をします。
		/// </summary>
		/// <returns>接触した場合、true。</returns>
		public bool hitTest()
		{
			const float HITAREA = Player.SIZE * 0.5f + SIZE * 0.5f;
			const float HITAREA_SQUARED = HITAREA * HITAREA;
			bool hit = (HITAREA_SQUARED > Vector2.DistanceSquared(position, player.position));
			return hit;
		}

		/// <summary>
		/// 敵機をアクティブにします。
		/// </summary>
		/// <param name="speed">基準速度。</param>
		/// <returns>敵機をアクティブにできた場合、true。</returns>
		public virtual bool start(float speed)
		{
			bool result = !graphics.screenSize.Contains((int)position.X, (int)position.Y);
			if (result)
			{
				startForce(speed);
			}
			return result;
		}

		/// <summary>
		/// 敵機を強制的にアクティブにします。
		/// </summary>
		/// <param name="speed">基準速度。</param>
		private void startForce(float speed)
		{
			float AROUND_HALF = graphics.screenSize.Width + graphics.screenSize.Height;
			float AROUND_HALF_QUARTER = graphics.screenSize.Width * 2 + graphics.screenSize.Height;
			int AROUND = (int)AROUND_HALF * 2;
			int p = rnd.Next(AROUND);
			Vector2 pos;
			if (p < graphics.screenSize.Width || p >= AROUND_HALF &&
				p < AROUND_HALF_QUARTER)
			{
				pos.X = p % graphics.screenSize.Width;
				pos.Y = p < AROUND_HALF ? 0 : graphics.screenSize.Height;
			}
			else
			{
				pos.X = p < AROUND_HALF ? 0 : graphics.screenSize.Width;
				pos.Y = p % graphics.screenSize.Height;
			}
			position = pos;
			initVelocity(rnd.Next(1, 3) + speed);
		}

		/// <summary>
		/// 敵機の移動速度と方角を初期化します。
		/// </summary>
		/// <param name="speed">速度。</param>
		protected virtual void initVelocity(float speed)
		{
			Vector2 v = player.position - position;
			if (v == Vector2.Zero)
			{
				// 長さが0だと単位ベクトル計算時にNaNが出るため対策
				v = Vector2.UnitX;
			}
			v.Normalize();
			velocity = v * speed;
		}
	}
}
