using Godot;
using System;

public partial class player : CharacterBody2D
{
	public const float Speed = 200.0f;
	public const float JumpVelocity = -400.0f;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private NodePath animationNodePath = "anim";
	public AnimatedSprite2D animation;
	public bool isJumping = false;
	public bool hasJumped = false;
	int inputDirection = 1;
	private RemoteTransform2D remoteTransform;

	public override void _Ready()
	{
		this.remoteTransform = GetNode<RemoteTransform2D>("remote"); // Inicialize o objeto remoteTransform antes de usá-lo
		this.animation = GetNode<AnimatedSprite2D>(this.animationNodePath);
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (IsOnFloor())
		{
			if (Input.IsActionJustPressed("up"))
			{
				velocity.Y = JumpVelocity;
				this.isJumping = true;
				this.hasJumped = true;
			}
			else this.isJumping = false;
		}
		else
		{
			velocity.Y += this.gravity * (float)delta;
			
			if (!this.isJumping) this.animation.Play("fall");
			else this.animation.Play("jump");
		}

		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			if (Input.IsActionPressed("right")) this.inputDirection = 1;
			if (Input.IsActionPressed("left")) this.inputDirection = -1;
			
			this.animation.Scale = new Vector2(this.inputDirection, this.animation.Scale.Y);
			
			if (!this.isJumping) this.animation.Play("run");
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			if (IsOnFloor()) this.animation.Play("idle");
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void _on_hurtbox_body_entered(Node2D body) //OnHurtboxBodyEntered
	{
		if (body.IsInGroup("players")) GD.Print("players");
		else GD.Print("not in players");
		
		if (body.IsInGroup("enemies")) GD.Print("enemies");
		else GD.Print("not in enemies");
		
		GD.Print(Owner.GetGroups());
	}

	/*public void FollowCamera(Camera2D camera)
	{
		this.remoteTransform.RemotePath = camera.GetPath();
	}*/
}
