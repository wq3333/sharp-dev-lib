namespace SharpDevLib;

/// <summary>
/// 包含KeyValue的数据传输对象
/// </summary>
/// <typeparam name="TKey">Key数据类型</typeparam>
/// <typeparam name="TValue">Value数据类型</typeparam>
public class KeyValueDto<TKey,TValue> : BaseDto
{
    /// <summary>
    /// 实例化 KeyValueDto 对象
    /// </summary>
    public KeyValueDto()
    {
    }

    /// <summary>
    /// 实例化 KeyValueDto 对象
    /// </summary>
    public KeyValueDto(TKey key,TValue value)
    {
        Key = key;
        Value = value;
    }

    /// <summary>
    /// Key
    /// </summary>
    public TKey? Key { get; set; }

    /// <summary>
    /// Value
    /// </summary>
    public TValue? Value { get; set; }
}