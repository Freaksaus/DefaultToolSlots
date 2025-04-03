using StardewModdingAPI.Utilities;

namespace DefaultToolSlots;

/// <summary>
/// Configuration settings
/// </summary>
internal sealed class ModConfig
{
    /// <summary>
    /// Keybinding used to enable the toolbar swapping
    /// </summary>
    public KeybindList ToggleEnabledKey { get; set; } = KeybindList.Parse("");

    /// <summary>
    /// Is the toolbar swapping enabled
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Toolbar slot for watering can (1-12)
    /// </summary>
    public int WateringCanSlot { get; set; } = 1;

    /// <summary>
    /// Toolbar slot for hoe (1-12)
    /// </summary>
    public int HoeToolbarSlot { get; set; } = 1;

    /// <summary>
    /// Toolbar slot for axe (1-12)
    /// </summary>
    public int AxeToolbarSlot { get; set; } = 1;

    /// <summary>
    /// Toolbar slot for pickaxe (1-12)
    /// </summary>
    public int PickAxeSlot { get; set; } = 1;

    /// <summary>
    /// Toolbar slot for scythe (1-12)
    /// </summary>
    public int ScytheSlot { get; set; } = 1;

    /// <summary>
    /// Toolbar slot for fishing rod (1-12)
    /// </summary>
    public int FishingRod { get; set; } = 1;

    /// <summary>
    /// Toolbar slot for return scepter (1-12)
    /// </summary>
    public int ReturnScepterSlot { get; set; } = 1;
}
