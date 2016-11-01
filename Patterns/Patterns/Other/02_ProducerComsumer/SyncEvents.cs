using System.Threading;

namespace Patterns.Other._02_ProducerComsumer
{
    public class SyncEvents
    {
        private EventWaitHandle _exitThreadEvent;
        public EventWaitHandle ExitThreadEvent
        {
            get { return _exitThreadEvent; }
        }

        private EventWaitHandle _newItemEvent;
        public EventWaitHandle NewItemEvent
        {
            get { return _newItemEvent; }
        }

        private WaitHandle[] _eventArray;
        public WaitHandle[] EventArray
        {
            get { return _eventArray; }
        }

        public SyncEvents()
        {
            _newItemEvent = new AutoResetEvent(false);
            _exitThreadEvent = new ManualResetEvent(false);
            _eventArray = new WaitHandle[] { _newItemEvent, _exitThreadEvent};
        }
    }
}