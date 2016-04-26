using System;

namespace CS_TDD._004_StubsAndMocks._019_RhynoMocks._015_RhynoMocksEventRelatedTests
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
            throw new NotImplementedException();
        }
    }
}
