﻿using GenericModConfigMenu;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Tools;

namespace DefaultToolSlots;

/// <summary>The mod entry point.</summary>
internal sealed class ModEntry : Mod
{
    private ModConfig Config { get; set; } = new ModConfig();

    private const string RETURN_SCEPTER_ID = "ReturnScepter";
    private const int MINIMUM_TOOL_SLOT = 1;
    private const int MAXIMUM_TOOL_SLOT = 36;

    public override void Entry(IModHelper helper)
    {
        Config = helper.ReadConfig<ModConfig>();

        helper.Events.GameLoop.GameLaunched += OnGameLaunched;
        helper.Events.Player.InventoryChanged += OnPlayerInventoryChanged;
        helper.Events.Input.ButtonPressed += OnButtonPressed;
    }

    private void OnGameLaunched(object? sender, GameLaunchedEventArgs e)
    {
        // get Generic Mod Config Menu's API (if it's installed)
        var configMenu = this.Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
        if (configMenu is null)
        {
            return;
        }

        // register mod
        configMenu.Register(
            mod: this.ModManifest,
            reset: () => this.Config = new ModConfig(),
            save: () => this.Helper.WriteConfig(this.Config)
        );

        this.Config = Helper.ReadConfig<ModConfig>();

        // add some config options
        configMenu.AddBoolOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("enabled"),
            tooltip: () => Helper.Translation.Get("enabled-tooltip"),
            getValue: () => Config.Enabled,
            setValue: value => this.Config.Enabled = value
        );

        configMenu.AddKeybindList(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("toggle-enabled-key"),
            tooltip: () => Helper.Translation.Get("toggle-enabled-key-tooltip"),
            getValue: () => this.Config.ToggleEnabledKey,
            setValue: value => this.Config.ToggleEnabledKey = value
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("wateringcan-slot"),
            tooltip: () => Helper.Translation.Get("wateringcan-slot-tooltip"),
            getValue: () => this.Config.WateringCanSlot,
            setValue: value => this.Config.WateringCanSlot = value,
            min: MINIMUM_TOOL_SLOT,
            max: MAXIMUM_TOOL_SLOT
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("hoe-slot"),
            tooltip: () => Helper.Translation.Get("hoe-slot-tooltip"),
            getValue: () => this.Config.HoeToolbarSlot,
            setValue: value => this.Config.HoeToolbarSlot = value,
            min: MINIMUM_TOOL_SLOT,
            max: MAXIMUM_TOOL_SLOT
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("axe-slot"),
            tooltip: () => Helper.Translation.Get("axe-slot-tooltip"),
            getValue: () => this.Config.AxeToolbarSlot,
            setValue: value => this.Config.AxeToolbarSlot = value,
            min: MINIMUM_TOOL_SLOT,
            max: MAXIMUM_TOOL_SLOT
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("pickaxe-slot"),
            tooltip: () => Helper.Translation.Get("pickaxe-slot-tooltip"),
            getValue: () => this.Config.PickAxeSlot,
            setValue: value => this.Config.PickAxeSlot = value,
            min: MINIMUM_TOOL_SLOT,
            max: MAXIMUM_TOOL_SLOT
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("scythe-slot"),
            tooltip: () => Helper.Translation.Get("scythe-slot-tooltip"),
            getValue: () => this.Config.ScytheSlot,
            setValue: value => this.Config.ScytheSlot = value,
            min: MINIMUM_TOOL_SLOT,
            max: MAXIMUM_TOOL_SLOT
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("fishingrod-slot"),
            tooltip: () => Helper.Translation.Get("fishingrod-slot-tooltip"),
            getValue: () => this.Config.FishingRod,
            setValue: value => this.Config.FishingRod = value,
            min: MINIMUM_TOOL_SLOT,
            max: MAXIMUM_TOOL_SLOT
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("returnscepter-slot"),
            tooltip: () => Helper.Translation.Get("returnscepter-slot-tooltip"),
            getValue: () => this.Config.ReturnScepterSlot,
            setValue: value => this.Config.ReturnScepterSlot = value,
            min: MINIMUM_TOOL_SLOT,
            max: MAXIMUM_TOOL_SLOT
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("pan-slot"),
            tooltip: () => Helper.Translation.Get("pan-slot-tooltip"),
            getValue: () => this.Config.PanSlot,
            setValue: value => this.Config.PanSlot = value,
            min: MINIMUM_TOOL_SLOT,
            max: MAXIMUM_TOOL_SLOT
        );
    }

    private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
    {
        if (!Context.IsWorldReady)
        {
            return;
        }

        if (!Context.IsPlayerFree)
        {
            return;
        }

        if (Game1.player.isFakeEventActor)
        {
            return;
        }

        if (this.Config.ToggleEnabledKey.IsDown())
        {
            Config.Enabled = !Config.Enabled;
            Game1.addHUDMessage(new(
                $"Default Tool Slots is now {(Config.Enabled ? "enabled" : "disabled")})",
                HUDMessage.newQuest_type));
        }
    }

    private void OnPlayerInventoryChanged(object? sender, InventoryChangedEventArgs e)
    {
        if (Config is null || !Config.Enabled)
        {
            return;
        }

        var tools = e.Player.Items.Where(x => x is Tool);
        foreach (var tool in tools)
        {
            switch (tool)
            {
                case WateringCan:
                    SetToolToToolbarSlot(tool, Config.WateringCanSlot);
                    break;
                case Hoe:
                    SetToolToToolbarSlot(tool, Config.HoeToolbarSlot);
                    break;
                case Axe:
                    SetToolToToolbarSlot(tool, Config.AxeToolbarSlot);
                    break;
                case Pickaxe:
                    SetToolToToolbarSlot(tool, Config.PickAxeSlot);
                    break;
                case MeleeWeapon:
                    if (tool.ItemId == MeleeWeapon.scytheId ||
                        tool.ItemId == MeleeWeapon.goldenScytheId ||
                        tool.ItemId == MeleeWeapon.iridiumScytheID)
                    {
                        SetToolToToolbarSlot(tool, Config.ScytheSlot);
                    }
                    break;
                case FishingRod:
                    SetToolToToolbarSlot(tool, Config.FishingRod);
                    break;
                case Wand:
                    if (tool.ItemId == RETURN_SCEPTER_ID)
                    {
                        SetToolToToolbarSlot(tool, Config.ReturnScepterSlot);
                    }
                    break;
                case Pan:
                    SetToolToToolbarSlot(tool, Config.PanSlot);
                    break;
            }
        }
    }

    private static void SetToolToToolbarSlot(Item tool, int? slot)
    {
        if (tool is null || slot is null)
        {
            return;
        }

        var currentInventoryIndex = Game1.player.getIndexOfInventoryItem(tool);
        var defaultInventoryIndex = slot.Value - 1;
        if (currentInventoryIndex == defaultInventoryIndex || defaultInventoryIndex > Game1.player.Items.Count)
        {
            return;
        }

        var itemInDefautlSlot = Game1.player.Items[defaultInventoryIndex];

        Game1.player.Items[defaultInventoryIndex] = tool;
        if (itemInDefautlSlot is not null)
        {
            Game1.player.Items[currentInventoryIndex] = itemInDefautlSlot;
        }
        else
        {
            Game1.player.Items[currentInventoryIndex] = null;
        }
    }
}
