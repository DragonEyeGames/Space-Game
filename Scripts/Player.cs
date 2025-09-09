using Godot;
using System;
//handles movement, mostly godot docs
public partial class Player : CharacterBody2D
{
	public int speed {get; set;} = 400;
	
	
	public void GetInput()
	{
		Vector2 inputDirection = Input.GetVector("Left", "Right", "Forward", "Back");
		Velocity = inputDirection * speed; 
	}
	
	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
	}
}
