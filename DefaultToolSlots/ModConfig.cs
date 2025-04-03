﻿using StardewModdingAPI.Utilities;

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
}
