using Godot;
using System;

public partial class Bullet : CharacterBody2D
{
	public bool exploding=false;
	
	public override void _Ready()
	{
		
	}
	public override void _PhysicsProcess(double delta)
	{
		
	}
	
	public virtual void OnArea2DBodyEntered(Node2D body)
	{
		
	}
}
