using MiNET.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HuHelper
{
    public enum TimerError
    {
        MMSYSERR_NOERROR = 0,
        MMSYSERR_ERROR = 1,
        MMSYSERR_INVALPARAM = 11,
        MMSYSERR_NOCANDO = 97,
    }

    public enum RepeateType
    {
        TIME_ONESHOT = 0x0000,
        TIME_PERIODIC = 0x0001
    }

    public enum CallbackType
    {
        TIME_CALLBACK_FUNCTION = 0x0000,
        TIME_CALLBACK_EVENT_SET = 0x0010,
        TIME_CALLBACK_EVENT_PULSE = 0x0020,
        TIME_KILL_SYNCHRONOUS = 0x0100
    }

    public class HighPrecisionTimer
    {
        private delegate void TimerCallback(int id, int msg, int user, int param1, int param2);

        [DllImport("winmm.dll", EntryPoint = "timeGetDevCaps")]
        private static extern TimerError TimeGetDevCaps(ref TimerCaps ptc, int cbtc);

        [DllImport("winmm.dll", EntryPoint = "timeSetEvent")]
        private static extern int TimeSetEvent(int delay, int resolution, TimerCallback callback, int user, int eventType);

        [DllImport("winmm.dll", EntryPoint = "timeKillEvent")]
        private static extern TimerError TimeKillEvent(int id);

        private static TimerCaps _caps;
        private int _interval = 1;
        private int _resolution = 1;
        private TimerCallback _callback;
        private int _id;

        static HighPrecisionTimer()
        {
            TimeGetDevCaps(ref _caps, Marshal.SizeOf(_caps));
        }

        public HighPrecisionTimer()
        {
            Running = false;
            _interval = _caps.periodMin;
            _resolution = _caps.periodMin;
            _callback = new TimerCallback(TimerEventCallback);
        }

        ~HighPrecisionTimer()
        {
            TimeKillEvent(_id);
        }

        public int Interval
        {
            get { return _interval; }
            set
            {
                if (value < _caps.periodMin || value > _caps.periodMax)
                    throw new Exception("invalid Interval");
                _interval = value;
            }
        }

        public bool Running { get; private set; }

        public event Action Ticked;

        public void Start()
        {
            if (!Running)
            {
                _id = TimeSetEvent(_interval, _resolution, _callback, 0,
                  (int)RepeateType.TIME_PERIODIC | (int)CallbackType.TIME_KILL_SYNCHRONOUS);
                if (_id == 0) throw new Exception("failed to start Timer");
                Running = true;
            }
        }

        public void Stop()
        {
            if (Running)
            {
                TimeKillEvent(_id);
                Running = false;
            }
        }

        private void TimerEventCallback(int id, int msg, int user, int param1, int param2)
        {
            Ticked?.Invoke();
        }
    }
}
