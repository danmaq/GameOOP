using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sample1_11.core;

namespace Sample1_11.character
{

	/// <summary>自機の情報。</summary>
	class Player
		: ITask
	{

		/// <summary>クラス インスタンス。</summary>
		public static readonly Player instance = new Player();

		/// <summary>大きさ。</summary>
		public const float SIZE = 64;

		/// <summary>移動速度。</summary>
		private const float SPEED = 3;

		/// <summary>自機の初期残機。</summary>
		private const int DEFAULT_AMOUNT = 2;

		/// <summary>入力を受け付けるキー一覧。</summary>
		private readonly Keys[] acceptInputKeyList;

		/// <summary>キー入力に対応した移動方向。</summary>
		private readonly Dictionary<Keys, Vector2> velocity;

		/// <summary>ミス猶予(残機)数。</summary>
		private int m_amount;

		/// <summary>現在座標。</summary>
		public Vector2 position
		{
			get;
			private set;
		}

		/// <summary>ミス猶予(残機)数。</summary>
		public int amount
		{
			get
			{
				return m_amount;
			}
			private set
			{
				m_amount = value;
				amountString = value < 0 ? string.Empty : new string('*', value);
			}
		}

		/// <summary>ミス猶予(残機)数の文字列による表現。</summary>
		public string amountString
		{
			get;
			private set;
		}

		/// <summary>各種値を初期化します。</summary>
		private Player()
		{
			acceptInputKeyList =
				new Keys[] { Keys.Up, Keys.Down, Keys.Left, Keys.Right };
			velocity = new Dictionary<Keys, Vector2>();
			velocity.Add(Keys.Up, new Vector2(0, -Player.SPEED));
			velocity.Add(Keys.Down, new Vector2(0, Player.SPEED));
			velocity.Add(Keys.Left, new Vector2(-Player.SPEED, 0));
			velocity.Add(Keys.Right, new Vector2(Player.SPEED, 0));
		}

		/// <summary>残機を増やします。</summary>
		public void extend()
		{
			amount++;
		}

		/// <summary>残機を減らします。</summary>
		/// <returns>ゲームが続行可能である場合、true。</returns>
		public bool miss()
		{
			// ミスするとプレイヤーは元の座標へと戻る
			resetPosition();
			return --amount >= 0;
		}

		/// <summary>現在位置を初期化します。</summary>
		private void resetPosition()
		{
			Point center = Game1.SCREEN.Center;
			position = new Vector2(center.X, center.Y);
		}

		/// <summary>座標や残機情報を初期化します。</summary>
		public void setup()
		{
			resetPosition();
			amount = DEFAULT_AMOUNT;
		}

		/// <summary>キー入力に応じて移動します。</summary>
		public void update()
		{
			KeyboardState keyState = KeyStatus.instance.keyboardState;
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
			graphics.spriteBatch.DrawString(graphics.spriteFont,
				"PLAYER: " + amountString,
				new Vector2(600, 560), Color.Black);
			graphics.spriteBatch.Draw(graphics.gameThumbnail, position,
				null, Color.White, 0f, new Vector2(Graphics.RECT * 0.5f),
				Player.SIZE / Graphics.RECT, SpriteEffects.None, 0f);
		}
	}
}
