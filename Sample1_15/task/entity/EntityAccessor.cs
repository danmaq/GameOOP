using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample1_15.task.entity
{

	/// <summary>Entityへのアクセサ クラス。</summary>
	class EntityAccessor
	{

		/// <summary>Entity本体オブジェクト。</summary>
		public readonly Entity entity;

		/// <summary>コンストラクタ。</summary>
		/// <param name="entity">Entity本体オブジェクト。</param>
		public EntityAccessor(Entity entity)
		{
			this.entity = entity;
		}

		
	}
}
