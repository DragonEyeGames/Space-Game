using Godot;
using System;

public partial class HealthKitPath : Node2D
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
				child.GetChild<PathFollow2D>(0).Progress+=50f*(float)delta;
				if(child.GetChild<PathFollow2D>(0).ProgressRatio>=0.99f) {
					QueueFree();
				}
			}
		}
	}


}
