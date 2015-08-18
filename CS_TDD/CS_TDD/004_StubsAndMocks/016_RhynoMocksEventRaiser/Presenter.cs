using System;
using CS_TDD._004_StubsAndMocks._015_RhynoMocksEventRelatedTests;

namespace CS_TDD._004_StubsAndMocks._016_RhynoMocksEventRaiser
{
    class Presenter
    {
        IModel model = null;
        IView window = null;

        public Presenter(IView window, IModel model)
        {
            this.model = model;
            this.window = window;
            this.window.Load += new EventHandler(View_Load);
        }

        void View_Load(object sender, System.EventArgs e)
        {
            model.DoSomeWork();
        }
    }
}
