using Godot;
using System;
using System.Collections.Generic;
using DelveInternals;
using DelveInternals.Units;

public partial class GameManager : Node2D
{
	public static GameManager Instance { get; private set; }
	public Node CurrentScene { get; set; }
	public Unit[] CurrentTeam { get; set; }
	
	private Dictionary<string, Node> Scenes { get; set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var root = GetTree().Root;
		CurrentScene = root.GetChild(root.GetChildCount() - 1);

		CurrentTeam = new Unit[3];
		Scenes = new Dictionary<string, Node>();
		
		Instance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void ChangeScene(string scenePath)
	{
		CallDeferred(nameof(DeferredChangeScene), scenePath);
	} 
	private void DeferredChangeScene(string path)
	{
		CurrentScene.QueueFree();
		var nextScene = GD.Load<PackedScene>(path);
		CurrentScene = nextScene.Instantiate();
		GetTree().Root.AddChild(CurrentScene);
		GetTree().CurrentScene = CurrentScene;
	}
}
