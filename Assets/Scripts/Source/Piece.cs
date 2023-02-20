using EpEngine;

namespace Chess
{
	public enum PieceType
	{
		Pawn = 0,
		Knight,
		Bishop,
		Rook,
		Queen,
		King
	}

	public enum PieceColor
	{
		White = 0,
		Black
	}

	public class Piece : Entity
	{
		internal PieceType Type;
		internal PieceColor Color;

		public Vector2i Position
		{
			get
			{
				return new Vector2i((int)Translation.X, (int)Translation.Y);;
			}
			set
			{
				Translation = new Vector3(value.X, value.Y, 0.0f);
			}
		}

		// OnCreate is called by the engine when a scene is started containing this entity
		void OnCreate()
		{}

		// OnUpdate is called every frame when a scene is running containing this entity
		// The 'ts' param, or timestep, contains the time passed since last frame
		void OnUpdate(float ts)
		{}
	}
}
