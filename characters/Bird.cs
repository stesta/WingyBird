using Godot;
using System;

public partial class Bird : CharacterBody2D
{
	// Gravity strength (pixels per second squared)
	[Export]
	public float Gravity = 800.0f;

	// Current velocity
	private Vector2 _velocity = Vector2.Zero;

	[Export]
	public float FlapStrength = 300.0f;

	public override void _PhysicsProcess(double delta)
	{
		// Apply gravity
		_velocity.Y += Gravity * (float)delta;
		// Move the character
		Velocity = _velocity;
		MoveAndSlide();
		// Update _velocity from the result (in case of collision)
		_velocity = Velocity;
	}

	// Call this to make the bird flap upward (like Flappy Bird)
	public void Flap()
	{
		_velocity.Y = -FlapStrength;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("flap"))
		{
			Flap();
		}
	}
}
