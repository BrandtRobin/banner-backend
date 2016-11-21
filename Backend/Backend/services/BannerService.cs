using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Backend.ExtensionMethods;
using Backend.Interfaces;
using Backend.Models;
using HtmlAgilityPack;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Backend.services
{
    public class BannerService : IBannerService
    {
        private static IMongoCollection<Banner> _bannerCollection;

        public BannerService()
        {
            IMongoClient client = new MongoClient();
            var db = client.GetDatabase("test");
            _bannerCollection = db.GetCollection<Banner>("banners");
        }

        public async Task<List<Banner>> GetBanners()
        {
            var collection = await _bannerCollection.AsQueryable().ToListAsync();
            return collection;
        }

        public async Task<Banner> GetBannerById(string id)
        {
            var banner = await _bannerCollection.AsQueryable().FirstOrDefaultAsync(s => s.Id == id);
            return banner;
        }

        public async Task<Banner> CreateBanner(Banner banner)
        {
            if (banner.Html.IsValidHtml())
            {
                await _bannerCollection.InsertOneAsync(banner);
                return banner;
            }
            return null;
        }

        public async Task<Banner> UpdateBanner(Banner banner)
        {
            if (!banner.Html.IsValidHtml()) return null;

            var filter = Builders<Banner>.Filter.Eq(s => s.Id, banner.Id);
            var update = Builders<Banner>.Update
                .Set("Html", banner.Html)
                .CurrentDate("Modified");

            var options = new FindOneAndUpdateOptions<Banner>
            {
                ReturnDocument = ReturnDocument.After
            };

            var result = await _bannerCollection.FindOneAndUpdateAsync(filter, update, options);
            return result;
        }

        public async Task<long> DeleteBanner(string id)
        {
            var filter = Builders<Banner>.Filter.Eq(s => s.Id, id);
            var result = await _bannerCollection.DeleteOneAsync(filter);
            return result.DeletedCount;
        }
    }
}