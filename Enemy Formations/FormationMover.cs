using Godot;
using System;

public partial class FormationMover : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		foreach (var child in GetChildren()) {
			if(child is Path2D){
				Node2D newChild = child as Node2D;
				newChild.Position=new Vector2(newChild.Position.X, newChild.Position.Y+1*(float)delta);
				child.GetChild<PathFollow2D>(0).ProgressRatio+=.1f*(float)delta;
			}
		}
	}
}
