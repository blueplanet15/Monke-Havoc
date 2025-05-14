using MonkeHavoc.Classes;
using MonkeHavoc.Modules;
using MonkeHavoc.Modules.Guns;
using MonkeHavoc.Modules.Horror;
using MonkeHavoc.Modules.Movement;
using MonkeHavoc.Modules.Multiplayer;

namespace MonkeHavoc.Panel
{
    public class AllButtons
    {
        public static MonkeHavocModule[][] categories = new MonkeHavocModule[][]
        {
            new MonkeHavocModule[] // Home
            {
                new MonkeHavocModule()
                {
                    textOnButton = "Settings", toggle = false, foreverOrOnce = () => AllTheCoolThings.Settings()
                },
                new MonkeHavocModule()
                {
                    textOnButton = "Movement", toggle = false, foreverOrOnce = () => AllTheCoolThings.Movement()
                },
                new MonkeHavocModule()
                {
                    textOnButton = "Guns", toggle = false, foreverOrOnce = () => AllTheCoolThings.Guns()
                },
                new MonkeHavocModule()
                {
                    textOnButton = "Multiplayer", toggle = false, foreverOrOnce = () => AllTheCoolThings.Multiplayer()
                },
                new MonkeHavocModule()
                {
                    textOnButton = "Visual", toggle = false, foreverOrOnce = () => AllTheCoolThings.Visual()
                },
                new MonkeHavocModule()
                {
                    textOnButton = "Spawners", toggle = false, foreverOrOnce = () => AllTheCoolThings.Spawners()
                },
                new MonkeHavocModule()
                {
                    textOnButton = "Horror", toggle = false, foreverOrOnce = () => AllTheCoolThings.Horror()
                },
            },

            new MonkeHavocModule[] // Settings
            {
            },

            new MonkeHavocModule[] // Movement
            {
                new MonkeHavocModule()
                {
                    textOnButton = "Platforms", foreverOrOnce = () => Platforms.PlatformsModuleUpdate(),
                    enable = () => Platforms.CreatePlats(), disable = () => Platforms.DIEDIEDIE()
                },
                new MonkeHavocModule()
                {
                    textOnButton = "Speedboost", foreverOrOnce = () => Speedboost.LetsDoThisFAST()
                },
                new MonkeHavocModule()
                {
                    textOnButton = "No Slip", foreverOrOnce = () => NoSlip.OnEnable(),
                    disable = () => NoSlip.OnDisable()
                },
                new MonkeHavocModule()
                {
                    textOnButton = "Slippy Hands", foreverOrOnce = () => SlippyHands.OnEnable(),
                    disable = () => SlippyHands.OnDisable()
                },
                new MonkeHavocModule()
                {
                    textOnButton = "Fly", foreverOrOnce = () => Fly.UpdateTheMod()
                },
                new MonkeHavocModule()
                {
                    textOnButton = "Long Arms", enable = () => LongArms.FOREVERTOGETHER(),
                    disable = () => LongArms.HELEFTNOOOOOOOOO()
                },
                new MonkeHavocModule()
                {
                    textOnButton = "Checkpoints", foreverOrOnce = () => Checkpoints.OnActivateOrOnForever(),
                    enable = () => Checkpoints.OnEnable(), disable = () => Checkpoints.OnDisable()
                },
                new MonkeHavocModule()
                {
                    textOnButton = "Frozone", foreverOrOnce = () => Frozone.UpdateDaMod(),
                    disable = () => Frozone.DisableDaMod()
                },
                new MonkeHavocModule()
                {
                    textOnButton = "Airplane", foreverOrOnce = () => Airplane.UpdateTheMod(),
                    enable = () => Airplane.EnableBadable(), disable = () => Airplane.DisableBadable()
                },
            },

            new MonkeHavocModule[] // Guns
            {
                new MonkeHavocModule()
                {
                    textOnButton = "Teleport Gun", foreverOrOnce = () => TPGun.ForeverIWasDoingGUNS(),
                    enable = () => TPGun.IHAVENABLEDMYFULLPOWER(), disable = () => TPGun.DisabledAhhhhh()
                },
                new MonkeHavocModule()
                {
                    textOnButton = "Hoverboard Gun", foreverOrOnce = () => HoverboardGun.ForeverTogether(),
                    enable = () => HoverboardGun.EnableWOOOOOO(), disable = () => HoverboardGun.DisableMeSad()
                },
            },

            new MonkeHavocModule[] // Multiplayer
            {
                new MonkeHavocModule()
                {
                    textOnButton = "Bone ESP", foreverOrOnce = () => BoneESP.OnActivateOrOnForever(),
                    disable = () => BoneESP.OnDisable()
                },
            },

            new MonkeHavocModule[] // Visual
            {
            },

            new MonkeHavocModule[] // Spawners
            {
            },

            new MonkeHavocModule[] // Horror
            {
                new MonkeHavocModule()
                    { textOnButton = "Scary Back", toggle = false, foreverOrOnce = () => ScaryBack.DoTheThing() },
                new MonkeHavocModule()
                {
                    textOnButton = "RT Lighting", toggle = false,
                    foreverOrOnce = () => HorrorLightingHandler.HorrorLighting()
                },
                new MonkeHavocModule()
                {
                    textOnButton = "No RT Lighting", toggle = false,
                    foreverOrOnce = () => HorrorLightingHandler.NormalLighting()
                }
            },
        };
    }
}