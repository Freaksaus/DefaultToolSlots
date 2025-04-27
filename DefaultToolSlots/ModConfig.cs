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
    /// Keep tools in the same spot even when swapping toolbars
    /// </summary>
    public bool KeepToolsWhileSwappingToolbars { get; set; } = false;

    /// <summary>
    /// Is swapping enabled for the watering can
    /// </summary>
    public bool WateringCanEnabled { get; set; } = true;

    /// <summary>
    /// Toolbar slot for watering can
    /// </summary>
    public int WateringCanSlot { get; set; } = 1;

    /// <summary>
    /// Is swapping enabled for the hoe
    /// </summary>
    public bool HoeEnabled { get; set; } = true;

    /// <summary>
    /// Toolbar slot for hoe
    /// </summary>
    public int HoeToolbarSlot { get; set; } = 1;

    /// <summary>
    /// Is swapping enabled for the axe
    /// </summary>
    public bool AxeEnabled { get; set; } = true;

    /// <summary>
    /// Toolbar slot for axe
    /// </summary>
    public int AxeToolbarSlot { get; set; } = 1;

    /// <summary>
    /// Is swapping enabled for the pickaxe
    /// </summary>
    public bool PickaxeEnabled { get; set; } = true;

    /// <summary>
    /// Toolbar slot for pickaxe
    /// </summary>
    public int PickAxeSlot { get; set; } = 1;

    /// <summary>
    /// Is swapping enabled for the scythe
    /// </summary>
    public bool ScytheEnabled { get; set; } = true;

    /// <summary>
    /// Toolbar slot for scythe
    /// </summary>
    public int ScytheSlot { get; set; } = 1;

    /// <summary>
    /// Is swapping enabled for the fishing rod
    /// </summary>
    public bool FishingRodEnabled { get; set; } = true;

    /// <summary>
    /// Toolbar slot for fishing rod
    /// </summary>
    public int FishingRod { get; set; } = 1;

    /// <summary>
    /// Is swapping enabled for the return scepter
    /// </summary>
    public bool ReturnScepterEnabled { get; set; } = true;

    /// <summary>
    /// Toolbar slot for return scepter
    /// </summary>
    public int ReturnScepterSlot { get; set; } = 1;

    /// <summary>
    /// Is swapping enabled for the pan
    /// </summary>
    public bool PanEnabled { get; set; } = true;

    /// <summary>
    /// Toolbar slot for pan
    /// </summary>
    public int PanSlot { get; set; } = 1;

    /// <summary>
    /// Is swapping enabled for melee weapons
    /// </summary>
    public bool MeleeWeaponEnabled { get; set; } = true;

    /// <summary>
    /// Toolbar slot for melee weapon
    /// </summary>
    public int MeleeWeaponSlot { get; set; } = 1;

    /// <summary>
    /// Is swapping enabled for slingshot
    /// </summary>
    public bool SlingshotEnabled { get; set; } = true;

    /// <summary>
    /// Toolbar slot for slingshot
    /// </summary>
    public int SlingshotSlot { get; set; } = 1;

    /// <summary>
    /// Is swapping enabled for horse flute
    /// </summary>
    public bool HorseFluteEnabled { get; set; } = true;

    /// <summary>
    /// Toolbar slot for horse flute
    /// </summary>
    public int HorseFluteSlot { get; set; } = 1;

    /// <summary>
    /// Is swapping enabled for bombs
    /// </summary>
    public bool BombsEnabled { get; set; } = false;

    /// <summary>
    /// Toolbar slot for bombs
    /// </summary>
    public int BombSlot { get; set; } = 1;

    /// <summary>
    /// Index of the currently selected toolbar
    /// </summary>
    public int CurrentToolbarIndex { get; set; } = 0;
}
