using System;

namespace SGame.UI
{
    public interface UIEventHandler
    {
        Action onShown { get; set; }
        Action onHide { get; set; }
        Action onCreate { get; set; }
        Action onDestory { get; set; }
        Action onClose { get; set; }
    }
}