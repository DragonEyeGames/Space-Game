using Godot;
using System;
using System.Collections.Generic;

public partial class StarField : Node2D
{
	[Export] public int StarCount = 300;
	[Export] public Color[] StarColors = new Color[]
	{
		new Color(1, 1, 1),        // White
		new Color(1, 0.95f, 0.9f), // Warm white
		new Color(0.8f, 0.9f, 1f)  // Cool white
	};
	
	public List<ColorRect> SmallStars = new List<ColorRect>();
	public List<ColorRect> BigStars = new List<ColorRect>();

	public override void _Ready()
	{
		GD.Randomize();
		Vector2 screenSize = GetViewport().GetVisibleRect().Size;

		for (int i = 0; i < StarCount; i++)
		{
			var star = new ColorRect();
			star.Color = StarColors[GD.Randi() % StarColors.Length];

			float size = (float)GD.RandRange(1f, 3f); // Star size 1–3 px
			if(size <=2) {
				SmallStars.Add(star);
			}
			else if(size <=3) {
				BigStars.Add(star);
			}
			star.Size = new Vector2(size, size);

			star.Position = new Vector2(
				(float)GD.RandRange(0, screenSize.X),
				(float)GD.RandRange(0, screenSize.Y)
			);
			
			
			star.ZIndex=-10;
			

			Color modulate = star.Modulate;
			modulate.A = (float)GD.RandRange(0.5, 1.0); // Transparency 0.5–1.0
			star.Modulate = modulate;

			AddChild(star);
		}
	}
	
	public override void _Process(double delta)
	{
		Vector2 screenSize = GetViewport().GetVisibleRect().Size;

		foreach (ColorRect star in SmallStars)
		{
			star.Position += new Vector2(0, 2 * (float)delta);

			// Reset if off-screen
			if (star.Position.Y > screenSize.Y)
			{
				star.Position = new Vector2(
					(float)GD.RandRange(0, screenSize.X),
					1
				);
			}
		}

		foreach (ColorRect star in BigStars)
		{
			star.Position += new Vector2(0, 5 * (float)delta);

			// Reset if off-screen
			if (star.Position.Y > screenSize.Y)
			{
				star.Position = new Vector2(
					(float)GD.RandRange(0, screenSize.X),
					0
				);
			}
		}
	}
}
