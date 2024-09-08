namespace QuestTeleport;

internal sealed class Customization
{
    /// <summary>
    /// 传送道具
    /// </summary>
    public const string TELEPORT_ITEM_NAME = "meleeToolRepairT0StoneAxe";
    /// <summary>
    /// 传送道具所需数量
    /// </summary>
    public const int TELEPORT_ITEM_COUNT = 5;
    /// <summary>
    /// 传送等待时间
    /// </summary>
    public const float TELEPORT_DELAY = 10f;
    /// <summary>
    /// 是否开启任务传送全球广播
    /// </summary>
    public const bool ENABLE_TELE_BROADCAST = true;
    
    public static void FromLocalCfg()
    {
        
    }
}