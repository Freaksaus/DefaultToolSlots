using GenericModConfigMenu;
using HarmonyLib;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Tools;

namespace DefaultToolSlots;

/// <summary>The mod entry point.</summary>
internal sealed class ModEntry : Mod
{
    private static ModConfig Config { get; set; } = new ModConfig();

    private const string RETURN_SCEPTER_ID = "ReturnScepter";
    private const string HORSE_FLUTE_ID = "911";
    private const string CHERRY_BOMB_ID = "286";
    private const string BOMB_ID = "287";
    private const string MEGA_BOMB_ID = "288";
    private const int MINIMUM_TOOL_SLOT = 1;
    private const int MAXIMUM_TOOL_SLOT = 36;
    private const int TOOLBAR_SLOT_COUNT = 12;

    public override void Entry(IModHelper helper)
    {
        Harmony harmony = new(Helper.ModRegistry.ModID);

        harmony.Patch(
               original: AccessTools.Method(typeof(Farmer), nameof(Farmer.shiftToolbar)),
               postfix: new HarmonyMethod(typeof(ModEntry), nameof(ShiftToolbar_Postfix))
           );

        harmony.Patch(
               original: AccessTools.Method(typeof(ItemGrabMenu), nameof(ItemGrabMenu.organizeItemsInList)),
               postfix: new HarmonyMethod(typeof(ModEntry), nameof(OrganizeItemsInList_Postfix))
           );

        Config = helper.ReadConfig<ModConfig>();

        helper.Events.GameLoop.GameLaunched += OnGameLaunched;
        helper.Events.Player.InventoryChanged += OnPlayerInventoryChanged;
        helper.Events.Input.ButtonPressed += OnButtonPressed;
    }

    private static void ShiftToolbar_Postfix(ref bool right)
    {
        SetCurrentToolbarIndex(right);
        SortTools(Game1.player);
    }

