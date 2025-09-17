using Godot;
using System;


public partial class FormationMover : Node2D
{
	
	[Export]
	float firePoint = .2f;
	bool fired=false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		foreach (var child in GetChildren()) {
			if(child is Path2D){
				child.GetChild<PathFollow2D>(0).ProgressRatio+=.1f*(float)delta;
				if(child.GetChild<PathFollow2D>(0).ProgressRatio>=0.99f) {
					QueueFree();
				}
				else if(child.GetChild<PathFollow2D>(0).ProgressRatio>=firePoint && fired==false) {
					foreach (var child2 in GetChildren()) {
						if(child2 is Path2D){
							child2.GetChild<PathFollow2D>(0).GetChild<Enemy>(0).Fire();
							fired=true;
						}
					}
				}
			}
		}
	}
}
