using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSCLoader;

namespace MSCSC
{
    public class RestartTCPClient : ConsoleCommand
    {
        public override string Name => "tcprestart";
        public override string Help => "MSCSC: restarts the tcp socket.";
        public override void Run(string[] args)
        {
            SocketConnect.client.Close();
            SocketConnect.listener.Stop();
            SocketConnect.mThread.Abort();
            SocketConnect.StartThread();
            //ModConsole.Print("<color=yellow>[MSCSC]: </color><color=white>TCP socked restarted.</color>");
        }
    }
}
