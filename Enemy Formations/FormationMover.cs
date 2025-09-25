using Godot;
using System;


public partial class FormationMover : Node2D
{
	private int childCount=0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		childCount=GetChildren().Count;
	}

	// Called eveaaaaaaaaaaaary frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		childCount = 0;
		foreach (var child in GetChildren()) {
			if(child is Path2D){
				child.GetChild<PathFollow2D>(0).Progress+=100f*(float)delta;
				if(child.GetChild<PathFollow2D>(0).ProgressRatio>=0.99f) {
					child.QueueFree();
				}
				if(GodotObject.IsInstanceValid(child.GetChild(0)))
				{
					if(child.GetChild(0).GetChildCount() > 0)
					{
						if (GodotObject.IsInstanceValid(child.GetChild(0)))
						{
							childCount += 1;
						}
					}
				}
			}
		}
		if(childCount<=0) {
			GD.Print("LEAVING");
			QueueFree();
		} else
		{
			GD.Print(childCount);
		}
	}
}
