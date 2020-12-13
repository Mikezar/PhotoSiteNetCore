using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoSite.ApiService.Base;
using PhotoSite.ApiService.Data;
using PhotoSite.ApiService.Data.Common;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;

namespace PhotoSite.ApiService.Services.Implementations
{
    public class BlackIpService : DbServiceBase, IBlackIpService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public BlackIpService(MainDbContext dbContext) : base(dbContext)
        {
        }

        /// <inheritdoc cref="IBlackIpService.GetV4"/>
        public async Task<IEnumerable<BlackInterNetworkV4>> GetV4()
        {
            // TODO: Add to Cache
            var all = await DbContext.BlackIps!.AsNoTracking().ToArrayAsync();
            return all.Where(t => !t.IsInterNetworkV6).Select(CreateBlackInterNetworkV4);
        }

        private BlackInterNetworkV4 CreateBlackInterNetworkV4(BlackIp blackIp)
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

        /// <inheritdoc cref="IBlackIpService.GetV6"/>
        public async Task<IEnumerable<BlackInterNetworkV6>> GetV6()
        {
            // TODO: Add to Cache
            var all = await DbContext.BlackIps!.AsNoTracking().ToArrayAsync();
            return all.Where(t => t.IsInterNetworkV6).Select(t => new BlackInterNetworkV6()
            {
                MaskAddressBits = new BitArray(IPAddress.Parse(t.MaskAddress!).GetAddressBytes()),
                SubnetMask = t.SubnetMask
            });
        }

        /// <inheritdoc cref="IBlackIpService.GetAll"/>
        public async Task<IEnumerable<BlackIp>> GetAll()
        {
            return await DbContext.BlackIps!.AsNoTracking().ToArrayAsync();
        }

        /// <inheritdoc cref="IBlackIpService.Create"/>
        public async Task<IIdResult> Create(BlackIp blackIp)
        {
            var exist = await DbContext.BlackIps!.AsNoTracking().AnyAsync(t => t.MaskAddress == blackIp.MaskAddress);
            if (exist)
                return (IIdResult)Result.GetError($"Mask-address '{blackIp.MaskAddress}' exists in blacklist");

            var maxId = 0;
            if (await DbContext.BlackIps!.CountAsync() > 0)
                maxId = await DbContext.BlackIps!.AsNoTracking().MaxAsync(t => t.Id);
            maxId += 1;

            blackIp.Id = maxId;
            await DbContext.AddAsync(blackIp);
            await DbContext.SaveChangesAsync();

            return IdResult.GetOk(maxId);
        }

        /// <inheritdoc cref="IBlackIpService.Delete"/>
        public async Task<IResult> Delete(int id)
        {
            var value = await DbContext.BlackIps!.FirstOrDefaultAsync(t => t.Id == id);
            if (value == null)
                return Result.GetError($"Not found blacklist id={id}");

            DbContext.Remove(value);
            await DbContext.SaveChangesAsync();

            return Result.GetOk();
        }

        /// <inheritdoc cref="IBlackIpService.Update"/>
        public async Task<IResult> Update(BlackIp blackIp)
        {
            var exist = await DbContext.BlackIps!.AsNoTracking().AnyAsync(t => t.Id == blackIp.Id);
            if (!exist)
                return Result.GetError($"Not found blacklist id={blackIp.Id}");

            var ext = await DbContext.BlackIps!.AsNoTracking().AnyAsync(t => t.MaskAddress == blackIp.MaskAddress && t.Id != blackIp.Id);
            if (ext)
                return Result.GetError($"Mask-address '{blackIp.MaskAddress}' exists in other blacklist");

            DbContext.Attach(blackIp);
            DbContext.Update(blackIp);
            await DbContext.SaveChangesAsync();

            return Result.GetOk();
        }
    }
}