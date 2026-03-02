using Godot;
using System;

public partial class AliceFly : CharacterBody2D
{
	const float Gravity = 800.0f;
	const float UpSpeed = -350.0f;
	[Export] private AnimationPlayer _animationPlayer;
	[Export] private AnimatedSprite2D _aliceSprite;

	[Signal] public delegate void OnGameOverEventHandler();//游戏结束信号
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity =Velocity;
		velocity.Y +=Gravity*(float)delta;//重力
		if(Input.IsActionJustPressed("fly")==true)
		{
			//Position -=new Vector2(0,20);
			velocity.Y = UpSpeed;
			_animationPlayer.Play("power");
		}

		Velocity=velocity;
		MoveAndSlide();//移动物理引擎接管
		
		if(IsOnFloor())
		{
			GameOver();
		}
	}

	public void GameOver()
	{
		_aliceSprite.Stop();
		SetPhysicsProcess(false);
		GD.Print("You Died!");
		EmitSignal(SignalName.OnGameOver);
	}
}
