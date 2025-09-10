using Godot;
using System;
//handles movement, mostly godot docs
public partial class Player : CharacterBody2D
{
	public int speed {get; set;} = 300;
	
	
	public void GetInput()
	{
		Vector2 inputDirection = Input.GetVector("Left", "Right", "Forward", "Back");
		Velocity = inputDirection * speed; 
		if(Velocity.X<0){
			if(Mathf.Abs(Velocity.Y)>0) {
				Rotation=-.1f;
			}
			else{
				Rotation=-.2f;
			}
		} else if (Velocity.X>0) {
			if(Mathf.Abs(Velocity.Y)>0) {
				Rotation=.1f;
			}
			else{
				Rotation=.2f;
			}
		}
		else{
			Rotation=0;
		}
	}
	
	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
	}
}
