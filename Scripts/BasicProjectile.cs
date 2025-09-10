using Godot;
using System;

public partial class BasicProjectile : Area2D
{
	public override void _PhysicsProcess(double delta)
	{
		Position = new Vector2(Position.X, Position.Y - 2);
	}
}
