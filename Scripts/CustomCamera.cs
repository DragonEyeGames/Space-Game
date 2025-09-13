using Godot;
using System;

public partial class CustomCamera : Camera2D
{
	public Vector2 origin;
	public bool shaking=false;
	[Export]
	public int power = 4;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		origin = GlobalPosition;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(shaking) {
			GlobalPosition=new Vector2(origin.X+GD.RandRange(-power, power), origin.Y+GD.RandRange(-power, power));
		}
	}
	
}
