using System.Collections.Generic;

namespace Approacheth.UI
{
    public interface IUIWindow
    {
        void Setup(UIConfig uiConfig, SpaceObject spaceObject);

        UIWindowFactory.SEGMENTS[] GetBuildSegments();

        void Close();
    }

}