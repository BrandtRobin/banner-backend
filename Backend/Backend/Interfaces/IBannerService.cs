using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Interfaces
{
    interface IBannerService
    {
        Task<List<Banner>> GetBanners();
        Task<Banner> GetBannerById(string id);
        Task<Banner> CreateBanner(Banner banner);
        Task<Banner> UpdateBanner(string id, Banner banner);
        Task<long> DeleteBanner(string id);
    }
}
