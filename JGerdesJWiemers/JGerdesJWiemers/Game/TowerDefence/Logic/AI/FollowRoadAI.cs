﻿using FarseerPhysics;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.TowerDefence.Tiles;
using Microsoft.Xna.Framework;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence.Logic.AI
{
    class FollowRoadAI
    {
        public delegate void DestinationChangedHandler(Tile destination);

        public event DestinationChangedHandler OnDestinationChanged;
        public event DestinationChangedHandler OnOnDespawn;

        private Map _map;
        private Direction _direction;
        private Tile _destination;

        private class Direction
        {
            public Direction(int x, int y)
            {
                PositionChange = new Vector2i(x, y);
            }

            public Direction Left;
            public Direction Right;
            public Vector2i PositionChange;
        }

        public FollowRoadAI(Map map)
        {
            _map = map;
            Direction up = new Direction(0, -1);
            Direction right = new Direction(1, 0);
            Direction down = new Direction(0, 1);
            Direction left = new Direction(-1, 0);

            up.Left = left;
            up.Right = right;
            right.Left = up;
            right.Right = down;
            down.Left = right;
            down.Right = left;
            left.Left = down;
            left.Right = up;

            _direction = right;
        }

        public void Update(Body body)
        {
            Vector2 position = ConvertUnits.ToDisplayUnits(body.WorldCenter);
            Tile destination = findDestination(_map.GetTileIndexAtMapPoint(position.X, position.Y));
            if (destination != _destination)
            {
                _destination = destination;
                OnDestinationChanged(_destination);
            }
            Tile t = _map.GetTileAtMapPoint(body.Position.X, body.Position.Y);
            if (t.GetType() == TileType.DespawnTile)
            {
                if (OnOnDespawn != null) OnOnDespawn(t);
            }
        }

        private Tile findDestination(Vector2i currentTileIndex)
        {
            Direction direction = _direction;
            Vector2i nextTileIndex = currentTileIndex + direction.PositionChange;
            Tile nextTile = _map.GetTileByIndex(nextTileIndex.X, nextTileIndex.Y);


            bool runnable = (nextTile == null || !(nextTile.GetType() == TileType.RoadTile || nextTile.GetType() == TileType.SpawnTile || nextTile.GetType() == TileType.DespawnTile));
            //Try to go right
            if (runnable)
            {
                
                direction = _direction.Right;
                nextTileIndex = currentTileIndex + direction.PositionChange;
                nextTile = _map.GetTileByIndex(nextTileIndex.X, nextTileIndex.Y);
                runnable = (nextTile == null || !(nextTile.GetType() == TileType.RoadTile || nextTile.GetType() == TileType.SpawnTile || nextTile.GetType() == TileType.DespawnTile));
            }
            //Try to go left
            if (runnable)
            {
               
                direction = _direction.Left;
                nextTileIndex = currentTileIndex + direction.PositionChange;
                nextTile = _map.GetTileByIndex(nextTileIndex.X, nextTileIndex.Y);
                runnable = (nextTile == null || !(nextTile.GetType() == TileType.RoadTile || nextTile.GetType() == TileType.SpawnTile || nextTile.GetType() == TileType.DespawnTile));
            }
            //Try to turn around
            if (runnable)
            {
                direction = _direction.Right.Right;
                nextTileIndex = currentTileIndex + direction.PositionChange;
                nextTile = _map.GetTileByIndex(nextTileIndex.X, nextTileIndex.Y);
            }

            _direction = direction;
            return nextTile;
        }
    }
}
