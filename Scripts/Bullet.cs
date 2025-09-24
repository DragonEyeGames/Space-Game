using Godot;
using System;

public partial class Bullet : CharacterBody2D
{
	public bool exploding=false;
	
	public override void _Ready()
	{
		Velocity = Vector2.Right.Rotated(Rotation + Mathf.Pi / 2f) * 200;
	}
	public override void _PhysicsProcess(double delta)
	{
		Velocity = Vector2.Right.Rotated(Rotation - Mathf.Pi / 2f) * 200;
		MoveAndSlide();
	}
	
	public virtual void OnArea2DBodyEntered(Node2D body)
	{
		return;
	}
}
