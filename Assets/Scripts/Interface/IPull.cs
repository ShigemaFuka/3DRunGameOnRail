/// <summary>
/// アイテム自身をプレイヤーの位置に移動する機能
/// 特定のアイテムを取得したときに、このインターフェースを呼び出す
/// </summary>
public interface IPull
{
    /// <summary>
    /// アイテム自身をプレイヤーの位置に移動する機能
    /// </summary>
    public void PullItem(float speed , bool isPull);
}
