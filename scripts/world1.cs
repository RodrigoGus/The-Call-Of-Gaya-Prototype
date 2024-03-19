using Godot;
using System;

public partial class world1 : Node2D
{
	private player _player;
	private Camera2D camera;

	public override void _Ready()
	{
		/*_player = new player();
		camera = GetNode<Camera2D>("camera");
		
		_player.FollowCamera(camera);*/
	}
	public override void _Process(double delta)
	{
	}
}
