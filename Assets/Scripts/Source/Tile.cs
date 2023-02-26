using EpEngine;
using System;

namespace Chess
{
	public class TileColors
	{
		public static Vector4 White => new Vector4((float)245 / 255, (float)231 / 255, (float)192 / 255, 1);
		public static Vector4 Black => new Vector4((float)101 / 255, (float)68 / 255, (float)59 / 255, 1);
	}

	public class Tile : Entity
	{
		public PieceColor Color;

		private SpriteComponent SpriteComponent;

		public Vector2i Position
		{
			get
			{
				return new Vector2i((int)Translation.X, (int)Translation.Y); ;
			}
			set
			{
				Translation = new Vector3(value.X, value.Y, 0.0f);
			}
		}

		// OnCreate is called by the engine when a scene is started containing this entity
		void OnCreate()
		{
			SpriteComponent = GetComponent<SpriteComponent>();	
		}

		// OnUpdate is called every frame when a scene is running containing this entity
		// The 'ts' param, or timestep, contains the time passed since last frame
		void OnUpdate(float ts)
		{
		}
	}
}
