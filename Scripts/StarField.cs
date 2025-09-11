using Godot;
using System;
using System.Collections.Generic;

public partial class StarField : Node2D
{
	[Export] public int StarCount = 300;
	[Export] public Texture2D StarTexture; // Assign a circular texture in the Inspector
	[Export] public Color[] StarColors = new Color[]
	{
		new Color(1, 1, 1),        // White
		new Color(1, 0.95f, 0.9f), // Warm white
		new Color(0.8f, 0.9f, 1f)  // Cool white
	};

	private List<Sprite2D> SmallStars = new List<Sprite2D>();
	private List<Sprite2D> BigStars = new List<Sprite2D>();

	public override void _Ready()
	{
		GD.Randomize();
		Vector2 screenSize = GetViewport().GetVisibleRect().Size;

		for (int i = 0; i < StarCount; i++)
		{
			var star = new Sprite2D
			{
				Texture = StarTexture,
				Modulate = StarColors[GD.Randi() % StarColors.Length],
				ZIndex = -10
			};

			// Random scale instead of size
			float scale = (float)GD.RandRange(0.01f, 0.03f); // Scale range
			star.Scale = new Vector2(scale, scale);

			// Categorize stars
			if (scale <= 0.4f)
				SmallStars.Add(star);
			else
				BigStars.Add(star);

			star.Position = new Vector2(
				(float)GD.RandRange(0, screenSize.X),
				(float)GD.RandRange(0, screenSize.Y)
			);

			// Random alpha
			Color modulate = star.Modulate;
			modulate.A = (float)GD.RandRange(0.5f, 1.0f);
			star.Modulate = modulate;

			AddChild(star);
		}
	}

	public override void _Process(double delta)
	{
		Vector2 screenSize = GetViewport().GetVisibleRect().Size;

		foreach (var star in SmallStars)
		{
			star.Position += new Vector2(0, 20f * (float)delta);
			if (star.Position.Y > screenSize.Y)
			{
				star.Position = new Vector2(
					(float)GD.RandRange(0, screenSize.X),
					0
				);
			}
		}

		foreach (var star in BigStars)
		{
			star.Position += new Vector2(0, 50f * (float)delta);
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
