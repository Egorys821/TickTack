using Godot;
using System;
using TicTacToeBetter;
using TicTakToe;
/// <summary>
/// A single clickable cell of the game field's UI. Reports clicks to its <see cref="Mediator"/>
/// rather than mutating game state itself.
/// </summary>
public partial class GameCell : Button, IComponent
{
    /// <summary>The mark currently shown in this cell.</summary>
    public FieldType FieldType { get;  set; }

    /// <summary>This cell's position on the field.</summary>
    public Coordinate Location { get; private set; }

    /// <inheritdoc/>
    public IMediator Mediator { get ; private set; }

    /// <summary>Highlight color used by <see cref="ChangeColor"/> to mark winning cells.</summary>
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
    /// <summary>Sets the cell's disabled-state background color, used to highlight winning cells.</summary>
    /// <param name="color">The background color to apply.</param>
    public void ChangeColor(Color color)
    {
        StyleBoxFlat newStyleBox = new StyleBoxFlat();
        newStyleBox.BgColor = color;
        AddThemeStyleboxOverride("disabled", newStyleBox);
    }


    /// <summary>Sets this cell's position, mark, and mediator, and refreshes its display.</summary>
    /// <param name="location">The cell's position on the field.</param>
    /// <param name="type">The mark to show initially, typically <see cref="FieldType.Empty"/>.</param>
    /// <param name="mediator">The mediator to notify when this cell is clicked.</param>
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
    /// <summary>
    /// Handles the button's <c>Pressed</c> signal: if the cell is still empty, notifies
    /// <see cref="Mediator"/> with the event <c>"click"</c>; otherwise does nothing.
    /// </summary>
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
