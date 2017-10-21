using KTypes.Types.Internal;
using Lib_K_Relay;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTypes.Types
{
    public class TradeListener
    {
        private Proxy _proxy;
        
        

        /// <summary>
        /// Creates a new TradeListener instance.
        /// </summary>
        /// <param name="proxy">The proxy to attach the trade listener to.</param>
        /// <exception cref="ArgumentNullException">Thrown if proxy is null</exception>
        public TradeListener(Proxy proxy)
        {
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }

        public Promise On(string @event)
        {
            Promise promise = new Promise();
            switch (@event.ToLower())
            {
                case "request":
                    _proxy.HookPacket(PacketType.TRADEREQUESTED, (client, packet) =>
                    {
                        TradeRequestedPacket p = packet as TradeRequestedPacket;
                        promise.InvokeThen(new object[1] { p });
                    });
                    break;
                case "newtrade":
                    _proxy.HookPacket(PacketType.TRADESTART, (client, packet) =>
                    {
                        TradeStartPacket p = packet as TradeStartPacket;
                        // create Trade model from packet data then resolve promise with data.
                        promise.InvokeThen(new object[1] { p });
                    });
                    break;
            }
            return promise;
        }
    }
}
