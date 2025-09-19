using Godot;
using System;

public partial class Gate : StaticBody2D
{
	[Export]
	public float Speed = 100.0f;

	[Signal]
	public delegate void GateClearedEventHandler();

	public override void _Ready()
	{
		var gateCleared = GetNodeOrNull<Area2D>("%GateCleared");
		gateCleared.BodyEntered += (body) =>
		{
			EmitSignal(SignalName.GateCleared);
		};
	}

	public override void _PhysicsProcess(double delta)
	{
		// Move the gate left based on speed
		// If the gate is off-screen to the left
		if (Position.X < -600)
		{
			var yPos = GD.RandRange(-250, 250);
			Position = Position with { X = 800, Y = yPos };
		}
		else
		{
			Position += new Vector2(-Speed * (float)delta, 0);
		}

		base._PhysicsProcess(delta);
	}
}
