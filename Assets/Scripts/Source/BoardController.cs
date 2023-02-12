using EpEngine;
using System;
using System.Collections.Generic;

namespace Chess
{
    public class BoardController : Entity
    {
		public int Size = 8;

		private Dictionary<Vector2, Entity> Tiles = new Dictionary<Vector2, Entity>();
		private Dictionary<Vector2, Piece> Pieces = new Dictionary<Vector2, Piece>();

        // OnCreate is called by the engine when a scene is started containing this entity
        void OnCreate() 
        {
			Vector4 whiteColor = new Vector4((float)245 / 255, (float)231 / 255, 192 / (float)255, 1.0f);
			Vector4 blackColor = new Vector4((float)101 / 255, (float)68 / 255, 59 / (float)255, 1.0f);

			for (int x = 1; x < Size + 1; x++)
			{
				for (int y = 1; y < Size + 1; y++)
				{
					string name = $"Tile{x}_{y}";
					Entity entity = CreateEntity(name);
					
					entity.AddComponent<SpriteComponent>();
					if (y % 2 == 0)
					{
						if (x % 2 == 0)
							entity.Color = blackColor;
						else
							entity.Color = whiteColor;
					} else
					{
						if (x % 2 == 0)
							entity.Color = whiteColor;
						else
							entity.Color = blackColor;
					}

					entity.Translation = new Vector3(x, y, 0.0f);

					Tiles.Add(new Vector2(x, y), entity);
				}
			}

			SetupBoard();
        }

        // OnUpdate is called every frame when a scene is running containing this entity
        // The 'ts' param, or timestep, contains the time passed since last frame
        void OnUpdate(float ts)
        {

        }

		private void SetupBoard()
		{
			// Pawns
			for (int x = 1; x < Size + 1; x++)
			{
				Piece whitePawn = new Piece(PieceType.Pawn, new Vector2i(x, 2), PieceColor.White);
				Piece blackPawn = new Piece(PieceType.Pawn, new Vector2i(x, 7), PieceColor.Black);
			}
		}
	}
}
