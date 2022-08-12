using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PhotoSite.ApiService.Caches.Interfaces;
using PhotoSite.ApiService.Data;
using PhotoSite.ApiService.Data.Common;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Core.ExtException;
using PhotoSite.Data.Entities;
using PhotoSite.Data.Repositories.Interfaces;

namespace PhotoSite.ApiService.Services.Implementations
{
    public class BlackIpService : IBlackIpService
    {
        private readonly IBlackIpRepository _blackIpRepository;
        private readonly IBlackIpCache _blackIpCache;

        /// <summary>
        /// ctor
        /// </summary>
        public BlackIpService(IBlackIpRepository blackIpRepository, IBlackIpCache blackIpCache)
        {
            _blackIpRepository = blackIpRepository;
            _blackIpCache = blackIpCache;
        }

        /// <inheritdoc cref="IBlackIpService.GetV4"/>
        public async Task<IEnumerable<BlackInterNetworkV4>> GetV4()
        {
            return await _blackIpCache.GetV4();
        }

        internal static BlackInterNetworkV4 CreateBlackInterNetworkV4(BlackIp blackIp)
        {
            var maskAddressBits =
                BitConverter.ToUInt32(IPAddress.Parse(blackIp.MaskAddress!).GetAddressBytes().Reverse().ToArray(), 0);
            var mask = uint.MaxValue << (32 - blackIp.SubnetMask);
            return new BlackInterNetworkV4
            {
                MaskAddress = maskAddressBits & mask,
                Mask = mask
            };
        }

        internal static BlackInterNetworkV6 CreateBlackInterNetworkV6(BlackIp blackIp)
        {
            return new BlackInterNetworkV6()
            {
                MaskAddressBits = new BitArray(IPAddress.Parse(blackIp.MaskAddress!).GetAddressBytes()),
                SubnetMask = blackIp.SubnetMask
            };
        }

        /// <inheritdoc cref="IBlackIpService.GetV6"/>
        public async Task<IEnumerable<BlackInterNetworkV6>> GetV6()
        {
            return await _blackIpCache.GetV6();
        }

        /// <inheritdoc cref="IBlackIpService.GetAll"/>
        public async Task<IEnumerable<BlackIp>> GetAll()
        {
            return await _blackIpRepository.GetAll();
        }

        /// <inheritdoc cref="IBlackIpService.Create"/>
        public async Task<IIdResult> Create(BlackIp blackIp)
        {
            Validate(blackIp);

            var exist = await _blackIpRepository.Exists(blackIp.MaskAddress!);
            if (exist)
                throw new UserException($"Mask-address '{blackIp.MaskAddress}' exists in blacklist");

            await _blackIpRepository.Create(blackIp);

            _blackIpCache.Remove();

            return new IdResult(blackIp.Id);
        }

        /// <inheritdoc cref="IBlackIpService.Delete"/>
        public async Task Delete(int id)
        {
            var value = await _blackIpRepository.Get(id);
            if (value is null)
                throw new UserException($"Not found blacklist id={id}");

            await _blackIpRepository.Delete(value);

            _blackIpCache.Remove();
        }

        /// <inheritdoc cref="IBlackIpService.Update"/>
        public async Task Update(BlackIp blackIp)
        {
            Validate(blackIp);

            var exist = await _blackIpRepository.GetAsNoTracking(blackIp.Id);
            if (exist is null)
                throw new UserException($"Not found blacklist id={blackIp.Id}");

            var ext = await _blackIpRepository.ExistsOtherBlackIpByMaskAddress(blackIp.Id, blackIp.MaskAddress!);
            if (ext)
                throw new UserException($"Mask-address '{blackIp.MaskAddress}' exists in other blacklist");

            await _blackIpRepository.Update(blackIp);

            _blackIpCache.Remove();
        }

        private void Validate(BlackIp blackIp)
        {
            if (blackIp.MaskAddress is null || blackIp.MaskAddress.Length == 0)
                throw new UserException("MaskAddress is empty!");
        }
    }
}