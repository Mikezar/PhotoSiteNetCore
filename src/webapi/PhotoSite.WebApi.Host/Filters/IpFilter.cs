using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PhotoSite.ApiService.Services.Interfaces;

namespace PhotoSite.WebApi.Filters
{
    /// <summary>
    /// IP-filter
    /// </summary>
    public class IpFilter
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="next"></param>
        public IpFilter(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress;

            if (ipAddress != null)
            {
                var blackListService = context.RequestServices.GetService<IBlackIpService>();
                if (blackListService is not null)
                {
                    var isInBlackIpList = false;
                    if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        // Convert the IpAddress to an unsigned integer.
                        var ipAddressBits = BitConverter.ToUInt32(ipAddress.GetAddressBytes().Reverse().ToArray(), 0);
                        var blackIpListV4 = await blackListService.GetV4();
                        foreach (var blackIp in blackIpListV4)
                        {
                            // https://stackoverflow.com/a/1499284/3085985
                            // Bitwise AND mask and MaskAddress, this should be the same as mask and IpAddress
                            // as the end of the mask is 0000 which leads to both addresses to end with 0000
                            // and to start with the prefix.
                            isInBlackIpList = blackIp.MaskAddress == (ipAddressBits & blackIp.Mask);
                            if (isInBlackIpList)
                                break;
                        }
                    }
                    else if (ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        // Convert the IpAddress to a BitArray.
                        var ipAddressBits = new BitArray(ipAddress.GetAddressBytes());
                        var blackIpListV6 = await blackListService.GetV6();
                        foreach (var blackIp in blackIpListV6)
                        {
                            isInBlackIpList =
                                CheckInterNetworkV6(ipAddressBits, blackIp.MaskAddressBits!, blackIp.SubnetMask);
                            if (isInBlackIpList)
                                break;
                        }
                    }

                    if (isInBlackIpList)
                    {
                        context.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                        return;
                    }
                }
                else
                {
                    // TODO: Log!
                }
            }

            await _next.Invoke(context);
        }

        private bool CheckInterNetworkV6(BitArray ipAddressBits, BitArray maskAddressBits, int subnetMask)
        {
            // Convert the mask address to a BitArray.
            //var maskAddressBits = new BitArray(maskAddress.GetAddressBytes());

            // And convert the IpAddress to a BitArray.
            //var ipAddressBits = new BitArray(ipAddress.GetAddressBytes());

            if (maskAddressBits.Length != ipAddressBits.Length)
            {
                // TODO: Log -> throw new ArgumentException("Length of IP Address and Subnet Mask do not match.");
                return true;
            }

            // Compare the prefix bits.
            for (int maskIndex = 0; maskIndex < subnetMask; maskIndex++)
                if (ipAddressBits[maskIndex] != maskAddressBits[maskIndex])
                    return false;

            return true;
        }
    }
}