using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sample1_07
{

	/// <summary>自機の情報。</summary>
	class Player
	{

		/// <summary>大きさ。</summary>
		public const float SIZE = 64;

		/// <summary>移動速度。</summary>
		public const float SPEED = 3;

		/// <summary>自機の初期残機。</summary>
		public const int DEFAULT_AMOUNT = 2;

		/// <summary>入力を受け付けるキー一覧。</summary>
		public Keys[] acceptInputKeyList;

		/// <summary>キー入力に対応した移動方向。</summary>
		public Dictionary<Keys, Vector2> velocity;

		/// <summary>ミス猶予(残機)数。</summary>
		public int amount;

		/// <summary>現在座標。</summary>
		public Vector2 position;

		/// <summary>コンストラクタ。各種値を初期化します。</summary>
		public Player()
		{
			acceptInputKeyList =
				new Keys[] { Keys.Up, Keys.Down, Keys.Left, Keys.Right };
			velocity = new Dictionary<Keys, Vector2>();
			velocity.Add(Keys.Up, new Vector2(0, -Player.SPEED));
			velocity.Add(Keys.Down, new Vector2(0, Player.SPEED));
			velocity.Add(Keys.Left, new Vector2(-Player.SPEED, 0));
			velocity.Add(Keys.Right, new Vector2(Player.SPEED, 0));
		}

		/// <summary>キー入力に応じて移動します。</summary>
		/// <param name="keyState">現在のキー入力状態。</param>
		public void move(KeyboardState keyState)
		{
			Vector2 prev = position;
			for (int i = 0; i < acceptInputKeyList.Length; i++)
			{
				Keys key = acceptInputKeyList[i];
				if (keyState.IsKeyDown(key))
				{
					position += velocity[key];
				}
			}
			if (!Game1.SCREEN.Contains((int)position.X, (int)position.Y))
			{
				position = prev;
			}
		}

		/// <summary>1フレーム分の描画を行います。</summary>
		/// <param name="graphics">グラフィック データ。</param>
		public void draw(Graphics graphics)
		{
			graphics.spriteBatch.Draw(graphics.gameThumbnail, position,
				null, Color.White, 0f, new Vector2(Graphics.RECT * 0.5f),
				Player.SIZE / Graphics.RECT, SpriteEffects.None, 0f);
		}
	}
}
