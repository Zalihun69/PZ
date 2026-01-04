using Godot;
using System;

public partial class DialogueUI : CanvasLayer
{
	private Label _label;
	private Panel _panel; // Hozzáadjuk a panelt is

	public override void _Ready()
	{
		_panel = GetNode<Panel>("Panel");
		_label = GetNode<Label>("Panel/Label");
		
		Visible = false;

		// Beállítjuk, hogy a Label magától törje a sorokat, ha kell
		_label.AutowrapMode = TextServer.AutowrapMode.WordSmart;
	}

	public async void MutassSzoveget(string tartalom)
	{
		_label.Text = tartalom;
		
		// Ez a sor "kényszeríti" a panelt, hogy azonnal igazodjon a szöveg méretéhez
		_panel.Size = _panel.GetCombinedMinimumSize();
		
		Visible = true;

		await ToSignal(GetTree().CreateTimer(3.0f), SceneTreeTimer.SignalName.Timeout);
		
		Visible = false;
	}
}
