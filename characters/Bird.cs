using Godot;
using System;

public partial class Bird : CharacterBody2D
{
	// Gravity strength (pixels per second squared)
	[Export]
	public float Gravity = 800.0f;

	// Current velocity
	private Vector2 _velocity = Vector2.Zero;
	private AnimationPlayer _animationPlayer;

	[Export]
	public float FlapStrength = 300.0f;

	[Signal]
	public delegate void BirdCollidedEventHandler();

	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");
		_animationPlayer.Play("default");
	}

	public override void _PhysicsProcess(double delta)
	{
		// Apply gravity
		_velocity.Y += Gravity * (float)delta;
		// Move the character
		Velocity = _velocity;
		MoveAndSlide();
		// Update _velocity from the result (in case of collision)
		_velocity = Velocity;

		// Check for collisions after moving
		CheckCollisions();
	}

	// Checks for collisions after movement
	private void CheckCollisions()
	{
		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			KinematicCollision2D collision = GetSlideCollision(i);
			if (collision != null)
			{
				EmitSignal(SignalName.BirdCollided);
			}
		}
	}

	// Call this to make the bird flap upward (like Flappy Bird)
	public void Flap()
	{
		_velocity.Y = -FlapStrength;
		_animationPlayer.Play("flap");
	}

	public void Unflap()
	{
		_animationPlayer.Play("unflap");
		_animationPlayer.Play("default");
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("flap"))
		{
			Flap();
		}
		if (@event.IsActionReleased("flap"))
		{
			Unflap();
		}
	}
}
