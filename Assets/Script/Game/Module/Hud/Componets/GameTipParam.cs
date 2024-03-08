using Unity.Entities;
namespace SGame
{
    public class GameTipParam : IComponentData
    {
        public TipType  type;
        public string   text;
    }
}