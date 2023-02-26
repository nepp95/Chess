using EpEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess
{
    public class BoardController : Entity
    {
		private int Size = 8;

		private Dictionary<Vector2i, Piece> Pieces = new Dictionary<Vector2i, Piece>();
		private Dictionary<Vector2i, Tile> Tiles = new Dictionary<Vector2i, Tile>();
		private PieceColor ActiveTurn = PieceColor.White;
		private Entity SelectedEntity;

        // OnCreate is called by the engine when a scene is started containing this entity
        void OnCreate() 
        {
			SetupBoard();
        }

        // OnUpdate is called every frame when a scene is running containing this entity
        // The 'ts' param, or timestep, contains the time passed since last frame
        void OnUpdate(float ts)
        {
			Entity selectedEntity = GetSelectedEntity();
			if (selectedEntity == null)
				return;

			//  && selectedEntity.UUID != SelectedEntity.UUID --> implicitly so, because an entity can't be both a tile and a piece
			if (IsTile(selectedEntity) && IsPiece(SelectedEntity)) 
			{
				Piece piece = GetPiece(SelectedEntity);
			
				Vector2i targetPosition = GetTile(selectedEntity).Position;
				if (IsValidMove(piece, targetPosition))
					Move(piece, targetPosition);
			}

			SelectedEntity = selectedEntity;

			if (SelectedEntity != null)
				Log.Trace(SelectedEntity.Name);
		}

		private void Move(Piece piece, Vector2i targetPosition)
		{
			Log.Trace($"Moved {piece.Name} to {targetPosition}");
		}

		private bool IsValidMove(Piece piece, Vector2i targetPosition)
		{
			// TODO: Implement
			if (piece.Color != ActiveTurn)
				return false;

			return true;
		}

		private bool IsPiece(Entity entity)
		{
			return Pieces.Where(piece => piece.Value.UUID == entity.UUID).Any();
		}

		private bool IsTile(Entity entity)
		{
			return Tiles.Where(tile => tile.Value.UUID == entity.UUID).Any();
		}

		private Piece GetPiece(Entity entity)
		{
			if (entity == null)
				return null;

			KeyValuePair<Vector2i, Piece> result = Pieces.Where(piece => piece.Value.UUID == entity.UUID).First();

			return result.Value;
		}

		private Tile GetTile(Entity entity)
		{
			if (entity == null)
				return null;

			KeyValuePair<Vector2i, Tile> result = Tiles.Where(tile => tile.Value.UUID == entity.UUID).First();

			return result.Value;
		}

		private void OnSelectionChange(Piece oldPiece, Piece newPiece)
		{

		}

		private void SetupBoard()
		{
			// Destroy entities

			// Tiles
			for (int x = 1; x < Size + 1; x++)
			{
				for (int y = 1; y < Size + 1; y++)
				{
					if (y % 2 == 0)
						if (x % 2 == 0)
							AddTile(new Vector2i(x, y), PieceColor.Black);
						else
							AddTile(new Vector2i(x, y), PieceColor.White);
					else
						if (x % 2 == 0)
							AddTile(new Vector2i(x, y), PieceColor.White);
						else
							AddTile(new Vector2i(x, y), PieceColor.Black);
				}
			}

			Log.Trace($"Created {Size * Size} tiles");

			// Pawns
			for (int x = 1; x < Size + 1; x++)
			{
				AddPiece(PieceType.Pawn, new Vector2i(x, 2), PieceColor.White);
				AddPiece(PieceType.Pawn, new Vector2i(x, 7), PieceColor.Black);
			}

			// Knights
			AddPiece(PieceType.Knight, new Vector2i(2, 1), PieceColor.White);
			AddPiece(PieceType.Knight, new Vector2i(7, 1), PieceColor.White);
			AddPiece(PieceType.Knight, new Vector2i(2, 8), PieceColor.Black);
			AddPiece(PieceType.Knight, new Vector2i(7, 8), PieceColor.Black);

			// Bishops
			AddPiece(PieceType.Bishop, new Vector2i(3, 1), PieceColor.White);
			AddPiece(PieceType.Bishop, new Vector2i(6, 1), PieceColor.White);
			AddPiece(PieceType.Bishop, new Vector2i(3, 8), PieceColor.Black);
			AddPiece(PieceType.Bishop, new Vector2i(6, 8), PieceColor.Black);

			// Rooks
			AddPiece(PieceType.Rook, new Vector2i(1, 1), PieceColor.White);
			AddPiece(PieceType.Rook, new Vector2i(8, 1), PieceColor.White);
			AddPiece(PieceType.Rook, new Vector2i(1, 8), PieceColor.Black);
			AddPiece(PieceType.Rook, new Vector2i(8, 8), PieceColor.Black);

			// Queens
			AddPiece(PieceType.Queen, new Vector2i(4, 1), PieceColor.White);
			AddPiece(PieceType.Queen, new Vector2i(4, 8), PieceColor.Black);

			// Kings
			AddPiece(PieceType.King, new Vector2i(5, 1), PieceColor.White);
			AddPiece(PieceType.King, new Vector2i(5, 8), PieceColor.Black);

			Log.Trace($"Created {Pieces.Count} pieces");
		}

		private void AddTile(Vector2i position, PieceColor color)
		{
			Entity entity = CreateEntity($"Tile{position.X}_{position.Y}");

			{
				ScriptComponent sc = entity.AddComponent<ScriptComponent>();
				sc.ClassName = typeof(Tile).ToString();
			}

			Tile tile = entity.As<Tile>();
			tile.Position = position;
			tile.Color = color;

			{
				SpriteComponent sc = tile.AddComponent<SpriteComponent>();
				if (color == PieceColor.White)
					sc.Color = TileColors.White;
				else
					sc.Color = TileColors.Black;
			}

			Tiles.Add(position, tile);
		}

		private void AddPiece(PieceType type, Vector2i position, PieceColor color)
		{
			Entity entity = CreateEntity($"{color}{type}");

			{
				ScriptComponent sc = entity.AddComponent<ScriptComponent>();
				sc.ClassName = typeof(Piece).ToString();
			}

			Piece piece = entity.As<Piece>();
			piece.Position = position;
			piece.Type = type;
			piece.Color = color;

			{
				SpriteComponent sc = entity.AddComponent<SpriteComponent>();

				switch (type)
				{
					case PieceType.Pawn: sc.TextureHandle = Asset.FindTexture($"{color}Pawn"); break;
					case PieceType.Knight: sc.TextureHandle = Asset.FindTexture($"{color}Knight"); break;
					case PieceType.Bishop: sc.TextureHandle = Asset.FindTexture($"{color}Bishop"); break;
					case PieceType.Rook: sc.TextureHandle = Asset.FindTexture($"{color}Rook"); break;
					case PieceType.Queen: sc.TextureHandle = Asset.FindTexture($"{color}Queen"); break;
					case PieceType.King: sc.TextureHandle = Asset.FindTexture($"{color}King"); break;
				}
			}

			Pieces.Add(position, piece);
		}
	}
}
