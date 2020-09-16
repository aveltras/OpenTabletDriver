﻿using System.Numerics;
using OpenTabletDriver.Plugin.Platform.Display;

namespace OpenTabletDriver.Interop.Display
{
    internal class Display : IDisplay
    {
        internal Display(float width, float height, Vector2 position, int index = 0, float scaling = 1)
        {
            Width = width;
            Height = height;
            Position = position;
            Index = index;
            Scaling = scaling;
        }

        public int Index { private set; get; }
        public float Scaling { private set; get; }
        public float Width { private set; get; }
        public float Height { private set; get; }
        public Vector2 Position { private set; get; }

        public override string ToString()
        {
            return $"Display {Index} ({Width}x{Height}@{Position}, {Scaling}x)";
        }
    }
}