    private static void SetCurrentToolbarIndex(bool right)
    {
        if (right)
        {
            Config.CurrentToolbarIndex--;
        }
        else
        {
            Config.CurrentToolbarIndex++;
        }

        var maxToolbarIndex = (Game1.player.Items.Count / TOOLBAR_SLOT_COUNT) - 1;
        if (Config.CurrentToolbarIndex < 0)
        {
            Config.CurrentToolbarIndex = maxToolbarIndex;
        }
        else if (Config.CurrentToolbarIndex > maxToolbarIndex)
        {
            Config.CurrentToolbarIndex = 0;
        }
    }

#pragma warning disable IDE0060 // Remove unused parameter
    private static void OrganizeItemsInList_Postfix(ref IList<Item> items)
    {
        if (Game1.activeClickableMenu is not GameMenu)
        {
            return;
        }

        SortTools(Game1.player);
    }
#pragma warning restore IDE0060 // Remove unused parameter

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
            reset: () => Config = new ModConfig(),
            save: () => this.Helper.WriteConfig(Config)
        );

        Config = Helper.ReadConfig<ModConfig>();

        configMenu.AddBoolOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("enabled"),
            tooltip: () => Helper.Translation.Get("enabled-tooltip"),
            getValue: () => Config.Enabled,
            setValue: value => Config.Enabled = value
        );

        configMenu.AddKeybindList(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("toggle-enabled-key"),
            tooltip: () => Helper.Translation.Get("toggle-enabled-key-tooltip"),
            getValue: () => Config.ToggleEnabledKey,
            setValue: value => Config.ToggleEnabledKey = value
        );

        configMenu.AddBoolOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("keep-tools-while-swapping-toolbars"),
            tooltip: () => Helper.Translation.Get("keep-tools-while-swapping-toolbars-tooltip"),
            getValue: () => Config.KeepToolsWhileSwappingToolbars,
            setValue: value => Config.KeepToolsWhileSwappingToolbars = value
        );

        configMenu.AddSectionTitle(
            mod: this.ModManifest,
            text: () => Helper.Translation.Get("tools-section-title")
        );

        configMenu.AddBoolOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("wateringcan-enabled"),
            getValue: () => Config.WateringCanEnabled,
            setValue: value => Config.WateringCanEnabled = value
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("wateringcan-slot"),
            tooltip: () => Helper.Translation.Get("wateringcan-slot-tooltip"),
            getValue: () => Config.WateringCanSlot,
            setValue: value => Config.WateringCanSlot = value,
            min: MINIMUM_TOOL_SLOT,
            max: MAXIMUM_TOOL_SLOT
        );

        configMenu.AddBoolOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("hoe-enabled"),
            getValue: () => Config.HoeEnabled,
            setValue: value => Config.HoeEnabled = value
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("hoe-slot"),
            tooltip: () => Helper.Translation.Get("hoe-slot-tooltip"),
            getValue: () => Config.HoeToolbarSlot,
            setValue: value => Config.HoeToolbarSlot = value,
            min: MINIMUM_TOOL_SLOT,
            max: MAXIMUM_TOOL_SLOT
        );

        configMenu.AddBoolOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("axe-enabled"),
            getValue: () => Config.AxeEnabled,
            setValue: value => Config.AxeEnabled = value
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("axe-slot"),
            tooltip: () => Helper.Translation.Get("axe-slot-tooltip"),
            getValue: () => Config.AxeToolbarSlot,
            setValue: value => Config.AxeToolbarSlot = value,
            min: MINIMUM_TOOL_SLOT,
            max: MAXIMUM_TOOL_SLOT
        );

        configMenu.AddBoolOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("pickaxe-enabled"),
            getValue: () => Config.PickaxeEnabled,
            setValue: value => Config.PickaxeEnabled = value
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("pickaxe-slot"),
            tooltip: () => Helper.Translation.Get("pickaxe-slot-tooltip"),
            getValue: () => Config.PickAxeSlot,
            setValue: value => Config.PickAxeSlot = value,
            min: MINIMUM_TOOL_SLOT,
            max: MAXIMUM_TOOL_SLOT
        );

        configMenu.AddBoolOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("scythe-enabled"),
            getValue: () => Config.ScytheEnabled,
            setValue: value => Config.ScytheEnabled = value
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("scythe-slot"),
            tooltip: () => Helper.Translation.Get("scythe-slot-tooltip"),
            getValue: () => Config.ScytheSlot,
            setValue: value => Config.ScytheSlot = value,
            min: MINIMUM_TOOL_SLOT,
            max: MAXIMUM_TOOL_SLOT
        );

        configMenu.AddBoolOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("fishingrod-enabled"),
            getValue: () => Config.FishingRodEnabled,
            setValue: value => Config.FishingRodEnabled = value
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("fishingrod-slot"),
            tooltip: () => Helper.Translation.Get("fishingrod-slot-tooltip"),
            getValue: () => Config.FishingRod,
            setValue: value => Config.FishingRod = value,
            min: MINIMUM_TOOL_SLOT,
            max: MAXIMUM_TOOL_SLOT
        );

        configMenu.AddBoolOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("pan-enabled"),
            getValue: () => Config.PanEnabled,
            setValue: value => Config.PanEnabled = value
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("pan-slot"),
            tooltip: () => Helper.Translation.Get("pan-slot-tooltip"),
            getValue: () => Config.PanSlot,
            setValue: value => Config.PanSlot = value,
            min: MINIMUM_TOOL_SLOT,
            max: MAXIMUM_TOOL_SLOT
        );


        configMenu.AddSectionTitle(
            mod: this.ModManifest,
            text: () => Helper.Translation.Get("weapons-section-title")
        );

        configMenu.AddBoolOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("meleeweapon-enabled"),
            getValue: () => Config.MeleeWeaponEnabled,
            setValue: value => Config.MeleeWeaponEnabled = value
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("meleeweapon-slot"),
            tooltip: () => Helper.Translation.Get("meleeweapon-slot-tooltip"),
            getValue: () => Config.MeleeWeaponSlot,
            setValue: value => Config.MeleeWeaponSlot = value,
            min: MINIMUM_TOOL_SLOT,
            max: MAXIMUM_TOOL_SLOT
        );

        configMenu.AddBoolOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("slingshot-enabled"),
            getValue: () => Config.SlingshotEnabled,
            setValue: value => Config.SlingshotEnabled = value
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("slingshot-slot"),
            tooltip: () => Helper.Translation.Get("slingshot-slot-tooltip"),
            getValue: () => Config.SlingshotSlot,
            setValue: value => Config.SlingshotSlot = value,
            min: MINIMUM_TOOL_SLOT,
            max: MAXIMUM_TOOL_SLOT
        );

        configMenu.AddBoolOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("bombs-enabled"),
            getValue: () => Config.BombsEnabled,
            setValue: value => Config.BombsEnabled = value
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("bomb-slot"),
            tooltip: () => Helper.Translation.Get("bomb-slot-tooltip"),
            getValue: () => Config.BombSlot,
            setValue: value => Config.BombSlot = value,
            min: MINIMUM_TOOL_SLOT,
            max: MAXIMUM_TOOL_SLOT
        );

        configMenu.AddSectionTitle(
            mod: this.ModManifest,
            text: () => Helper.Translation.Get("other-section-title")
        );

        configMenu.AddBoolOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("returnscepter-enabled"),
            getValue: () => Config.ReturnScepterEnabled,
            setValue: value => Config.ReturnScepterEnabled = value
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("returnscepter-slot"),
            tooltip: () => Helper.Translation.Get("returnscepter-slot-tooltip"),
            getValue: () => Config.ReturnScepterSlot,
            setValue: value => Config.ReturnScepterSlot = value,
            min: MINIMUM_TOOL_SLOT,
            max: MAXIMUM_TOOL_SLOT
        );

        configMenu.AddBoolOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("horseflute-enabled"),
            getValue: () => Config.HorseFluteEnabled,
            setValue: value => Config.HorseFluteEnabled = value
        );

        configMenu.AddNumberOption(
            mod: this.ModManifest,
            name: () => Helper.Translation.Get("horseflute-slot"),
            tooltip: () => Helper.Translation.Get("horseflute-slot-tooltip"),
            getValue: () => Config.HorseFluteSlot,
            setValue: value => Config.HorseFluteSlot = value,
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

        if (Config.ToggleEnabledKey.IsDown())
        {
            Config.Enabled = !Config.Enabled;
            Game1.addHUDMessage(new(
                $"Default Tool Slots is now {(Config.Enabled ? "enabled" : "disabled")})",
                HUDMessage.newQuest_type));

            if (Config.Enabled)
            {
                SortTools(Game1.player);
            }
        }
    }

    private void OnPlayerInventoryChanged(object? sender, InventoryChangedEventArgs e)
    {
        SortTools(e.Player);
    }

    private static void SortTools(Farmer farmer)
    {
        if (Config is null || !Config.Enabled)
        {
            return;
        }

        var tools = farmer.Items
            .Where(x => x is not null)
            .Where(x => x is Tool || x.ItemId == HORSE_FLUTE_ID || x.ItemId == CHERRY_BOMB_ID || x.ItemId == BOMB_ID || x.ItemId == MEGA_BOMB_ID);

        foreach (var tool in tools)
        {
            switch (tool)
            {
                case WateringCan:
                    if (Config.WateringCanEnabled)
                    {
                        SetToolToToolbarSlot(tool, Config.WateringCanSlot);
                    }
                    break;
                case Hoe:
                    if (Config.HoeEnabled)
                    {
                        SetToolToToolbarSlot(tool, Config.HoeToolbarSlot);
                    }
                    break;
                case Axe:
                    if (Config.AxeEnabled)
                    {
                        SetToolToToolbarSlot(tool, Config.AxeToolbarSlot);
                    }
                    break;
                case Pickaxe:
                    if (Config.PickaxeEnabled)
                    {
                        SetToolToToolbarSlot(tool, Config.PickAxeSlot);
                    }
                    break;
                case MeleeWeapon:
                    if (tool.ItemId == MeleeWeapon.scytheId ||
                        tool.ItemId == MeleeWeapon.goldenScytheId ||
                        tool.ItemId == MeleeWeapon.iridiumScytheID)
                    {
                        if (Config.ScytheEnabled)
                        {
                            SetToolToToolbarSlot(tool, Config.ScytheSlot);
                        }
                    }
                    else
                    {
                        if (Config.MeleeWeaponEnabled)
                        {
                            SetToolToToolbarSlot(tool, Config.MeleeWeaponSlot);
                        }
                    }
                    break;
                case FishingRod:
                    if (Config.FishingRodEnabled)
                    {
                        SetToolToToolbarSlot(tool, Config.FishingRod);
                    }
                    break;
                case Wand:
                    if (tool.ItemId == RETURN_SCEPTER_ID && Config.ReturnScepterEnabled)
                    {
                        SetToolToToolbarSlot(tool, Config.ReturnScepterSlot);
                    }
                    break;
                case Pan:
                    if (Config.PanEnabled)
                    {
                        SetToolToToolbarSlot(tool, Config.PanSlot);
                    }
                    break;
                case StardewValley.Tools.Slingshot:
                    if (Config.SlingshotEnabled)
                    {
                        SetToolToToolbarSlot(tool, Config.SlingshotSlot);
                    }
                    break;
                case StardewValley.Object:
                    if (tool.ItemId == HORSE_FLUTE_ID && Config.HorseFluteEnabled)
                    {
                        SetToolToToolbarSlot(tool, Config.HorseFluteSlot);
                    }
                    else if ((tool.ItemId == CHERRY_BOMB_ID || tool.ItemId == BOMB_ID || tool.ItemId == MEGA_BOMB_ID) && Config.BombsEnabled)
                    {
                        SetToolToToolbarSlot(tool, Config.BombSlot);
                    }
                    break;
            }
        }
    }

    private static void SetToolToToolbarSlot(Item tool, int? slot)
    {
        if (tool is null || !slot.HasValue)
        {
            return;
        }

        var currentInventoryIndex = Game1.player.getIndexOfInventoryItem(tool);
        var defaultInventoryIndex = slot.Value;
        if (!Config.KeepToolsWhileSwappingToolbars)
        {
            defaultInventoryIndex += Config.CurrentToolbarIndex * TOOLBAR_SLOT_COUNT;
        }

        defaultInventoryIndex -= 1;
        if (currentInventoryIndex == defaultInventoryIndex || defaultInventoryIndex >= Game1.player.Items.Count)
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
