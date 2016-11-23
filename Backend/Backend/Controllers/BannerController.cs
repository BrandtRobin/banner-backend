using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI.HtmlControls;
using Backend.Models;
using Backend.services;

namespace Backend.Controllers
{
    public class BannerController : ApiController
    {
        private readonly BannerService _bannerService;

        public BannerController()
        {
            _bannerService = new BannerService();
        }

        // GET: api/banner
        public async Task<IHttpActionResult> Get()
        {
            var banners = await _bannerService.GetBanners();
            return Ok(banners);
        }

        // GET: api/banner/id
        public async Task<IHttpActionResult> Get(string id)
        {
            var banner = await _bannerService.GetBannerById(id);
            if (banner == null) return NotFound();
            return Ok(banner);
        }

        // POST: api/banner
        public async Task<IHttpActionResult> Post(Banner newBanner)
        {
            if (ModelState.IsValid)
            {
                var banner = await _bannerService.CreateBanner(newBanner);
                if (banner == null) return BadRequest("Must contain valid html");
                var location = Request.RequestUri + "/" + banner.Id;
                return Created(location, banner);
            }

            return BadRequest();
        }

        // PUT: api/banner/id
        public async Task<IHttpActionResult> Put(string id, Banner newBanner)
        {
            if (ModelState.IsValid)
            {
                var banner = await _bannerService.UpdateBanner(id, newBanner);
                if (banner == null) return BadRequest("Not found or not valid html");
                return Ok(banner);
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/banner/id
        public async Task<IHttpActionResult> Delete(string id)
        {
            if (await _bannerService.DeleteBanner(id) > 0) return Ok();
            return NotFound();
        }

        // Renders the html directly
        [Route("api/banner/{id}/html")]
        public async Task<HttpResponseMessage> GetHtml(string id)
        {
            var banner = await _bannerService.GetBannerById(id);
            var response = new HttpResponseMessage();

            if (banner == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            response.Content = new StringContent(banner.Html);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
