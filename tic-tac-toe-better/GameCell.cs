using Godot;
using System;
using TicTacToeBetter;
using TicTakToe;
public partial class GameCell : Button, IComponent
{
    public FieldType FieldType { get;  set; }
    public Coordinate Location { get; private set; }
    public IMediator Mediator { get ; private set; }

    public static Color GreenColor = new Color("#32CD32");
    /// <summary>
    /// Use after updating FieldType.
    /// </summary>
    public void UpdateUI()
    {
        char futureChar = ' ';
        switch (FieldType)
        {
            case FieldType.X:
                futureChar = 'X';
                break;
            case FieldType.O:
                futureChar = 'O';
                break;
        }
        Text = "" + futureChar;
    }
    public void ChangeColor(Color color)
    {
        StyleBoxFlat newStyleBox = new StyleBoxFlat();
        newStyleBox.BgColor = color;
        AddThemeStyleboxOverride("disabled", newStyleBox);
    }


    public void Init(Coordinate location, FieldType type, IMediator mediator)
    {
        FieldType = type;
        Location = location;
        this.Mediator = mediator;
        UpdateUI();
        //Text = "x:"+location.X + "y:" + location.Y;

    }
    public override void _Ready()
    {
        Pressed += Click;
    }
    public void Click()
    {
        if (FieldType != FieldType.Empty) return;

        Mediator.Notify(this, "click");

        // Debug Info
        GD.Print(Location.X, Location.Y, FieldType);
    }

    public override void _Process(double delta)
    {
    }
}
