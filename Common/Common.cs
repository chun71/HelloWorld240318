namespace HelloWorld240318.Common
{
    public class Common
    {
        public static Dictionary<string, string> nameList { get; } = new Dictionary<string, string>()
        {
            {"Kevin", "凱文"},
            {"Ted", "泰迪"}
        };

        public static Dictionary<int, string> sexList { get; } = new Dictionary<int, string>()
        {
            {0, "女"},
            {1, "男"}
        };

        public static Dictionary<int, string> commutingList { get; } = new Dictionary<int, string>()
        {
            {0, "走路或大眾交通"},
            {1, "機車"},
            {2, "汽車"}
        };
    }
}
