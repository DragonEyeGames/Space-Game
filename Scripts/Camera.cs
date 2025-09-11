using Godot;
using System;

public partial class Camera : Camera2D
{
	public override void _Process(double delta) 
	{
		this.Position = this.Position - new Vector2(0, .5f);
	}
}
