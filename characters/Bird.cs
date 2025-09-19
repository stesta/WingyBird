using Godot;
using System;

public partial class Bird : CharacterBody2D
{
	[Export]
	public float Gravity = 800.0f;

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
		Velocity = _velocity;
		MoveAndSlide();

		// Update _velocity from the result (in case of collision)
		_velocity = Velocity;
		CheckCollisions();
	}

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
