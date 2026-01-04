using Godot;
using System;

public partial class DialogueTrigger : Area2D
{
	// Ez az üzenet, amit majd kiírunk. Az [Export] miatt az Editorban is átírhatod!
	[Export] public string Uzenet = "Szia! Én egy titkos üzenet vagyok.";

	public override void _Ready()
	{
		// Itt mondjuk meg a játéknak: "Figyeld, ha valaki hozzád ér!"
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		// Ellenőrizzük, hogy a Játékos lépett-e be (kicsi és nagybetűre is figyelünk)
		if (body.Name.ToString().ToLower() == "player") 
		{
			// Megkeressük a szövegdobozt a képernyőn
			var ui = GetTree().Root.FindChild("DialogueUI", true, false) as DialogueUI;

			if (ui != null)
			{
				// Ha megtaláltuk, meghívjuk a függvényét
				ui.MutassSzoveget(Uzenet);
			}
			else
			{
				// Ha nem találja, kiírja a lenti ablakba (Output), hogy baj van
				GD.Print("Hoppá! Nem találom a 'DialogueUI' nevű node-ot!");
			}
		}
	}
}
