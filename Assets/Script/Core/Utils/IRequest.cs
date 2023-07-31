
public interface IRequest
{
    // 是否存在错误
    string error { get; }
    
    // 请求是否结束
    bool isDone { get; }

    // 释放请求 (非必要）
    void Close();
}
