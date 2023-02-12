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
		public PieceType Type;
		public PieceColor PieceColor;
		public Vector2i Position;

		public Piece(PieceType type, Vector2i position, PieceColor color)
		{
			this.Type = type;
			this.Position = position;
			this.PieceColor = color;

			AddComponent<SpriteComponent>();
		}
	}
}
