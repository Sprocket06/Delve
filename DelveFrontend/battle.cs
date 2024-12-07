using Godot;
using System;

public partial class battle : Control
{
	private Label _battleResults;
	private Button _continueBtn;
	
	public override void _Ready()
	{
		_battleResults = GetNode<Label>("results");
		_battleResults.Text =
			$"Rooms Cleared: {GameManager.Instance.LastRoomsCleared}\nGold Earned: {GameManager.Instance.LastGoldCollected}";
		_continueBtn = GetNode<Button>("continue");
		_continueBtn.Pressed += BackToTeamSelect;
	}

	public override void _Process(double delta)
	{
	}

	private void BackToTeamSelect()
	{
		GameManager.Instance.ChangeScene("res://team_select.tscn");
	}
}
