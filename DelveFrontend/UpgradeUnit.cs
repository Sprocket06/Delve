using Godot;
using System;
using DelveInternals.Units;

public partial class UpgradeUnit : Control
{
	private Label _gold;
	private UnitDisplay _unitDisplay;
	private Button _buyTraining;
	private Button _buyArmor;
	private Button _buyWeapon;
	private Button _backButton;
	private Unit _modifyingUnit;
	public override void _Ready()
	{
		_modifyingUnit = GameManager.Instance.CurrentTeam[TeamSelect.CurrentModifyingSlot];
		_backButton = GetNode<Button>("BackButton");
		_unitDisplay = GetNode<UnitDisplay>("TeamUnits/Unit1/unit_display");
		_unitDisplay.SetUnit(_modifyingUnit);

		_backButton.Pressed += Back;

		_buyArmor = GetNode<Button>("TeamUnits/Unit1/armor");
		_buyWeapon = GetNode<Button>("TeamUnits/Unit1/weapon");
		_buyTraining = GetNode<Button>("TeamUnits/Unit1/endurance");

		_buyArmor.Pressed += TryUpgradeArmor;
		_buyTraining.Pressed += TryUpgradeHealth;
		_buyWeapon.Pressed += TryUpgradeWeapon;

		_gold = GetNode<Label>("player_gold");
		_gold.Text = $"Gold: {GameManager.Instance.Gold}";
	}

	public override void _Process(double delta)
	{
	}

	private void UpdateDisplay()
	{
		_gold.Text = $"Gold: {GameManager.Instance.Gold}";
		_unitDisplay.SetUnit(_modifyingUnit);
	}

	private void TryUpgradeArmor()
	{
		if (GameManager.Instance.Gold >= 100)
		{
			GameManager.Instance.Gold -= 100;
			_modifyingUnit.Defense += 1;
			UpdateDisplay();
		}
	}
	private void TryUpgradeWeapon()
	{
		if (GameManager.Instance.Gold >= 100)
		{
			GameManager.Instance.Gold -= 100;
			_modifyingUnit.Attack += 1;
			UpdateDisplay();
		}
	}
	private void TryUpgradeHealth()
	{
		if (GameManager.Instance.Gold >= 100)
		{
			GameManager.Instance.Gold -= 100;
			_modifyingUnit.Health += 10;
			UpdateDisplay();
		}
	}
	private void Back()
	{
		GameManager.Instance.ChangeScene("res://team_select.tscn");
	}
}
