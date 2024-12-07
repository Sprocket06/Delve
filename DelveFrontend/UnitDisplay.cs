using Godot;
using System;
using DelveInternals.Units;

public partial class UnitDisplay : BoxContainer
{
	public Unit DisplayUnit { get; private set; }
	private Label _unitName;
	private Label _unitStats;
	private RichTextLabel _unitDescription;
	
	public override void _Ready()
	{
		_unitName = GetNode<Label>("UnitInfo/Name");
		_unitStats = GetNode<Label>("UnitInfo/Body/Stats");
		_unitDescription = GetNode<RichTextLabel>("UnitInfo/Body/Description");
	}

	public override void _Process(double delta)
	{
	}

	public void ToggleDescription()
	{
		_unitDescription.Visible = !_unitDescription.Visible;
	}

	public void SetUnit(Unit displayUnit)
	{
		DisplayUnit = displayUnit;
		_unitName.Text = displayUnit.GetName();
		_unitStats.Text = $"Health: {displayUnit.Health}\nAttack: {displayUnit.Attack}\nDefense: {displayUnit.Defense}";
		_unitDescription.Text = $"[center]{displayUnit.Description}";
		_unitDescription.SizeFlagsHorizontal = SizeFlags.ExpandFill;
	}
}
