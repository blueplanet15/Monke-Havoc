using MonkeHavoc.Classes;
using MonkeHavoc.Modules;

namespace MonkeHavoc.Panel
{
    public class AllButtons
    {
        public static MonkeHavocModule[][] categories = new MonkeHavocModule[][]
        {
            new MonkeHavocModule[]
            {
                new MonkeHavocModule() { textOnButton = "test", foreverOrOnce = () => testmod.forever() },
            },
        };
    }
}