namespace Approacheth.UI
{
    public interface IUIWindow
    {
        void Setup(UIConfig uiConfig, SpaceObject spaceObject);

        void Close();
    }

}